using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketStore.Core.Configurations;
using TicketStore.Models;

namespace TicketStore.Core
{
    [DbConfigurationType(typeof(DataContextConfiguration))]
    public class ApplicationDataContext : IdentityDbContext<ApplicationUser>
    {
        private readonly MethodInfo _addMethod = typeof(ApplicationDataContext).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.IsGenericMethodDefinition && x.Name == "Add");
        private readonly MethodInfo _deleteMethod = typeof(ApplicationDataContext).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.IsGenericMethodDefinition && x.Name == "Delete");

        public const string HttpContextKey = "HCK_Key";

        static ApplicationDataContext()
        {
            
        }

        public ILogger Logger { get; set; }

        public int? ManagedThreadId { get; set; }

        [ThreadStatic]
        private static ApplicationDataContext _applicationDataContext;

        public ApplicationDataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            SetLogging();
            this.Configuration.ValidateOnSaveEnabled = false;
        }
        
       

        /// <summary>
        /// Включает лог SQL-а в режиме DEBUG-а
        /// </summary>
        public void SetLogging()
        {
            if(!Debugger.IsAttached)
                return;

            Logger = new FileLogger();

            this.Database.Log = s => Logger.Write("Sql", s);
        }

        #region UnitOfWorkMembers

        public static ApplicationDataContext JoinOrOpen()
        {
            if (HasHttpContext())
                return GetOrAddFromHttpContext();

            return _applicationDataContext ?? (_applicationDataContext = new ApplicationDataContext() { ManagedThreadId = Thread.CurrentThread.ManagedThreadId });
        }

        private static bool HasHttpContext()
        {
            return HttpContext.Current != null;
        }

        private static ApplicationDataContext GetOrAddFromHttpContext()
        {
            lock (HttpContext.Current.Items.SyncRoot)
            {
                if (HttpContext.Current.Items[HttpContextKey] == null)
                {
                    var dataContext = (ApplicationDataContext)null;
                    HttpContext.Current.Items[HttpContextKey] = dataContext = new ApplicationDataContext() { ManagedThreadId = Thread.CurrentThread.ManagedThreadId };
                    return dataContext;
                }
                else
                    return HttpContext.Current.Items[HttpContextKey] as ApplicationDataContext;
            }
        }

        public static void Close()
        {
            if (HasHttpContext())
                HttpContext.Current.Items[HttpContextKey] = null;
            if (_applicationDataContext != null)
                _applicationDataContext.Dispose();
        }

        private void EnsureThreadSafety()
        {
            if (ManagedThreadId.HasValue && Thread.CurrentThread.ManagedThreadId != ManagedThreadId.Value)
                throw new InvalidOperationException("DataContext is not thread safe");
        }

        public void Add(object @object, bool inTransactionScope = false)
        {
            var genericMethod = _addMethod.MakeGenericMethod(@object.GetType());
            genericMethod.Invoke(this, new[] { @object, inTransactionScope });
        }

        public void Add<TEntity>(TEntity entity, bool inTransactionScope = false) where TEntity : class
        {
            if(Set<TEntity>().Local.Contains(entity))
                return;

            if (entity is IEntityEvent)
                (entity as IEntityEvent).Adding(this);

            Set<TEntity>().Add(entity);

            if (!inTransactionScope)
                SaveChanges();

            if (entity is IEntityEvent)
                (entity as IEntityEvent).Added(this);

            if (!inTransactionScope)
                SaveChanges();
        }

        public void Delete(object @object, bool inTransactionScope = false)
        {
            var genericMethod = _deleteMethod.MakeGenericMethod(@object.GetType());
            genericMethod.Invoke(this, new[] { @object, inTransactionScope });
        }

        public void Delete<TEntity>(TEntity entity, bool inTransactionScope = false) where TEntity : class
        {
            //use scope key to notify child deleting do not use save changes

            var cascadeDeleteProcessor = new CascadeDeleteProcessor();
            cascadeDeleteProcessor.Process(entity);
            if (entity is IEntityEvent)
                (entity as IEntityEvent).Deleting(this);

            Set(typeof(TEntity)).Remove(entity);

            if (!inTransactionScope)
                SaveChanges();

            if (entity is IEntityEvent)
                (entity as IEntityEvent).Deleted(this);

            if (!inTransactionScope)
                SaveChanges();
        }

        public void Commit(bool inTransactionScope = false)
        {
            var entries = new List<DbEntityEntry>();

            foreach (var dbEntityEntry in this.ChangeTracker.Entries())
            {
                if (dbEntityEntry.State != EntityState.Added && dbEntityEntry.State != EntityState.Deleted && 
                    dbEntityEntry.State  != EntityState.Modified && dbEntityEntry.State != EntityState.Detached)
                    continue;

                var entity = dbEntityEntry.Entity;
                entries.Add(dbEntityEntry);

                if (entity is IEntityEvent)
                    (entity as IEntityEvent).Updating(this);
            }

            if (!inTransactionScope)
                SaveChanges();

            foreach (var dbEntityEntry in entries)
            {
                var entity = dbEntityEntry.Entity;
                if (entity is IEntityEvent)
                    (entity as IEntityEvent).Updated(this);
            }

            if (!inTransactionScope)
                SaveChanges();
        }

        #endregion

        #region Sets


        /*
        public IDbSet<Delivery> Delivery
        {
            get
            {
                EnsureThreadSafety();
                return Set<Delivery>();
            }
        }
        */


        #endregion

        #region Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CarPartUnderSection>()
            //    .HasRequired(x => x.CarPartSection)
            //    .WithMany()
            //    .HasForeignKey(x => x.CarPartSectionId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}