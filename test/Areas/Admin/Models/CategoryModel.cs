using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Models
{
    public class CategoryModel
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("種類")]
        //[Remote("CheckProductId", "Admin", "Admin", ErrorMessage = "商品編號已存在")]
        [StringLength(10, ErrorMessage = "最多輸入10字")]
        public string Name { get; set; }

        [DisplayName("商品圖片")]
        [Required]
        public HttpPostedFileBase ImgUrl { get; set; }

        [DisplayName("簡介")]
        [Required]
        public string Summary { get; set; }
    }

    public class CategoryNoValidModel
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("種類")]
        //[Remote("CheckProductId", "Admin", "Admin", ErrorMessage = "商品編號已存在")]
        [StringLength(10, ErrorMessage = "最多輸入10字")]
        public string Name { get; set; }

        [DisplayName("商品圖片")]
        [Required]
        public HttpPostedFileBase ImgUrl { get; set; }

        [DisplayName("簡介")]
        [Required]
        public string Summary { get; set; }

        public string ImgUrl1 { get; set; }
    }
}