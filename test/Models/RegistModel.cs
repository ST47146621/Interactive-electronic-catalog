using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Models
{
    public class RegistModel
    {
        [Required]
        [Remote("CheckAccount","Home")]
        [MaxLength(20)]
        public string Account { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        public string Addr { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        public string Role { get; set; }
        [Required]
        public bool? Sex { get; set; }
        [Required]
        public byte? Job { get; set; }
        [Required]
        public byte? Style { get; set; }
        [Required]
        public byte? Money { get; set; }
    }
    public class MemberSectionModel
    {
        public string Account { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password2 { get; set; }
        [Required]
        public string Addr { get; set; }
        public string Phone { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        public string Birth { get; set; }
        public string Sex { get; set; }
        public string Job { get; set; }
        public byte? Style { get; set; }
        public string Money { get; set; }
    }
}