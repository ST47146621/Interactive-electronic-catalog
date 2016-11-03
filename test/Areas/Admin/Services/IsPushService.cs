using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Areas.Admin.Services
{
    public class IsPushService
    {
        DBEntity db = new DBEntity();
        public List<string[]> GetIsPush()
        {
            List<string[]> result = new List<string[]>();
            result.Add(new string[] { "熱門商品推薦", "相關商品熱門推薦", "使用者關聯推薦", "使用者分群推薦" });
            var hot = db.Repository<ShopCartList>().Get(p => p.ShopCart.Status == true && p.IsPush == 1).ToList();
            int counthot = 0;
            for (var i = 0; i < hot.Count; i++)
            {
                counthot += hot[i].Quantity;
            }
            var typehot = db.Repository<ShopCartList>().Get(p => p.ShopCart.Status == true && p.IsPush == 2).ToList();
            int counttypehot = 0;
            for (var i = 0; i < typehot.Count; i++)
            {
                counttypehot += typehot[i].Quantity;
            }
            var relation = db.Repository<ShopCartList>().Get(p => p.ShopCart.Status == true && p.IsPush == 3).ToList();
            int countrelation = 0;
            for (var i = 0; i < relation.Count; i++)
            {
                countrelation += relation[i].Quantity;
            }
            var field = db.Repository<ShopCartList>().Get(p => p.ShopCart.Status == true && p.IsPush == 4).ToList();
            int countfield = 0;
            for (var i = 0; i < field.Count; i++)
            {
                countfield += relation[i].Quantity;
            }
            result.Add(new string[] { counthot.ToString(), counttypehot.ToString(), countrelation.ToString(), countfield.ToString() });
            return result;
        }
    }
}