using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Services.Generic
{
    public abstract class DetailViewService
    {
        public IEnumerable<SelectListItem> GetSignListItem()
        {
            return new List<SelectListItem> { 
                new SelectListItem { Text = "=", Value = "0" },
                new SelectListItem { Text = "LIKE", Value = "1" },
                new SelectListItem { Text = ">", Value = "2" },
                new SelectListItem { Text = ">=", Value = "3" },
                new SelectListItem { Text = "<", Value = "4" },
                new SelectListItem { Text = "<=", Value = "5" },
                new SelectListItem { Text = "<>", Value = "6" }
            };
        }

        public IEnumerable<SelectListItem> GetConditionListItem()
        {
            return new List<SelectListItem> { 
                new SelectListItem { Text = "OR", Value = "0" },
                new SelectListItem { Text = "AND", Value = "1" }
            };
        }
    }
}