using Demo.IBLL;
using Demo.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using DemoWebApi.App_Start;

namespace DemoWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ControllerGroup("Swagger测试控制器","Swagger测试")]
    [ApiAuthorize]
    public class UserController : ApiController
    {

        public IUserBLL _userBll;

        public UserController(IUserBLL _userBll)
        {
            this._userBll = _userBll;
        }

        [HttpGet]
        public IHttpActionResult GetUserList()
        {

            IEnumerable<User> userList = _userBll.GetUserList();
            return Json(userList);
        }

        [HttpGet]
        public IHttpActionResult GetUserListById(int id)
        {
            User user = _userBll.GetUserList().Where(x => x.Id == id).FirstOrDefault();
            return Json(user);
        }

    }
}