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
using SCM.Core;
using SCM.Models;
using SCM.Models.IdentityModels;

namespace SCM.DataService.DataContext
{
    public class AppDataContext : IdentityDbContext<ScmUser, ScmRole, int, ApplicationUserLogin, ScmUserRole,
        ApplicationUserClaim>
    {
        public AppDataContext()
            : base("DefaultConnection")
        {
            SetLogging();
            this.Configuration.ValidateOnSaveEnabled = false;
        }
        
        public static AppDataContext Create()
        {
            return new AppDataContext();
        }

        static AppDataContext()
        {
            Database.SetInitializer(new IdentityDbInit());
        }
        
        public readonly MethodInfo _addMethod = typeof(AppDataContext).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.IsGenericMethodDefinition && x.Name == "Add");
        public readonly MethodInfo _deleteMethod = typeof(AppDataContext).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.IsGenericMethodDefinition && x.Name == "Delete");

        private const string HttpContextKey = "HCK_Key";

        private ILogger Logger { get; set; }

        private int? ManagedThreadId { get; set; }

        [ThreadStatic]
        private static AppDataContext _appDataContext;
        
        private void SetLogging()
        {
            if(!Debugger.IsAttached)
                return;

            Logger = new FileLogger();

            this.Database.Log = s => Logger.Write("Sql", s);
        }

        #region UnitOfWorkMembers

        public static AppDataContext JoinOrOpen()
        {
            if (HasHttpContext())
                return GetOrAddFromHttpContext();

            return _appDataContext ?? (_appDataContext = new AppDataContext() { ManagedThreadId = Thread.CurrentThread.ManagedThreadId });
        }

        private static bool HasHttpContext()
        {
            return HttpContext.Current != null;
        }

        private static AppDataContext GetOrAddFromHttpContext()
        {
            lock (HttpContext.Current.Items.SyncRoot)
            {
                if (HttpContext.Current.Items[HttpContextKey] == null)
                {
                    var dataContext = (AppDataContext)null;
                    HttpContext.Current.Items[HttpContextKey] = dataContext = new AppDataContext() { ManagedThreadId = Thread.CurrentThread.ManagedThreadId };
                    return dataContext;
                }
                else
                    return HttpContext.Current.Items[HttpContextKey] as AppDataContext;
            }
        }

        public static void Close()
        {
            if (HasHttpContext())
                lock (HttpContext.Current.Items.SyncRoot)
                {
                    HttpContext.Current.Items[HttpContextKey] = null;
                }

            if (_appDataContext != null)
                _appDataContext.Dispose();
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
        
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ScmUser>().ToTable("Users");
            modelBuilder.Entity<ScmRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ScmUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        #endregion

        #region Sets

        /*public IDbSet<SomeClass> Departaments
        {
            get
            {
                EnsureThreadSafety();
                return Set<SomeClass>();
            }
        }*/
        public IDbSet<Client> Clients
        {
            get
            {
                EnsureThreadSafety();
                return Set<Client>();
            }
        }
        public IDbSet<Sample> Samples
        {
            get
            {
                EnsureThreadSafety();
                return Set<Sample>();
            }
        }
        public IDbSet<Order> Orders
        {
            get
            {
                EnsureThreadSafety();
                return Set<Order>();
            }
        }
        public IDbSet<Worker> Workers
        {
            get
            {
                EnsureThreadSafety();
                return Set<Worker>();
            }
        }
        public IDbSet<Release> Releases
        {
            get
            {
                EnsureThreadSafety();
                return Set<Release>();
            }
        }

        public IDbSet<ChemicalComposition> ChemicalCompositions
        {
            get
            {
                EnsureThreadSafety();
                return Set<ChemicalComposition>();
            }
        }

        public IDbSet<ChemicalElement> ChemicalElements
        {
            get
            {
                EnsureThreadSafety();
                return Set<ChemicalElement>();
            }
        }
        #endregion

    }
}