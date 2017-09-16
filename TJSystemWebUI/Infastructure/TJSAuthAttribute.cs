using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace TJSystemWebUI.Infastructure
{
    public class TJSAuthAttribute:FilterAttribute,IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            IIdentity identity = filterContext.Principal.Identity;
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!   "+identity.Name+"  "+identity.AuthenticationType);
            if (!identity.IsAuthenticated)
            {
                filterContext.Result=new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result=new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","Account" },
                    {"Action","Login" },
                    {"returnUrl",filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }
}