using System;
using System.ComponentModel.DataAnnotations;

namespace NFlex.Core
{
    public abstract class EntityBase<TKey>:IEntity<TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        [Key]
        public TKey Id { get; set; }

        protected EntityBase()
        {
            var type = Common.GetType<TKey>();
            if (type.Name.ToLower() == "guid") Id = (TKey)(object)Guid.NewGuid();
        }
        protected EntityBase(TKey id)
        {
            Id = id;
        }
    }

    public abstract class EntityBase : EntityBase<Guid>
    {
        protected EntityBase() : base() { }
        protected EntityBase(Guid id) : base(id)
        {
            if (Id == Guid.Empty) Id = Guid.NewGuid();
        }
    }
}
