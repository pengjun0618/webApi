using Demo.Model;
using JWT;
using JWT.Serializers;
using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoWebApi.App_Start
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = from t in actionContext.Request.Headers where t.Key == "Authentication" select t.Value.FirstOrDefault();
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

                        actionContext.RequestContext.RouteData.Values.Add("Authentication", json);
                        return true;
                    }
                    catch (Exception ex)
                    {
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
    }
}