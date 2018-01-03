using Demo.Domain.Models;
using NFlex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repositories
{
    public interface IUserRepository:IRepository<UserInfo>
    {
    }
}
