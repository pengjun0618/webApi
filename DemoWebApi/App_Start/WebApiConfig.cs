using System.Web.Http;
using System.Web.Http.Cors;

namespace DemoWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            //config.EnableCors(new EnableCorsAttribute("http://localhost:57777,http://localhost:57447", "*", "*"));
            // Web API 路由
            config.MapHttpAttributeRoutes();

            // 解决json序列化时的循环引用问题
            //json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //返回json 格式化xml
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver(),        //小驼峰命名法,格式化日期时间
            //    DateFormatString = "yyyy-MM-dd HH:mm:ss"
            //};

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
