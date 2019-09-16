using System.Web.Http;
using WebActivatorEx;
using DemoWebApi;
using Swashbuckle.Application;
using System;
using DemoWebApi.App_Start;
using System.Reflection;
using Swashbuckle.Swagger;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DemoWebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>

                    {   //文档版本、标题
                        c.SingleApiVersion("v1", "SwaggerApi");
                        //注册XML文件路径
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        //注册CachingSwaggerProvider
                        c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                        //注册Swagger分组
                        c.GroupActionsBy(apiDesc => apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().Any() ? apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().First().GroupName + "." + apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().First().Useage : "无模块归类");
                        //请求头加token
                        c.OperationFilter<HttpHeaderFilter>();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectJavaScript(Assembly.GetExecutingAssembly(), "DemoWebApi.Scripts.Swagger.swagger.js");
                    });
        }
        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\bin\DemoWebApi.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
