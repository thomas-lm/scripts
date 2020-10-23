using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace thomaslm.mock.IMemoryCacheMock
{
    class CacheEntryMock : Mock<ICacheEntry>
    {
        delegate void SetValueCallback(object value);

        public CacheEntryMock(string key, Dictionary<string, object> cache)
        {
            SetupSet(e => e.Value = It.IsAny<object>())
                .Callback(new SetValueCallback((value) => cache.Add(key, value)));
        }
    }
}
