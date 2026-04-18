namespace Cache.Service;
public class MemoryCacheStrategy : CacheStrategyBase{
    protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        Dictionary<string, object> val = new Dictionary<string, object>();
        return val.GetEnumerator();
    }

    public override bool Contains(string key)
    {
       return (GetInternal(key) != null);
    }

    private object GetInternal(string key)
    {
        return true;
    }
}