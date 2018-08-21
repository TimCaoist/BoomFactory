using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class StringFilterRuleAttribute : FilterRuleAttribute
    {
        public CompareMode CompareMode { get; set; }

        public override bool IsMatch(object obj)
        {
            switch (CompareMode)
            {
                case CompareMode.Equlas:
                    return base.IsMatch(obj);
                case CompareMode.StartWith:
                    {
                        var str = obj.ToString();
                        return str.StartsWith(this.Value.ToString());
                    }
                case CompareMode.EndWith:
                    {
                        var str = obj.ToString();
                        return str.EndsWith(this.Value.ToString());
                    }
            }

            return false;
        }
    }
}
