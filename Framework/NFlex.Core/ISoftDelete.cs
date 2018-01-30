namespace NFlex.Core
{
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否已删除
        /// </summary>
        bool IsDeleted { get; set; }

    }
}
