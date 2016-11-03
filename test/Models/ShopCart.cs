//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShopCart
    {
        public ShopCart()
        {
            this.DriverLocation = new HashSet<DriverLocation>();
            this.ShopCartList = new HashSet<ShopCartList>();
        }
    
        public string Account { get; set; }
        public string ShopId { get; set; }
        public bool Status { get; set; }
        public int Price { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<byte> Payment { get; set; }
    
        public virtual ICollection<DriverLocation> DriverLocation { get; set; }
        public virtual Member Member { get; set; }
        public virtual PurchaseStatus PurchaseStatus { get; set; }
        public virtual ICollection<ShopCartList> ShopCartList { get; set; }
    }
}
