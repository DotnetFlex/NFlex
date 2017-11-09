using NFlex.Core;

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
            _dbContext.SaveChanges();
        }
    }
}
