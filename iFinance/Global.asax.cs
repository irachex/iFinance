using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace iFinance
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                "Auth", // 路由名称
                "auth/{action}/{id}", // 带有参数的 URL
                new { controller = "auth", action = "login", id = UrlParameter.Optional } // 参数默认值
            );

            routes.MapRoute(
                "Note",
                "note/{action}/{id}",
                new { controller = "note", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Admin",
                "admin/{action}/{id}",
                new { controller = "admin", action = "index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                "Default", // 路由名称
                "{action}/{id}", // 带有参数的 URL
                new { controller = "home", action = "index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}