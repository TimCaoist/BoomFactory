using BoomFactory.Model;
using System.Linq;

namespace BoomFactory.Fetcher
{
    public class CompentFetch : BaseFetch
    {
        private static readonly CompentFetch self = new CompentFetch();

        public static CompentFetch Instance => self;

        internal override TService Resolve<TService>(MetaData metaData, object[] filter, object[] constructorArgs, FactoryConfig factoryConfig)
        {
            return CreateInstance<TService>(metaData, metaData.SubTypes.First(), constructorArgs, factoryConfig);
        }
    }
}
