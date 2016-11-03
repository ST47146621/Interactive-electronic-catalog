using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class DBEntity : UnitOfWork
    {
        public DBEntity() :
            base(new test.Models.ARealShopEntities())
        {
        }

        public DBEntity(DbContext context) :
            base(context)
        {

        }
    }
}