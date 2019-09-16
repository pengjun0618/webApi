using Demo.IBLL;
using Demo.IDAL;
using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UserBLL: IUserBLL
    {
        public IUserDAL Dal;

        public UserBLL(IUserDAL _Dal)
        {
            Dal = _Dal;
        }

        public IEnumerable<User> GetUserList()
        {
            return Dal.GetUserList();
        }

        public User GetUserById(int id)
        {
            return Dal.GetUserById(id);
        }
        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            return Dal.GetUserByUserNameAndPassword(userName,password);
        }
    }
}
