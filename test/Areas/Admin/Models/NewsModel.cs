using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class NewsModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("標題")]
        public string Title { get; set; }

        [DisplayName("內文")]
        public string Text { get; set; }

        [DisplayName("新增時間")]
        public DateTime? Time { get; set; }
        [DisplayName("商品圖片")]
        [Required]
        public HttpPostedFileBase ImgUrl { get; set; }
    }
    public class NewsNoValidModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("標題")]
        public string Title { get; set; }

        [DisplayName("內文")]
        public string Text { get; set; }

        [DisplayName("新增時間")]
        public DateTime? Time { get; set; }
        [DisplayName("商品圖片")]
        public HttpPostedFileBase ImgUrl { get; set; }
        public string ImgUrl1 { get; set; }
    }
}