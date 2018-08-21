using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class IsNullFilterRuleAttribute : BaseFilterRuleAttribute
    {
        public override bool IsMatch(object obj)
        {
            return obj == null;
        }
    }
}
