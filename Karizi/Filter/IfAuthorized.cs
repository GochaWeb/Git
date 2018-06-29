using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Karizi.Models;

namespace Karizi.Filter
{
    public class IfAuthorized : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           Karizi.Models.Login US =(Karizi.Models.Login)filterContext.HttpContext.Session["Login"];
            if (US == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{{"controller","Home"},
                                    {"action","Index"}

                    });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}