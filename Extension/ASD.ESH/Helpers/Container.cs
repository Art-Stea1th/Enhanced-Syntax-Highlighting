using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ASD.ESH.Helpers {
    
    internal static class Container { // Simple thresd-safe Singleton-IoC Container

        private static ConcurrentDictionary<Type, object> instances = new ConcurrentDictionary<Type, object>();

        public static void Register<TToResolve>() where TToResolve : class, new()
            => instances.GetOrAdd(typeof(TToResolve), new TToResolve());

        public static void Register<TToResolve, TConcrete>() where TConcrete : class, TToResolve, new()
            => instances.GetOrAdd(typeof(TToResolve), new TConcrete());

        public static void Register<TToResolve>(TToResolve instance) where TToResolve : class
            => instances.GetOrAdd(typeof(TToResolve), instance ?? throw new ArgumentNullException(nameof(instance)));

        public static void Register<TToResolve>(Func<TToResolve> factory) where TToResolve : class
            => Register<TToResolve, TToResolve>(factory);

        public static void Register<TToResolve, TConcrete>(Func<TConcrete> factory) where TConcrete : class, TToResolve
            => instances.GetOrAdd(typeof(TToResolve), o => factory()
            ?? throw new InvalidOperationException($"'{nameof(factory)}' should not return null."));

        public static TToResolve Resolve<TToResolve>() where TToResolve : class {
            try {
                return ((TToResolve)instances.AsEnumerable().Single(p => p.Key == typeof(TToResolve)).Value);
            }
            catch (InvalidOperationException innerInvalidOperationException) {
                throw new InvalidOperationException(
                    $"Type '{typeof(TToResolve).Name}' has not been registered in the {nameof(Container)}.",
                    innerInvalidOperationException);
            }
        }
    }
}