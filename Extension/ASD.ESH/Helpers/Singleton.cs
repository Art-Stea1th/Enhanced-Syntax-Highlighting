using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ASD.ESH.Helpers {

    internal static class Singleton<T> where T : new() {

        private static ConcurrentDictionary<Type, T> instances = new ConcurrentDictionary<Type, T>();
        public static T Instance => instances.GetOrAdd(typeof(T), (t) => new T());

    }

    internal sealed class SingleItemEnumerable<T> : IEnumerable<T> {

        private readonly T value;
        public SingleItemEnumerable(T value) => this.value = value;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator() => new SingletonItemEnumerator(value);

        private class SingletonItemEnumerator : IEnumerator<T> {

            private bool hasNext = true;

            object IEnumerator.Current => Current;
            public T Current { get; }
            public SingletonItemEnumerator(T value) => Current = value;

            public bool MoveNext() {
                var result = hasNext;
                hasNext = false;
                return result;
            }
            public void Reset() { }
            public void Dispose() { }
        }
    }    
}