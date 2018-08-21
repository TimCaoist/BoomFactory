using BoomFactory.Model;

namespace BoomFactory.Fetcher
{
    /// <summary>
    /// 默认的查找类型类，抽象工厂查找
    /// </summary>
    public class DefaultFetch : BaseFetch
    {
        private static readonly DefaultFetch self = new DefaultFetch();

        public static DefaultFetch Instance => self;

        internal override TService Resolve<TService>(MetaData metaData, object[] filters, object[] constructorArgs, FactoryConfig factoryConfig)
        {
            //根据接口元数据查找匹配的子类类型配置
            var config = GetMatchSubType(metaData, filters, factoryConfig);
            if (config == null)
            {
                return default(TService);
            }

            
            var args = GetAjustConstructorArgs(config, constructorArgs);
            return CreateInstance<TService>(metaData, config.InstanceType, args, factoryConfig, config.Single);
        }

        /// <summary>
        /// 根据子类ConstructorAttribute的设置矫正构造函数的设置
        /// </summary>
        /// <param name="config">子类类型配置</param>
        /// <param name="constructorArgs">构造函数参数</param>
        /// <returns>矫正后的结果</returns>
        private object[] GetAjustConstructorArgs(ClassConfig config, object[] constructorArgs)
        {
            if (config.ConstructorArgsDefine == false)
            {
                return constructorArgs;
            }

            var argsIndex = config.ConstructorArgsIndex;
            if (argsIndex == null || argsIndex.Length == 0)
            {
                return null;
            }

            var len = argsIndex.Length;
            var cArgs = new object[len];
            for (var i = 0; i < len; i++)
            {
                cArgs[i] = constructorArgs[i];
            }

            return cArgs;
        }

        /// <summary>
        /// 根据元数据以及过滤数据规则配置查找出匹配的子类
        /// </summary>
        /// <param name="metaData">元数据</param>
        /// <param name="filters">过滤数据</param>
        /// <param name="factoryConfig">容器配置</param>
        /// <returns></returns>
        private ClassConfig GetMatchSubType(MetaData metaData, object[] filters, FactoryConfig factoryConfig)
        {
            ClassConfig defaultConfig = null;
            foreach (var config in metaData.Configs)
            {
                if (config.Value.IsMatch(filters, factoryConfig))
                {
                    return config.Value;
                }

                if (config.Value.IsDefault)
                {
                    defaultConfig = config.Value;
                }
            }

            //没有匹配的情况下如果有默认的就用默认的
            return defaultConfig;
        }
    }
}
