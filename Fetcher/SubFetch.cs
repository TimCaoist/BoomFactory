using BoomFactory.Model;
using System;

namespace BoomFactory.Fetcher
{
    public class SubFetch : BaseFetch
    {
        private static readonly SubFetch self = new SubFetch();

        public static SubFetch Instance => self;

        internal override TService Resolve<TService>(MetaData metaData, object[] filter, object[] constructorArgs, FactoryConfig factoryConfig)
        {
            //根据接口元数据查找匹配的子类类型配置
            var type = typeof(TService);
            var config = GetMatchSubType(metaData, type, factoryConfig);
            if (config == null)
            {
                return default(TService);
            }

            var args = GetAjustConstructorArgs(config, constructorArgs);
            return CreateInstance<TService>(metaData, config.InstanceType, args, factoryConfig, config.Single);
        }

        private ClassConfig GetMatchSubType(MetaData metaData, Type subType, FactoryConfig factoryConfig)
        {
            foreach (var config in metaData.Configs)
            {
                if (config.Key == subType)
                {
                    return config.Value;
                }
            }

            return null;
        }
    }
}
