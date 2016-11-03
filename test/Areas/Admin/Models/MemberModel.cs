using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class MemberModel
    {

        [Required]
        [DisplayName("帳號")]
        public string Account { get; set; }
        [Required]
        [DisplayName("密碼")]
        public string Password { get; set; }
        [Required]
        [DisplayName("地址")]
        public string Addr { get; set; }
        [Required]
        [DisplayName("電話號碼")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string Name { get; set; }
        [Required]
        [DisplayName("生日")]
        public string Birth { get; set; }
    }
    public class MemberEditModel
    {
        [Required]
        [DisplayName("帳號")]
        public string Account { get; set; }
        [Required]
        [DisplayName("密碼")]
        public string Password { get; set; }
        [Required]
        [DisplayName("地址")]
        public string Addr { get; set; }
        [Required]
        [DisplayName("電話號碼")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string Name { get; set; }
        [Required]
        [DisplayName("生日")]
        public DateTime? Birth { get; set; }
    }
}