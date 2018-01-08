using NFlex.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace NFlex.Data.EF
{
    public abstract class EfUnitOfWork:IUnitOfWork
    {
        protected IDbContext _dbContext { get;private set; }

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
