using BoomFactory.Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BoomFactory.Fetcher
{
    public abstract class BaseFetch
    {
        internal abstract TService Resolve<TService>(MetaData metaData, object[] filter, object[] constructorArgs, FactoryConfig factoryConfig);

        private readonly static Dictionary<Type, object> cacehObjects = new Dictionary<Type, object>();

        /// <summary>
        /// 根据元数据和类型构造参数和配置创造实例
        /// </summary>
        /// <typeparam name="TService">原始类型</typeparam>
        /// <param name="metaData">元数据</param>
        /// <param name="type">需要创建的类型</param>
        /// <param name="constructorArgs">构造参数</param>
        /// <param name="factoryConfig">容器配置</param>
        /// <param name="settingSingle">是否设置为单例</param>
        /// <returns>TService类型</returns>
        protected TService CreateInstance<TService>(MetaData metaData, Type type, object[] constructorArgs, FactoryConfig factoryConfig, bool settingSingle = true)
        {
            if (type == null)
            {
                return default(TService);
            }

            //如果有缓存直接从缓存拿取
            object obj;
            if (constructorArgs == null && cacehObjects.TryGetValue(type, out obj))
            {
                return (TService)obj;
            }

            obj = Activator.CreateInstance(type, constructorArgs);
            return TryStoreSingleObject<TService>(metaData, type, obj, constructorArgs, factoryConfig, settingSingle);
        }

        private TService TryStoreSingleObject<TService>(MetaData metaData, Type type, object target, object[] constructorArgs, FactoryConfig factoryConfig, bool settingSingle)
        {
            if (constructorArgs != null || factoryConfig.CompentMode == CompentMode.PerRequest)
            {
                return (TService)target;
            }

            if (factoryConfig.CompentMode == CompentMode.BySetting && settingSingle == false)
            {
                return (TService)target;
            }

            lock (cacehObjects)
            {
                object obj;
                if (cacehObjects.TryGetValue(type, out obj))
                {
                    return (TService)obj;
                }
                else {
                    cacehObjects.Add(type, target);
                }
            }
            
            return (TService)target;
        }
    }
}
