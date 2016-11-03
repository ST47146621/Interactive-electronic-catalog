using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Models
{
    public class LoginModel
    {
        [Display(Name = "登入帳號")]
        [Required]
        public string account { get; set; }

        [Display(Name = "登入密碼")]
        [Required]
        public string password { get; set; }

        [Display(Name = "記住我")]
        public bool rememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class LoginReturn
    {
        public bool status { get; set; }
        public string ReturnUrl { get; set; }
    }
}