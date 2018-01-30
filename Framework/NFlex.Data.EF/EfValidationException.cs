using System.Data.Entity.Validation;

namespace NFlex.Data.EF
{
    public class EfValidationException: DbEntityValidationException
    {
        /// <summary>
        /// 初始化Entity Framework实体验证异常
        /// </summary>
        /// <param name="exception">实体验证异常</param>
        public EfValidationException(DbEntityValidationException exception)
            : base("验证失败:", exception)
        {
            SetExceptionDatas(exception);
        }

        /// <summary>
        /// 设置异常数据
        /// </summary>
        private void SetExceptionDatas(DbEntityValidationException exception)
        {
            foreach (var errors in exception.EntityValidationErrors)
            {
                foreach (var error in errors.ValidationErrors)
                {
                    Data.Add(string.Format("{0} 属性验证失败", error.PropertyName), error.ErrorMessage);
                }
            }
        }
    }
}
