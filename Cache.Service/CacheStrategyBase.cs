using System;
using System.Collections;
using System.Collections.Generic;

namespace Cache.Service
{
    public abstract class CacheStrategyBase : IEnumerable<KeyValuePair<string, object>>
    {
        public abstract string StrategyName { get; }
        public abstract int Count { get; }
        public abstract T GetOrSet<T>(string key, Func<T> data);
        public abstract ICollection<string> Keys { get; }
        public abstract void Set<T>(string key, T value, TimeSpan? expiration = null);
        public abstract T Get<T>(string key);
        public abstract bool Contains(string key);
        public abstract bool Remove(string key);
        public abstract void Clear();
        public abstract IEnumerator<KeyValuePair<string, object>> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
}