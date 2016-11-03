using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class DetailRangeModel
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        public int Draw { get; set; }

        public String SortData { get; set; }

        public String Sort { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        [DisplayName("Filter1")]
        public String Filter1 { get; set; }

        [DisplayName("Filter2")]
        public String Filter2 { get; set; }

        [DisplayName("Filter3")]
        public String Filter3 { get; set; }

        [DisplayName("Filter4")]
        public String Filter4 { get; set; }

        [DisplayName("Filter5")]
        public String Filter5 { get; set; }

        [DisplayName("Start1")]
        public String Start1 { get; set; }

        [DisplayName("Start2")]
        public String Start2 { get; set; }

        [DisplayName("Start3")]
        public String Start3 { get; set; }

        [DisplayName("Start4")]
        public String Start4 { get; set; }

        [DisplayName("Start5")]
        public String Start5 { get; set; }

        [DisplayName("Start6")]
        public String Start6 { get; set; }

        [DisplayName("Start7")]
        public String Start7 { get; set; }

        [DisplayName("End1")]
        public String End1 { get; set; }

        [DisplayName("End2")]
        public String End2 { get; set; }

        [DisplayName("End3")]
        public String End3 { get; set; }

        [DisplayName("End4")]
        public String End4 { get; set; }

        [DisplayName("End5")]
        public String End5 { get; set; }

        [DisplayName("End6")]
        public String End6 { get; set; }

        [DisplayName("End7")]
        public String End7 { get; set; }

        public List<GridViewModel> DataTable { get; set; }
    }
}