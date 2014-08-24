using MVCWithAutofac.Core.Interfaces;
using MVCWithAutofac.Core.Model;
using System.Web.Mvc;

namespace MVCWithAutofac.Web.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public IRepository repo { set; get; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            repo.Insert<Log>(new Log
            {
                Action = string.Format("Controller: {0} , Action: {1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName),                    
                Name = "Log entry"
            });
            repo.SaveChanges();
        }
    }
}