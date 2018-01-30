using System;

namespace NFlex.Core
{
    public abstract class AggregateRoot<TKey> : EntityBase<TKey>, IAggregateRoot<TKey>
    {
        protected AggregateRoot() : base() { }
        protected AggregateRoot(TKey id) : base(id) { }

        /// <summary>
        /// 版本号(乐观锁)
        /// </summary>
        //[Timestamp]
        //public byte[] Version { get; set; }
    }

    public abstract class AggregateRoot: AggregateRoot<Guid>
    {
        protected AggregateRoot() : base() { }
        protected AggregateRoot(Guid id) : base(id) { }
    }
}
