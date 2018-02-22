using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NewXzc.Common
{
    class Cache
    {
        /// <summary>
        /// 缓存配置文件
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            objCache.Insert(CacheKey, objObject);
        }
    }
}
