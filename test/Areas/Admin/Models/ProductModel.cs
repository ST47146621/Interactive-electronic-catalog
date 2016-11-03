using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Services;

namespace test.Models
{
    public class ProductModel
    {
        [Required]
        [DisplayName("商品編號")]
        [Remote("CheckProductId", "Admin", "Admin", ErrorMessage = "商品編號已存在")]
        [StringLength(10, ErrorMessage = "最多輸入10字")]
        public string ProductId { get; set; }
        [Required]
        [DisplayName("商品名稱")]
        [StringLength(50, ErrorMessage = "最多輸入50字")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("商品規格")]
        public string ProductSpec { get; set; }
        [Required]
        [DisplayName("商品描述")]
        public string ProductMemo { get; set; }
        [Required]
        [DisplayName("商品原價")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        public int? ProductCost { get; set; }
        [Required]
        [DisplayName("商品售價")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        public int? ProductPrice { get; set; }
        [DisplayName("商品圖片1")]
        [Required]
        public HttpPostedFileBase ProductImgUrl1 { get; set; }
        [DisplayName("商品圖片2")]
        public HttpPostedFileBase ProductImgUrl2 { get; set; }
        [DisplayName("商品圖片3")]
        public HttpPostedFileBase ProductImgUrl3 { get; set; }
        [DisplayName("商品圖片4")]
        public HttpPostedFileBase ProductImgUrl4 { get; set; }
        [DisplayName("放置首頁")]
        public bool PushHot { get; set; }
        [DisplayName("商品顏色")]
        public List<Color> PColor { get; set; }
        [DisplayName("商品類別")]
        public List<ProductCategory> PCategory { get; set; }
        public int[] SetColor { get; set; }
        public int[] SetCategory { get; set; }
        [DisplayName("庫存量")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        [Required]
        public int Quantity { get; set; }
        [DisplayName("狀態")]
        public bool OnShelf { get; set; }
    }
    public class ProductNoValidModel
    {
        [Required]
        [DisplayName("商品編號")]
        [Remote("CheckProductId", "Admin", "Admin", ErrorMessage = "商品編號已存在")]
        [StringLength(10, ErrorMessage = "最多輸入10字")]
        public string ProductId { get; set; }
        [Required]
        [DisplayName("商品名稱")]
        [StringLength(50, ErrorMessage = "最多輸入50字")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("商品規格")]
        public string ProductSpec { get; set; }
        [Required]
        [DisplayName("商品描述")]
        public string ProductMemo { get; set; }
        [Required]
        [DisplayName("商品原價")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        public int? ProductCost { get; set; }
        [Required]
        [DisplayName("商品售價")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        public int? ProductPrice { get; set; }
        [DisplayName("商品圖片1")]
        public HttpPostedFileBase ProductImgUrl1 { get; set; }
        [DisplayName("商品圖片2")]
        public HttpPostedFileBase ProductImgUrl2 { get; set; }
        [DisplayName("商品圖片3")]
        public HttpPostedFileBase ProductImgUrl3 { get; set; }
        [DisplayName("商品圖片4")]
        public HttpPostedFileBase ProductImgUrl4 { get; set; }
        public string ImgUrl1 { get; set; }
        public string ImgUrl2 { get; set; }
        public string ImgUrl3 { get; set; }
        public string ImgUrl4 { get; set; }
        [DisplayName("放置首頁")]
        public bool PushHot { get; set; }
        [DisplayName("商品顏色")]
        public List<ColorEditModel> PColor { get; set; }
        [DisplayName("商品類別")]
        public List<ProductCategoryEditModel> PCategory { get; set; }
        public int[] SetColor { get; set; }
        public int[] SetCategory { get; set; }
        [DisplayName("庫存量")]
        [RegularExpression("[0-9]*", ErrorMessage = "請輸入數值型態")]
        [Required]
        public int Quantity { get; set; }
        [DisplayName("狀態")]
        public bool OnShelf { get; set; }
    }
    public class ColorEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool check { get; set; }
    }
    public class ProductCategoryEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool check { get; set; }
    }
    public class ProductDetailModel : DetailViewModel
    {
        public ProductDetailModel()
        {
        }

        public ProductDetailModel(bool flag)
        {
            DisplayNameService.GetDisplayName<ProductDetailModel>(this, "ProductDetails");
            DataTable = DisplayNameService.GetDisplayName<List<GridViewModel>>("ProductDetail");
        }

    }
    public class ProductDetailGridModel
    {
        public string checkbox { get; set; }
        public string productid { get; set; }
        public string productname { get; set; }
        public string productimgurl1 { get; set; }
    }
}