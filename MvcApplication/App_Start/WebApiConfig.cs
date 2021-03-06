using MvcApplication.Support;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MvcApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new MyCustomAuthorizationAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void Configure (IAppBuilder app)
        {

        }
    }
}
