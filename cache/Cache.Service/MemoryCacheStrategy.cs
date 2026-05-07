using System.Collections.Concurrent;

namespace Cache.Service;
public class MemoryCacheStrategy : CacheStrategyBase
{
    private readonly ConcurrentDictionary<string, CacheEntry<ICacheable>> _store = new();
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

    public override string StrategyName => "In-Memory Storage";

    public override int Count => _store.Count(x => !x.Value.IsExpired);

    // Cache entry
    private class CacheEntry<T> where T : ICacheable
    {
        public T Value { get; set; }
        public DateTime ExpirationTime { get; set; }
        
        public bool IsExpired => DateTime.UtcNow > ExpirationTime;
        public CacheEntry(T value, DateTime duration)
        {
            Value = value;
            ExpirationTime = duration;
        }
    }


    public override ICollection<string> Keys => _store
        .Where(x => !x.Value.IsExpired)
        .Select(x => x.Key)
        .ToList();

    public override T GetOrSet<T>(string key, Func<T> data)
    {
        if (Contains(key))
        {
            return Get<T>(key);
        }
        T dataSource = data();
        Set<T>(key, dataSource);
        return dataSource;

    }
    public override void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var expiry = DateTime.UtcNow.Add(expiration ?? _defaultExpiration);
        
        var entry = new CacheEntry<ICacheable>(value,expiry);
    
        _store[key] = entry;
    }

    public override T Get<T>(string key)
    {
        if (_store.TryGetValue(key, out var entry))
        {
            if (!entry.IsExpired)
            {
                return (T)entry.Value;
            }
            _store.TryRemove(key, out _);
        }

        return default;
    }

    public override bool Contains(string key)
    {
        if (!_store.TryGetValue(key, out var entry)) return false;
        
        if (entry.IsExpired)
        {
            Remove(key);
            return false;
        }

        return true;
    }

    public override bool Remove(string key)
    {
        return _store.TryRemove(key, out _);
    }

    public override void Clear()
    {
        _store.Clear();
    }

    public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        return _store
            .Where(x => !x.Value.IsExpired)
            .Select(x => new KeyValuePair<string, object>(x.Key, x.Value.Value))
            .GetEnumerator();
    }
}