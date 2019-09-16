using Autofac;
using Autofac.Integration.WebApi;
using Demo.BLL;
using Demo.DAL;
using Demo.IBLL;
using Demo.IDAL;
using System.Reflection;
using System.Web.Http;

namespace DemoWebApi.App_Start
{
    public class AutofacWebApiConfig
    {
        public static void Run()
        {
            SetAutofacWebApi();
        }

        private static void SetAutofacWebApi()
        {
            //Autofac
            ContainerBuilder builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            SetupResolveRules(builder);
            // 使用程序集扫描注册API控制器            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 可选:注册Autofac过滤器提供程序
            builder.RegisterWebApiFilterProvider(config);

            // 将依赖项解析器设置为Autofac
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<UserDAL>().As<IUserDAL>();
            builder.RegisterType<UserBLL>().As<IUserBLL>();
        }
    }
}