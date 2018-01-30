namespace NFlex.Core
{

    public interface IAggregateRoot:IEntity
    {
        /// <summary>
        /// 版本号(乐观锁)
        /// </summary>
        //byte[] Version { get; set; }
    }
    public interface IAggregateRoot<out TKey>:IEntity<TKey>, IAggregateRoot
    {
    }
}
