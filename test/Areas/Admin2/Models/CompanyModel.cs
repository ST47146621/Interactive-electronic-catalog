using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class CompanyModel
    {
        public string Sid { get; set; }
        public string Name { get; set; }
        public string Addr { get; set; }
        public string Phone { get; set; }
        public string Img { get; set; }

    }
    public class CompanyEditModel
    {
        [DisplayName("廠商代號")]
        public string Sid { get; set; }
        [DisplayName("廠商名稱")]
        public string Name { get; set; }
        [DisplayName("廠商地址")]
        public string Addr { get; set; }
        [DisplayName("廠商電話")]
        public string Phone { get; set; }
        [DisplayName("廠商圖片")]
        public HttpPostedFileBase Img { get; set; }

    }
}