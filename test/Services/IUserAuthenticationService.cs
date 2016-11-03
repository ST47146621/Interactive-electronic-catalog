using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="model"></param>
        void OnLogin(LoginModel model);

        /// <summary>
        /// 使用者登出
        /// </summary>
        void OnLogOut();
    }
}