using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Services.Generic
{
    public interface IAuthentication
    {

        /// <summary>
        /// 驗證使用者登入資訊
        /// </summary>
        /// <param name="account">使用者輸入的帳號</param>
        /// <param name="password">使用者輸入的密碼</param>
        void GetCommonAuthentication(string account, string password, string enterprise, bool isRember);

        /// <summary>
        /// 使用者登出
        /// </summary>
        void LoginOut();

        /// <summary>
        /// 判斷是否是內部連線
        /// </summary>
        /// <returns></returns>
        bool IsInternalConnect();

        /// <summary>
        /// 亂數取得新密碼
        /// </summary>
        /// <returns></returns>
        string GetRandPassword();

        /// <summary>
        /// 將密碼加鹽
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <param name="salt">鹽</param>
        /// <returns></returns>
        //string CreatPasswordHash(string account, string password, string salt);

        /// <summary>
        /// 製造鹽
        /// </summary>
        /// <returns></returns>
        //string CreatSalt();

    }
}