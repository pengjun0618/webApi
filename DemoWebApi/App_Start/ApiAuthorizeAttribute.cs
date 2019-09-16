using Demo.Model;
using JWT;
using JWT.Serializers;
using log4net;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoWebApi.App_Start
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public static readonly ILog log = LogManager.GetLogger("WebLogger");
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = from t in actionContext.Request.Headers where t.Key == "Authorization" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string secretKey = "JWT SECRET"; //口令加密秘钥
                string token = authHeader.FirstOrDefault();//获取token
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        byte[] key = Encoding.UTF8.GetBytes(secretKey);
                        IJsonSerializer serializer = new JsonNetSerializer();
                        IDateTimeProvider provider = new UtcDateTimeProvider();
                        IJwtValidator validator = new JwtValidator(serializer, provider);
                        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                        //解密
                        var json = decoder.DecodeToObject<Payload>(token, key, verify: true);
                        if (json.ExpiryDateTime < DateTime.Now)
                        {
                            return false;
                        }

                        actionContext.RequestContext.RouteData.Values.Add("Authorization", json); 
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log.Error("token生成失败:"+ex.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 授权验证失败 返回统一格式
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            var response = filterContext.Response = filterContext.Response ?? new HttpResponseMessage();
            var content = new ApiResultModel
            {
                Code = 401,
                Message = "服务端拒绝访问：你没有权限，或者掉线了"
            };
            response.Content = new StringContent(Json.Encode(content), Encoding.UTF8, "application/json");
        }
    }
}