using System.Collections;

namespace Cache.Service
{
    public abstract class CacheStrategyBase : IEnumerable<KeyValuePair<string, object>>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract IEnumerator<KeyValuePair<string, object>> GetEnumerator();

        /// <summary>
        /// Check if key contain some value
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns>True if the key contains value</returns>
        public abstract bool Contains(string key);
    }
}