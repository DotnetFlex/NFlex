using System;
using System.Collections.Generic;

namespace NFlex
{
    public sealed class Dictionary<TKey1,TKey2,TValue>:Dictionary<Tuple<TKey1,TKey2>,TValue>,IDictionary<Tuple<TKey1, TKey2>, TValue>
    {
        public TValue this[TKey1 key1,TKey2 key2]
        {
            get
            {
                var key = Tuple.Create<TKey1, TKey2>(key1, key2);
                if (ContainsKey(key))
                    return base[key];
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                var key = Tuple.Create(key1, key2);
                if (!ContainsKey(key))
                    Add(key, value);
                else
                    base[Tuple.Create(key1, key2)] = value;
            }
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            Add(Tuple.Create(key1, key2), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            return ContainsKey(Tuple.Create(key1, key2));
        }
    }
}
