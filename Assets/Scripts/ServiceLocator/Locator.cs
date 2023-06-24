using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    public class Locator
    {
        private static Locator _inctance;

        public static Locator Inctance => _inctance ??= new Locator();
        
        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();
        
        public void Register<TService>(object implementation) where TService : class, IService => 
            _dictionary.Add(typeof(TService), implementation);

        public void Unregister<TService>() where TService : class, IService => 
            _dictionary.Remove(typeof(TService));

        public TService GetService<TService>() where TService : class, IService
        {
            if (_dictionary.TryGetValue(typeof(TService), out var value))
                return (value as TService);
            
            return default(TService);
        }
    }
}