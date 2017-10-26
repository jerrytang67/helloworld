using System;
using System.Web;
using System.Web.Caching;

namespace pubg_database.Services.Cache
{
    public class WebCacheProvider 
    {
        private static readonly System.Web.Caching.Cache Cache;
        private const int NormalCacheTime = 10000;

        static WebCacheProvider()
        {
            if (Cache == null)
            {
                Cache = HttpContext.Current.Cache;
            }
        }

        private static void Add(string key, object value, int cacheSecond, CacheDependency dependency,
            CacheItemRemovedCallback callback)
        {
            Cache.Insert(key, value, null, DateTime.Now.AddSeconds(cacheSecond), TimeSpan.Zero, CacheItemPriority.High,
                callback);
        }

        public static void Add(string key, object value)
        {
            Add(key, value, NormalCacheTime);
        }

        public static void Add(string key, object value, int cacheSecond)
        {
            Add(key, value, cacheSecond, null, null);
        }

        public static object Get(string key)
        {
            return Cache.Get(key);
        }

        public static T Get<T>(string key) where T : class
        {
            var obj = Get(key);
            if (obj == null)
                return default(T);
            return (obj as T);
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}