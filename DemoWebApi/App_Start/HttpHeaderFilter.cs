using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

namespace DemoWebApi.App_Start
{
    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, System.Web.Http.Description.ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            //判断是否添加权限过滤器
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            //判断是否允许匿名方法 
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); 
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAuthorized && !allowAnonymous)
            {
                operation.parameters.Add(new Parameter { name = "Authorization", @in = "header", description = "Token", required = false, type = "string" });
            }
        }
    }
}