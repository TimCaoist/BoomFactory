using BoomFactory.FilterCompare;
using BoomFactory.Model.FAttribute;
using System;
using System.Collections.Generic;

namespace BoomFactory.Model
{
    public class ClassConfig
    {
        internal IEnumerable<FactoryAttribute> Attributes
        {
            get;  set;
        }

        public Type InstanceType { get; private set; }

        public bool Single {
            get {
                return CompentMode == CompentMode.SingleFirst;
            }
        }

        public bool IsDefault {
            get; internal set;
        }

        public CompentMode CompentMode { get; internal set; }

        internal ICollection<BaseFilterRuleAttribute> FilterRuleAttributes = new List<BaseFilterRuleAttribute>();

        public int ConfigLength {
            get; internal set;
        }

        public string ScopeName { get; internal set; }
        public int[] ConstructorArgsIndex { get; internal set; }
        public bool ConstructorArgsDefine { get; internal set; }

        public ClassConfig(Type type, IEnumerable<FactoryAttribute> attributes)
        {
            this.InstanceType = type;
            this.Attributes = attributes;
        }

        internal bool IsMatch(object[] filters, FactoryConfig factoryConfig)
        {
            if (factoryConfig.ScopeName != ScopeName && 
                !string.IsNullOrEmpty(ScopeName) && 
                !string.IsNullOrEmpty(factoryConfig.ScopeName))
            {
                return false;
            }

            if (filters.Length == 1)
            {
                Dictionary<string, object> dict = filters[0] as Dictionary<string, object>;
                if (dict != null)
                {
                    return DictCompare.Instance.IsMatch(FilterRuleAttributes, dict);
                }
            }

            return ValueCompare.Instance.IsMatch(FilterRuleAttributes, filters, ConfigLength);
        }
    }
}
