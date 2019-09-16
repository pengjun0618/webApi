using Demo.IBLL;
using Demo.Model;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DemoWebApi.Controllers
{
    public class TokenController : ApiController
    {
        public IUserBLL _userBll;

        public TokenController(IUserBLL _userBll)
        {
            this._userBll = _userBll;
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Login(LoginRequest loginRequest)
        {
            TokenInfo tokenInfo = new TokenInfo();
            if (loginRequest != null)
            {
                string userName = loginRequest.UserName;
                string password = loginRequest.Password;

                User user = _userBll.GetUserByUserNameAndPassword(userName, password);
                if (user == null)
                {
                    tokenInfo.Success = false;
                    tokenInfo.Message = "当前用户不存在！";
                }

                Payload payload = new Payload();
                payload.UserName = userName;
                payload.ID = user.Id;
                payload.ExpiryDateTime = DateTime.Now.AddHours(2);
                string secretKey = "JWT SECRET"; //口令加密秘钥
                try
                {
                    byte[] key = Encoding.UTF8.GetBytes(secretKey);
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
                    IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
                    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
                    var token = encoder.Encode(payload, key);//生成令牌
                    tokenInfo.Success = true;
                    tokenInfo.Token = token;
                }
                catch (Exception ex)
                {
                    tokenInfo.Success = false;
                    tokenInfo.Message = ex.Message;
                }

            }
            else
            {
                tokenInfo.Success = false;
                tokenInfo.Message = "用户或密码为空！";
            }
            return Json(tokenInfo);
        }
    }
}