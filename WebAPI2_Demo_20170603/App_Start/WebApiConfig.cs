using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI2_Demo_20170603.Controllers;
using System.Web.Http.Cors;

namespace WebAPI2_Demo_20170603
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 動作過濾器
            config.Filters.Add(new ValidateModelAttribute());
            // 例外過濾器
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            // 設定CORS
            //var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            //config.EnableCors(cors);
            config.EnableCors();
        }
    }
}
