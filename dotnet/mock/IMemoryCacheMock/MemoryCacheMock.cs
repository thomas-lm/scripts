using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace thomaslm.mock.IMemoryCacheMock
{
    class MemoryCacheMock : Mock<IMemoryCache>
    {
        private Dictionary<string, object> _cache;

        delegate void TryGetValueCallback(object key, ref object value);
        delegate bool TryGetValueReturns(object key, ref object value);
        delegate void RemoveCallback(object key);
        delegate ICacheEntry CreateEntryReturns(object key);

        public MemoryCacheMock() : base()
        {
            _cache = new Dictionary<string, object>();
            Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
                .Callback(new TryGetValueCallback((object key, ref object value) =>
                {
                    value = _cache.GetValueOrDefault(key.ToString());
                }))
                .Returns(new TryGetValueReturns((object key, ref object value) => _cache.ContainsKey(key.ToString())));

            Setup(c => c.CreateEntry(It.IsAny<object>()))
                .Returns(new CreateEntryReturns((k) => new CacheEntryMock(k.ToString(), _cache).Object));

            Setup(c => c.Remove(It.IsAny<object>()))
                .Callback(new RemoveCallback((object key) => _cache.Remove(key.ToString())));
        }


        /// <summary>
        /// Test if key is present in cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean VerifyContain(string key)
        {
            return _cache.ContainsKey(key);
        }

        /// <summary>
        /// Count the entries in cache
        /// </summary>
        /// <returns></returns>
        public int VerifyCount()
        {
            return _cache.Count;
        }


       
    }
}
