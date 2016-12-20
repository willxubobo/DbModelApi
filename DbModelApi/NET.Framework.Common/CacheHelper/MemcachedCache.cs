using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace NET.Framework.Common.CacheHelper
{
    /// <summary>
    ///     Memcached is a high-performance, distributed memory caching system originally by Danga Interactive for LiveJournal.
    ///     It is widely used by many web sites and applications.
    /// </summary>
    public class MemcachedCache
    {
        private const string NullKeyNotAllowed = "null key not allowed";
        private static TimeSpan _expiration = new TimeSpan(10, 0, 0, 0, 0);
        private static MemcachedClient _mc=new MemcachedClient();

        //public MemcachedCache()
        //{
        //    _mc = new MemcachedClient();
        //}

        /// <summary>
        ///     Removes all data from the cache.
        /// </summary>
        public static void Clear()
        {
            _mc.FlushAll();
        }

        /// <summary>
        ///     Retrieves the specified item from the cache.
        /// </summary>
        /// <typeparam name="T">Dependency object to be find child from.</typeparam>
        /// <param name="key">The identifier for the item to retrieve.</param>
        /// <returns>The retrieved item, or
        ///     <value>null</value>
        ///     if the key was not found.
        /// </returns>
        public static T Get<T>(string key) where T : class
        {
            CheckParamKey(key);

            object value = _mc.Get(key);
            
            return value as T;
        }


        ///// <summary>
        /////     Retrieves the specified item from the cache.
        ///// </summary>
        ///// <param name="key">The identifier for the item to retrieve.</param>
        ///// <returns>The retrieved item.</returns>
        //public object Get(string key)
        //{
        //    return Get<object>(key);
        //}

        /// <summary>
        ///     Inserts an item into the cache with a cache key to reference its location.
        /// </summary>
        /// <typeparam name="T">Dependency object to be find child from.</typeparam>
        /// <param name="key">The key used to reference the item.</param>
        /// <param name="value">The object to be inserted into the cache.</param>
        public static void Put<T>(string key, T value) where T : class
        {
            Put(key, value, _expiration);
        }

        ///// <summary>
        /////     Inserts an item into the cache with a cache key to reference its location.
        ///// </summary>
        ///// <param name="key">The key used to reference the item.</param>
        ///// <param name="value">The object to be inserted into the cache.</param>
        //public void Put(string key, object value)
        //{
        //    Put<object>(key, value, _expiration);
        //}

        /// <summary>
        ///     Inserts an item into the cache with a cache key to reference its location.
        /// </summary>
        /// <typeparam name="T">Dependency object to be find child from.</typeparam>
        /// <param name="key">The key used to reference the item.</param>
        /// <param name="value">The object to be inserted into the cache.</param>
        /// <param name="expiration">The expiration time of the cached object</param>
        public static void Put<T>(string key, T value, TimeSpan expiration) where T : class
        {
            CheckParamKey(key);
            CheckParamValue(value);

            _mc.Store(StoreMode.Set, key, value, expiration);
        }

        ///// <summary>
        /////     Inserts an item into the cache with a cache key and expiration.
        ///// </summary>
        ///// <param name="key">The key used to reference the item.</param>
        ///// <param name="value">The object to be inserted into the cache.</param>
        ///// <param name="expiration">The expiration time of the cached object</param>
        //public void Put(string key, object value, TimeSpan expiration)
        //{
        //    Put<object>(key, value, expiration);
        //}

        /// <summary>
        ///     Removes the specified item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the item to delete</param>
        public static void Remove(string key)
        {
            CheckParamKey(key);
            _mc.Remove(key);
        }

        ///// <summary>
        /////     Update the expiration time.
        ///// </summary>
        ///// <param name="key">The key for the cached object</param>
        //public void Renew(string key)
        //{
        //    Renew(key, _expiration);
        //}

        ///// <summary>
        /////     Update the expiration time.
        ///// </summary>
        ///// <param name="key">The key for the cached object</param>
        ///// <param name="expiration">The expiration time of the cached object</param>
        //public void Renew(string key, TimeSpan expiration)
        //{
        //    CheckParamKey(key);

        //    object value = _mc.Get(key);
        //    if (null != value)
        //    {
        //        _mc.Store(StoreMode.Set, key, value, expiration);
        //    }
        //}

        protected static void CheckParamKey(string key)
        {
            if (key == null || "".Equals(key))
            {
                throw new ArgumentNullException("key", NullKeyNotAllowed);
            }
        }

        protected static void CheckParamValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", NullKeyNotAllowed);
            }
        }
    }
}