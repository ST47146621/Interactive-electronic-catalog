using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Extend
{
    public static class HttpRequestExtend
    {
        //自定義取得虛擬路徑
        public static string PrefixUrl(this HttpRequest httpRequest)
        {
            return httpRequest.ApplicationPath == "/" ? "" : httpRequest.ApplicationPath;
        }

        //自定義將網址轉換成虛擬路徑
        public static string UrlContent(this HttpRequest httpRequest, string Path)
        {
            System.Web.Mvc.UrlHelper method = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
            return method.Content("~" + Path);
        }
    }
}