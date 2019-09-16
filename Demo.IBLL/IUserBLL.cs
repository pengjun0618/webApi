﻿using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.IBLL
{
    public interface IUserBLL
    {
        IEnumerable<User> GetUserList();

        User GetUserById(int id);

       User GetUserByUserNameAndPassword(string userName, string password);
    }
}
