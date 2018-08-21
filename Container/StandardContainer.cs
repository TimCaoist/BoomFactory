using BoomFactory.Loader;
using BoomFactory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomFactory.Container
{
    public class StandardContainer : FactoryContainer
    {
        private readonly static BaseLoader baseLoader = new StandLoader();

        private static readonly StandardContainer self = new StandardContainer();

        public static StandardContainer Instance => self;

        public StandardContainer() : base()
        {
        }

        public StandardContainer(FactoryConfig factoryConfig) : base(factoryConfig)
        {
        }

        protected override MetaData GetMetaData(Type type)
        {
            if (baseLoader.LoadComplete == false)
            {
                Task.WaitAny(baseLoader.Task);
            }

            return baseLoader.GetMetaData(type);
        }
    }
}
