using NFlex.Core;
using System.Data.Entity.Validation;

namespace NFlex.Data.EF
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public EfUnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                throw new EfValidationException(ex);
            }
        }
    }
}
