using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class PurchaseReturnModel
    {
        public bool Status { get; set; }
        public bool Exception { get; set; }
        public List<ProductParchaseStatus> ProductStatus { get; set; }
    }
    public class ProductParchaseStatus
    {
        public string ProductName { get; set; }
        public string ProductStatus { get; set; }
    }
}