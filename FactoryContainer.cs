using BoomFactory.Fetcher;
using BoomFactory.Model;
using System;
using System.Collections.Generic;

namespace BoomFactory
{
    public abstract class FactoryContainer
    {
        private readonly static FactoryConfig defaultConfig;

        private FactoryConfig factoryConfig;

        static FactoryContainer()
        {
            defaultConfig = new FactoryConfig();
        }

        public FactoryContainer()
        {
            this.factoryConfig = defaultConfig;
        }

        public FactoryContainer(FactoryConfig factoryConfig)
        {
            this.factoryConfig = factoryConfig;
        }

        public TService Resolve<TService>()
        {
            return Resolve<TService>(null, null);
        }

        public TService Resolve<TService>(Dictionary<string, object> dictionary)
        {
            return Resolve<TService>(new object[] { dictionary }, null);
        }

        public TService Resolve<TService>(object filter)
        {
            return Resolve<TService>(new object[] { filter});
        }

        public TService Resolve<TService>(object filter, params object[] constructorArgs)
        {
            return Resolve<TService>(new object[] { filter }, constructorArgs);
        }

        public TService Resolve<TService>(object[] filters)
        {
            return Resolve<TService>(filters, null);
        }

        public TService Resolve<TService>(object[] filter, object[] constructorArgs)
        {
            var type = typeof(TService);
            var metaData = GetMetaData(type);
            var fetch = FetchFactory.Create(metaData);
            return fetch.Resolve<TService>(metaData, filter, constructorArgs, factoryConfig);
        }

        public TSubType ResolveBySubType<TService, TSubType>(object[] constructorArgs)
        {
            var type = typeof(TService);
            var metaData = GetMetaData(type);
            var fetch = FetchFactory.Create(metaData);
            return fetch.Resolve<TSubType>(metaData, null, constructorArgs, factoryConfig);
        }

        protected abstract MetaData GetMetaData(Type type);
    }
}
