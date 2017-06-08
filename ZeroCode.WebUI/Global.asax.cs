using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZeroCode.AutoMapper;
using ZeroCode.Components;

namespace ZeroCode.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();   //ioc注册
            Configuration.Configure();          //AutoMapper映射注册

        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    var lastError = Server.GetLastError();
        //    if (lastError != null)
        //    {
        //        var httpError = lastError as HttpException;
        //        if (httpError != null)
        //        {
        //            //ASP.NET的400与404错误不记录日志，并都以自定义404页面响应
        //            var httpCode = httpError.GetHttpCode();
        //            if (httpCode == 400 || httpCode == 404)
        //            {
        //                Response.StatusCode = 404;
        //                Server.ClearError();
        //                Response.Redirect("/404.html");
        //                return;
        //            }

        //        }

        //        //对于路径错误不记录日志，并都以自定义404页面响应
        //        if (lastError.TargetSite.ReflectedType == typeof(System.IO.Path))
        //        {
        //            Response.StatusCode = 404;
        //            Response.Redirect("/404.html");
        //            Server.ClearError();
        //            return;
        //        }

        //        Response.StatusCode = 500;
        //        Server.ClearError();
        //        Response.Redirect("/500.html");
        //    }
        //}
    }
}
