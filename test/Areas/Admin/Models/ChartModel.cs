﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class GetProductNameModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class GetHotModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}