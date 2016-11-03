using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class TableModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string Name { get; set; }
        [Required]
        [DisplayName("留言")]
        public string Message { get;set; }
    }
    
    
    
    public class BigModel
    {
        public List<ProductModel> a { get; set; }
        public List<ShopCartModel> b { get; set; }
        public List<MemberModel> c {get;set;}

    }
}