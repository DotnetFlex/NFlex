using NFlex.Ioc;

namespace NFlex.Core
{
    public interface IUnitOfWork: IPerLifetimeDependency
    {
        /// <summary>
        /// 提交更新
        /// </summary>
        int Commit();
    }
}
