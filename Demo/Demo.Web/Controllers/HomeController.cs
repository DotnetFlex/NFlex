
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

namespace Demo.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ILogger logger { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }


        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}