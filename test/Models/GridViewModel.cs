using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class GridViewModel
    {
        public string keyId { get; set; }
        public string keyName { get; set; }
        public string chinese { get; set; }
        public int index { get; set; }
        public int width { get; set; }
        public string type { get; set; }
        public string pattern { get; set; }
        public bool display { get; set; }
        public bool isReadonly { get; set; }
        public bool isRequired { get; set; }
        public string defaultValue { get; set; }
    }
}