
using Demo.Infrastructure;
using Demo.Infrastructure.Caching;
using NFlex.Caching;
using NFlex.Caching.Memory;
using NFlex.Logging;
using System.Linq;
using System.Web.Mvc;
using NFlex.Ioc;
using System;
using static Demo.Web.IocConfig;
using NFlex.Core;
using Demo.Domain.Models;
using System.Collections.Generic;
using NFlex;
using NFlex.Data.EF;
using Demo.Infrastructure.Repositories;
using Demo.Domain.Repositories;

namespace Demo.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ILogger logger { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public UserRepository _userRepository { get; set; }
        public ISqlExecuter _sqlExecuter { get; set; }


        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var userId = Guid.Parse("4232581B-35CF-4528-AF05-7A5BDB9F649E");
            var userInfo = _userRepository.Single(userId);
            //userInfo.Age = 18;

            //DemoDbContext context = new DemoDbContext();
            //var u = context.UserInfo.FirstOrDefault(t => t.Id == userId);
            //u.Age = 20;
            //context.SaveChanges();

            
            ////userInfo.Version = u.Version;
            ////userInfo = _userRepository.QueryableAsNoTracking.FirstOrDefault(t => t.Id == userId);
            ////userInfo.Age = 19;
            //try
            //{
            //    //_unitOfWork.Commit();
            //}
            //catch
            //{
            //    //_unitOfWork.Commit();
            //}
            return View();
        }
    }
}