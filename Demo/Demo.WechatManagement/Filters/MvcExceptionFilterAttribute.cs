using NFlex.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace Demo.WechatManagement.Filters
{
    public class MvcExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            Log.Error(exception.Message, exception);

            var result = new ContentResult();
            result.Content = "";
            filterContext.Result = result;
        }
    }
}