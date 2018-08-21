using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class DefaultRuleAttribute : ClassConfigAttribute
    {
        public override void SetConfigValue(ClassConfig classConfig)
        {
            classConfig.IsDefault = true;
        }
    }
}
