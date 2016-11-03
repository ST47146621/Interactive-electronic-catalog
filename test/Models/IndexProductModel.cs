using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class IndexProductModel
    {
        public List<SectionDetailItem> PushHotItem { get; set; }
        public List<ProductCategoryModel> Special { get; set; }
        public List<ShowNews> ShowNews { get; set; }
        public int CountNews { get; set; }

    }
    public class ShowNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
    }
    public class ProductCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public List<CategoryItem> item { get; set; }
        public List<CategoryItem> onsaleitem { get; set; }
    }
    public class CategoryItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int? OrgPrice { get; set; }
        public string ProductImgUrl { get; set; }
        public string ProductMemo { get; set; }
        public double Star { get; set; }
    }
}