using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using test.Models;

namespace test.Services
{
    public static class DisplayNameService
    {
        public static TEntity GetDisplayName<TEntity>(string tableName) where TEntity : new()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();

            int language = 1;
            var str = unitOfWork.Repository<AllViewsColumn>()
                .Get(p => p.TableName == tableName && p.Language == language)
                .Select(p => p.SerialString).FirstOrDefault();

            if (str == null)
                return new TEntity();

            try
            {
                return JsonConvert.DeserializeObject<TEntity>(str);
            }
            catch
            {
                return new TEntity();
            }

        }

        public static void GetDisplayName<TEntity>(TEntity entity, string tableName)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();

            int language = 1;
            var str = unitOfWork.Repository<AllViewsColumn>()
                .Get(p => p.TableName == tableName && p.Language == language)
                .Select(p => p.SerialString).FirstOrDefault();

            if (String.IsNullOrEmpty(str))
            {
                return;
            }
            else
            {
                SetDisplayName<TEntity>(entity, JsonConvert.DeserializeObject<Dictionary<string, string>>(str));
            }

        }

        private static void SetDisplayName<TEntity>(TEntity entity, Dictionary<string, string> displayDictionary)
        {
            foreach (var item in displayDictionary)
            {
                string propertyName = item.Key;
                string propertyValue = item.Value;

                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(entity.GetType())[propertyName];

                if (descriptor == null)
                    continue;

                DisplayNameAttribute attribute = (DisplayNameAttribute)descriptor.Attributes[typeof(DisplayNameAttribute)];

                FieldInfo fieldToChange = attribute.GetType()
                    .GetField("_displayName", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);

                if (fieldToChange != null)
                    fieldToChange.SetValue(attribute, propertyValue);
            }
        }
    }
}