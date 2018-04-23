using Demo.Domain.Models;
using Demo.Domain.Repositories;
using NFlex.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class UserRepository:RepositoryBase<UserInfo>, IUserRepository
    {
        public UserRepository(IDbContext context):base(context)
        {

        }
    }
}
