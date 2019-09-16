using Demo.IBLL;
using Demo.Model;
using DemoWebApi.App_Start;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Text;
using System.Web.Http;

namespace DemoWebApi.Controllers
{
    [ControllerGroup("1", "Token创建")]
    public class TokenController : ApiController
    {
        public IUserBLL _userBll;

        public TokenController(IUserBLL _userBll)
        {
            this._userBll = _userBll;
        }

        [HttpPost]
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