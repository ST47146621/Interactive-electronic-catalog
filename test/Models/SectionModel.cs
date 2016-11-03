using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class SectionModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductMemo1 { get; set; }
        public string ProductImgUrl1 { get; set; }
        public string ProductImgUrl2 { get; set; }
        public string ProductImgUrl3 { get; set; }
        public string ProductImgUrl4 { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public List<string> Sharp { get; set; }
    }
    public class HistoryModel
    {
        public string ShopId { get; set; }
        public int? Price { get; set; }
        public string Date { get; set; }
        public string Payment { get; set; }
        public List<HistoryItemModel> item { get; set; }
    }
    public class HistoryItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
    public class SectionDetailModel
    {
        public double Star { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSpec { get; set; }
        public int ProductPrice { get; set; }
        public string ProductMemo { get; set; }
        public int? OrgPrice { get; set; }
        public string Time { get; set; }
        public string ProductImgUrl1 { get; set; }
        public string ProductImgUrl2 { get; set; }
        public string ProductImgUrl3 { get; set; }
        public string ProductImgUrl4 { get; set; }
        public List<CategoryTable> ProductCategory { get; set; }
        public List<SectionDetailItem> categoryitem { get; set; }
        public List<SectionDetailItem> detailitem { get; set; }
        public List<SectionDetailItem> matchitem { get; set; }
        public List<string> Sharp { get; set; }
        public string UserStatus { get; set; }
        public List<ReplyModel> replyitem { get; set; }
    }
    public class ReplyModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public int? RId { get; set; }
        public string ProductId { get; set; }
    }
    public class SectionDetailItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImgUrl { get; set; }
        public double Star { get; set; }

    }
    public class CountProduct
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class NewsPageModel
    {
        public List<ShowNews> ShowNews { get; set; }
        public int CountNews { get; set; }
    }
    public class NewsDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string ImageUrl { get; set; }
    }
}