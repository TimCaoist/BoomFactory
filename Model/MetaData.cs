using BoomFactory.Loader;
using BoomFactory.Model.FAttribute;
using BoomFactory.PropertySetter;
using System;
using System.Collections.Generic;

namespace BoomFactory.Model
{
    public class MetaData
    {
        public Type Key { get; private set; }

        public IEnumerable<FactoryAttribute> Attributes { get; private set; }

        public ICollection<Type> SubTypes { get; private set; }

        public MetaMode MetaMode { get; private set; }

        public Dictionary<Type, ClassConfig> Configs = new Dictionary<Type, ClassConfig>();

        internal MetaData(Type type, IEnumerable<FactoryAttribute> attributes)
        {
            this.Key = type;
            this.Attributes = attributes;
            SubTypes = new List<Type>();
        }

        internal void Analysis()
        {
            if (SubTypes.Count < 2)
            {
                MetaMode = MetaMode.Single;
            }
            else {
                MetaMode = MetaMode.Mutil;
            }

            foreach (var subType in SubTypes)
            {
                var attributes = AttributeLoader.Instance.GetFactoryAttributes(subType);
                var config = new ClassConfig(subType, attributes);
                ClassPropertySetter.Instance.ApplyAttibuteSetting(config);
                Configs.Add(subType, config);
            }
        }
    }
}
