using BoomFactory.Model.FAttribute;
using System.Collections.Generic;
using System.Linq;

namespace BoomFactory.FilterCompare
{
    public class DictCompare
    {
        private static readonly DictCompare self = new DictCompare();

        public static DictCompare Instance => self;

        public bool IsMatch(IEnumerable<BaseFilterRuleAttribute> attributes, Dictionary<string, object> filters)
        {
            foreach (var filter in filters)
            {
                var attr = attributes.FirstOrDefault(a => a.Name == filter.Key);
                if (attr == null || attr.IsMatch(filter.Value) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
