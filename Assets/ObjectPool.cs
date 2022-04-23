using System;
using System.Collections.Generic;

namespace IndieGame.Utility
{
    public static class ObjectPool
    {
        public static Dictionary<Type, object> PoolDictionary = new Dictionary<Type, object>();
        public static Dictionary<string, object> PoolDictionaryStr = new Dictionary<string, object>();
        public static T Get<T>()
        {
            if (PoolDictionary.ContainsKey(typeof(T)))
            {
                Stack<T> pool = PoolDictionary[typeof(T)] as Stack<T>;
                if (pool.Count > 0)
                    return pool.Pop();
            }
            return default(T);
        }
        public static void Set<T>(T obj)
        {
            if ((object)obj == null)
                return;
            object obj1;
            if(PoolDictionary.TryGetValue(typeof(T), out obj1))
            {
                (obj1 as Stack<T>).Push(obj);
            }
            else
            {
                Stack<T> ts = new Stack<T>();
                ts.Push(obj);
                PoolDictionary.Add(typeof(T), (object)ts);
            }
        }
        public static T Get<T>(string key)
        {
            if (PoolDictionaryStr.ContainsKey(key))
            {
                Queue<T> q = PoolDictionaryStr[key] as Queue<T>;
                if (q.Count > 0)
                    return q.Dequeue();
            }
            return default(T);
        }
        public static void Set<T>(string key, T obj)
        {
            if (obj == null)
                return;
            object obj1;
            if(PoolDictionaryStr.TryGetValue(key, out obj1))
            {
                (obj1 as Queue<T>).Enqueue(obj);
            }
            else
            {
                Queue<T> ts = new Queue<T>();
                ts.Enqueue(obj);
                PoolDictionaryStr.Add(key, ts);
            }
        }
        public static void Clear()
        {
            PoolDictionary.Clear();
            PoolDictionaryStr.Clear();
        }  
    }
}