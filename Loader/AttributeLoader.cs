using BoomFactory.Model.FAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Loader
{
    public class AttributeLoader
    {
        private static readonly AttributeLoader self = new AttributeLoader();

        private readonly static Type factoryType = typeof(FactoryAttribute);

        public static AttributeLoader Instance => self;

        public IEnumerable<FactoryAttribute> GetFactoryAttributes(Type type)
        {
            var attributes = type.GetCustomAttributes(false);
            return attributes.Where(a => a.GetType().IsSubclassOf(factoryType)).Select(a => (FactoryAttribute)a).ToArray();
        }
    }
}
