using Demo.IDAL;
using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class UserDAL: IUserDAL
    {
        public User[] users;
        public UserDAL()
        {
            users = new User[]
            {
                new User(){ Id = 1, UserName = "zhangsan",Password="123456" ,Age = 58 },
                new User(){ Id = 2, UserName = "lisi", Password="123456" ,Age = 18 },
                new User(){ Id = 3, UserName = "wangwu", Password="123456" ,Age = 15 }
            };
        }

        public IEnumerable<User> GetUserList()
        {
            return users;
        }

        public User GetUserById(int id)
        {
            return users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User GetUserByUserNameAndPassword(string userName,string password)
        {
            User user = users.Where(x=>x.UserName==userName&&x.Password==password).FirstOrDefault();
            return user;
        }

    }
}
