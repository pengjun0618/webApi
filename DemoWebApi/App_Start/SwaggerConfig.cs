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

                    {   //�ĵ��汾������
                        c.SingleApiVersion("v1", "SwaggerApi");
                        //ע��XML�ļ�·��
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        //ע��CachingSwaggerProvider
                        c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                        //ע��Swagger����
                        c.GroupActionsBy(apiDesc=>apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().Any() ? apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().First().GroupName+"."+ apiDesc.GetControllerAndActionAttributes<ControllerGroupAttribute>().First().Useage:"��ģ�����");
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectJavaScript(Assembly.GetExecutingAssembly(), "DemoWebApi.Scripts.swagger.js");
                    });
        }
        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\bin\DemoWebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
