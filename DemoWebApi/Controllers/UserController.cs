using Demo.IBLL;
using Demo.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using DemoWebApi.App_Start;
using log4net;

namespace DemoWebApi.Controllers
{
    [ControllerGroup("2","Swagger测试")]

    public class UserController : ApiController
    {
        public static readonly ILog log = LogManager.GetLogger("WebLogger");

        public IUserBLL _userBll;

        public UserController(IUserBLL _userBll)
        {
            this._userBll = _userBll;
        }

        [HttpGet]
        public IHttpActionResult GetUserList()
        {
            log.Info("Log日志打印:测试测试测试！");
            IEnumerable<User> userList = _userBll.GetUserList();
            return Json(userList);
        }

        [ApiAuthorize]
        [HttpGet]
        public IHttpActionResult GetUserListById(int id)
        {
            User user = _userBll.GetUserList().Where(x => x.Id == id).FirstOrDefault();
            return Json(user);
        }

    }
}