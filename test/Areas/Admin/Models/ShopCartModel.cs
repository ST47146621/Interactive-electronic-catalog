using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class ShopCartModel
    {
        [Required]
        [DisplayName("帳號")]
        public string Account { get; set; }
        public string ShopId { get; set; }
        public bool? Status { get; set; }
        public int? Price { get; set; }
        public DateTime? Date { get; set; }
        public int? Payment { get; set; }

    }
    public class ShopCartDetailModel
    {
        [DisplayName("購物單單號")]
        public string ShopId { get; set; }
        [DisplayName("帳號")]
        public string Account { get; set; }
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("狀態")]
        public bool? Status { get; set; }
        [DisplayName("總價")]
        public int? Price { get; set; }
        [DisplayName("結帳日期")]
        public DateTime? Date { get; set; }
        [DisplayName("結帳方式")]
        public string Payment { get; set; }


    }
    public class ShopStatus
    {
        public string ShopId { get; set; }
        public bool Status { get; set; }

    }
    public class ShopListId
    {
        public string ShopId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }

    }
    public class ShopHistoryModel
    {
        public string ShopId { get; set; }
        public int? Price { get; set; }
        public string Date { get; set; }
        public string Payment { get; set; }

        public Boolean Status { get; set; }

        public string PayStatus { get; set; }
        public List<ShopHistoryItemModel> item { get; set; }
    }
    public class ShopHistoryItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

}