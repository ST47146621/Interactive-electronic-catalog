using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Services.Generic
{
    public static class CacheExtensions
    {
        public static T GetOrStore<T>(string key, Func<T> generator) where T : class
        {
            key = key.ToUpper();

            var cache = HttpContext.Current.Cache;
            var result = cache[key] as T;
            if (result == null)
            {
                result = generator();
                cache.Add(key, result, null, System.Web.Caching.Cache.NoAbsoluteExpiration
                   , TimeSpan.FromHours(2)
                   , System.Web.Caching.CacheItemPriority.High, null);
            }
            return result;
        }

        public static void Update<T>(string key, T TEntity)
        {
            var cache = HttpContext.Current.Cache;
            key = key.ToUpper();
            cache.Insert(key, TEntity);
        }

        public static void ClearCache(string key)
        {
            key = key.ToUpper();
            HttpContext.Current.Cache.Remove(key);
        }

        public static void ClearAllCache()
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

    }
}