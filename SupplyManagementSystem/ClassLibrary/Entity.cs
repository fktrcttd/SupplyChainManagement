using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace TicketStore.Core
{
    [DebuggerDisplay("{TypeName,nq}={Id,nq}")]
    public class Entity : IEntityEvent, ICloneable
    {
        public Entity()
        {
            this.Title = string.Empty;
        }

        public int Id { get; set; }

        [DisplayName("Запрещено удалять")]
        public bool DeleteForbidden { get; set; }

        [DisplayName("Запрещено редактировать")]
        public bool EditForbidden { get; set; }

        [DisplayName("Название")]
        [DefaultValue("")]
        public string Title { get; set; }


        [DisplayName("Публиковать")]
        public bool IsPublish { get; set; }


        [DisplayName("Позиция")]
        [PropertyOrder(-1)]
        public int Position { get; set; }

        public DateTime? LastModifed { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual string TypeName
        {
            get
            {
                return ObjectContext.GetObjectType(this.GetType()).Name;
            }
        }

        /// <summary>
        /// Используется только при первичном заполнении базы данных и для проверки есть ли объект, если нет то автоматически добавляется.
        /// <remarks>Метод Seed где используется данное поле вызывается при каждом вызове update-database или миграции</remarks>
        /// </summary>
        [DisplayName("ИД для заполнения данными проекта")]
        public int BootstrappingId { get; set; }

        public virtual IDictionary<string, object> SiteUrlArguments
        {
            get
            {
                return new Dictionary<string, object>();
            }
        }

        public virtual void Added(ApplicationDataContext context)
        {
            
        }

        public virtual void Adding(ApplicationDataContext context)
        {
            LastModifed = DateTime.Now;
        }

        public virtual void Deleting(ApplicationDataContext context)
        {

        }

        public virtual void Deleted(ApplicationDataContext context)
        {

        }

        public virtual void Updating(ApplicationDataContext context)
        {
            LastModifed = DateTime.Now;
        }

        public virtual void Updated(ApplicationDataContext context)
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}