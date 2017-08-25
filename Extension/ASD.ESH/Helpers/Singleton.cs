using System;
using System.Collections.Concurrent;

namespace ASD.ESH.Helpers {

    internal static class Singleton<T> where T : new() {

        private static ConcurrentDictionary<Type, T> instances = new ConcurrentDictionary<Type, T>();
        public static T Instance => instances.GetOrAdd(typeof(T), (t) => new T());

    }
}