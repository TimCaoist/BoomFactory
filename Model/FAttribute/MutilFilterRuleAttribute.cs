using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class MutilFilterRuleAttribute : BaseFilterRuleAttribute
    {
        public object[] Values { get; set; }

        public override bool IsMatch(object obj)
        {
            foreach (var val in Values)
            {
                if (val.Equals(obj))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
