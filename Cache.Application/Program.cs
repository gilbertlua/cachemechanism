using Cache.Service;


CacheStrategyBase _cache = new MemoryCacheStrategy();
LargeData _dataManager = new LargeData();

_cache.GetOrSet("1",() => _dataManager.GetLargeData("1"));
Console.WriteLine("First data completed");
_cache.GetOrSet("2",() => _dataManager.GetLargeData("1"));
Console.WriteLine("Second data completed");

_cache.GetOrSet("1",() => _dataManager.GetLargeData("1"));
Console.WriteLine("First data completed");
_cache.GetOrSet("2",() => _dataManager.GetLargeData("1"));
Console.WriteLine("Second data completed");

