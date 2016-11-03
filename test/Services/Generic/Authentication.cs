using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using test.Extend;
using test.Models;

namespace test.Services.Generic
{
    public class Authentication : IAuthentication
    {
        private IUnitOfWork unitOfWork = new UnitOfWork(new ARealShopEntities());

        public void GetCommonAuthentication(string account, string password, string enterprise, bool isRember)
        {

            //enterprise = enterprise.ToLower();

            //Member member = this.unitOfWork
            //    .Repository<Member>().Get(p => p.TargetSystem == Settings.Default.SystemName
            //        && p.UserName == account && p.Enterprise == enterprise).FirstOrDefault();
            Member member = this.unitOfWork
                .Repository<Member>().Get(p => p.Account == account).FirstOrDefault();

            if (member == null)
                throw new MyException("查無此會員帳號");

            //string hashPaw = this.CreatPasswordHash(account, password, member.Salt);

            //if (member.Password != hashPaw)
            //throw new MyException(AuthencationMessageResource.WrongPassword);
            Member pass = this.unitOfWork
                .Repository<Member>().Get(p => p.Account.Equals(account) && p.Password.Equals(password)).FirstOrDefault();
            if (pass == null)
            {
                throw new MyException("帳號或密碼錯誤");
            }

            // TODO : userName 暫時等於 account
            this.AddAuthCookie(account, account, isRember);

            this.AddUserCookie(member, 1, isRember);
        }

        public void LoginOut()
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            HttpContext.Current.Session.RemoveAll();

            CacheExtensions.ClearAllCache();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);

            //建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            cookie = new HttpCookie("ASP.NET_SessionId", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);

            //刪除 USC 的 Cookie
            if (HttpContext.Current.Request.Cookies["USC"] != null)
            {
                cookie = new HttpCookie("USC", "");
                cookie.Expires = DateTime.Now.AddYears(-1);
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Domain = FormsAuthentication.CookieDomain;
                HttpContext.Current.Response.SetCookie(cookie);
            }

            //刪除 UIC 的 Cookie
            if (HttpContext.Current.Request.Cookies["UIC"] != null)
            {
                if (String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UIC"]["RememberMe"]))
                {
                    cookie = new HttpCookie("UIC", "");
                    cookie.Expires = DateTime.Now.AddYears(-1);
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    cookie.Domain = FormsAuthentication.CookieDomain;
                    HttpContext.Current.Response.SetCookie(cookie);
                }
            }
        }

        //public string CreatSalt()
        //{
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    int size = new Random().Next(5, 100);
        //    byte[] buff = new byte[size];
        //    rng.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}

        //public string CreatPasswordHash(string account, string password, string salt)
        //{
        //    SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider();
        //    string saltAndPwd = String.Concat(account, salt, password);
        //    string hashedPwd = Convert.ToBase64String(SHA512.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd)));
        //    return hashedPwd;
        //}

        public bool IsInternalConnect()
        {
            var url = "";

            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)  // 判斷是否有設定代理伺服器(Proxy)
                url = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; // 取得連線IP位址
            else
                url = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];  // 取得Proxy的伺服端IP位址

            if (url.StartsWith("192.168") || url.Equals("::1"))
                return true;
            else
                return false;
        }

        public string GetRandPassword()
        {
            string newPassword = "";
            string pattern = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();
            for (var i = 0; i < 8; i++)
                newPassword += pattern[r.Next(0, pattern.Length)];
            return newPassword;
        }

        #region private

        /// <summary>新增登入Cookie</summary>
        /// <param name="account">使用者帳號</param>
        /// <param name="isRember">是否記住我</param>
        private void AddAuthCookie(string account, string userName, bool isRember)
        {

            DateTime time = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: account,
                issueDate: time,
                expiration: time.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                isPersistent: isRember,
                userData: userName,
                cookiePath: FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = HttpContext.Current.Request.ApplicationPath;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 新增登入使用者資訊Cookie
        /// </summary>
        /// <param name="member">登入資訊</param>
        /// <param name="Language">語言</param>
        private void AddUserCookie(Member member, int Language, bool rememberMe)
        {
            //if (String.IsNullOrEmpty(member.Auth))
            //member.Auth = String.Empty;

            //新增UserInfo Cookie
            HttpCookie UserInfoCookie = new HttpCookie("UIC");
            UserInfoCookie.HttpOnly = true;
            UserInfoCookie.Name = "UIC";
            UserInfoCookie.Expires = DateTime.Now.AddYears(1);
            UserInfoCookie.Path = FormsAuthentication.FormsCookiePath;
            UserInfoCookie.Domain = FormsAuthentication.CookieDomain;
            UserInfoCookie.Values.Add("Name", member.Account.ToUpper());
            UserInfoCookie.Values.Add("Role", member.Role.ToUpper());
            
            //UserInfoCookie.Values.Add("Enterprise", member.Enterprise.ToUpper());
            //UserInfoCookie.Values.Add("Language", Language.ToString());
            //UserInfoCookie.Values.Add("Report", member.DataBaseConnect.TargetReport.ToUpper());
            //UserInfoCookie.Values.Add("Group", member.Auth.ToUpper());
            UserInfoCookie.Values.Add("RememberMe", rememberMe ? "T" : String.Empty);
            UserInfoCookie.Values.Add("MyName",HttpUtility.UrlEncode(member.Name));

            if (HttpContext.Current.Request.Cookies["UIC"] == null)
                HttpContext.Current.Response.AppendCookie(UserInfoCookie);
            else
                HttpContext.Current.Response.SetCookie(UserInfoCookie);
        }

        #endregion

    }
}