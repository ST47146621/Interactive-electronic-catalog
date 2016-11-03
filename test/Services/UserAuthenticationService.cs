using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Services
{
    public class UserAuthenticationService : Authentication, IUserAuthenticationService
    {
        public void OnLogin(LoginModel model)
        {
            base.GetCommonAuthentication(model.account
                , model.password
                , null
                , model.rememberMe);
        }

        public void OnLogOut()
        {
            base.LoginOut();
        }
    }
}