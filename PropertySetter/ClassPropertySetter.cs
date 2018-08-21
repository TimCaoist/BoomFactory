using BoomFactory.Model;
using BoomFactory.Model.FAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.PropertySetter
{
    internal class ClassPropertySetter
    {
        private readonly static ClassPropertySetter self = new ClassPropertySetter();

        internal static ClassPropertySetter Instance
        {
            get {
                return self;
            }
        }

        private void Set(ClassConfig classConfig, FactoryAttribute attribute)
        {
            if (attribute is ClassConfigAttribute cca)
            {
                cca.SetConfigValue(classConfig);
            }
        }

        public void ApplyAttibuteSetting(ClassConfig classConfig)
        {
            foreach (var attr in classConfig.Attributes)
            {
                Set(classConfig, attr);
            }

            classConfig.FilterRuleAttributes = classConfig.FilterRuleAttributes.OrderBy(f => f.Index).ToList();
            classConfig.ConfigLength = classConfig.FilterRuleAttributes.Count();
        }
    }
}
