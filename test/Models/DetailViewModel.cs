using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class DetailViewModel
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        public int Draw { get; set; }

        public string SortData { get; set; }

        public string Sort { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        [DisplayName("Filter1")]
        public string Filter1 { get; set; }

        [DisplayName("Filter2")]
        public string Filter2 { get; set; }

        [DisplayName("Filter3")]
        public string Filter3 { get; set; }

        public byte Select1 { get; set; }

        public byte Select2 { get; set; }

        public byte Select3 { get; set; }

        public byte Sign1 { get; set; }

        public byte Sign2 { get; set; }

        public byte Sign3 { get; set; }

        public byte Condition1 { get; set; }

        public byte Condition2 { get; set; }

        public List<GridViewModel> DataTable { get; set; }
    }
}