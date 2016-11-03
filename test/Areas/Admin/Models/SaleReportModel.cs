using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services;

namespace test.Models
{
    public class SaleReportModel : DetailViewModel
    {
        public SaleReportModel()
        {
        }

        public SaleReportModel(bool flag)
        {
            DisplayNameService.GetDisplayName<SaleReportModel>(this, "SaleReports");
            DataTable = DisplayNameService.GetDisplayName<List<GridViewModel>>("SaleReport");
        }

    }
    public class SaleReportGridModel
    {
        public string shopid { get; set; }
        public string total { get; set; }
        public string payment { get; set; }
        public string income { get; set; }
        public string cost { get; set; }
        public string final { get; set; }
    }
}