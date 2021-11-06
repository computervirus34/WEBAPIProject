using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPIProject.ActionFilters;

namespace WEBAPIProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //    // Web API configuration and services
            config.Filters.Add(new LoggingFilterAttribute());
            //Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //    //config.Routes.MapHttpRoute(
            //    //   name: "ActionBased",
            //    //   routeTemplate: "api/{controller}/{action}/{id}",
            //    //   defaults: new { id = RouteParameter.Optional }
            //    //);
            //    //config.Routes.MapHttpRoute(
            //    //  name: "ActionBased1",
            //    //  routeTemplate: "api/{controller}/action/{action}/{id}",
            //    //  defaults: new { id = RouteParameter.Optional }
            //    //);
        }
    }
}
