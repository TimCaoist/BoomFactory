using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class BaseFilterRuleAttribute : ClassConfigAttribute
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public virtual bool IsMatch(object obj)
        {
            return false;
        }

        public override void SetConfigValue(ClassConfig classConfig)
        {
            classConfig.FilterRuleAttributes.Add(this);
        }
    }
}
