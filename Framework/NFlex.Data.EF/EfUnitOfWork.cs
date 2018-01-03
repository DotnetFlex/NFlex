using NFlex.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace NFlex.Data.EF
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public EfUnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.Single();
                entity.Reload();
                throw new EfConcurrencyException(ex);
            }
            catch(DbEntityValidationException ex)
            {
                throw new EfValidationException(ex);
            }
        }
    }
}
