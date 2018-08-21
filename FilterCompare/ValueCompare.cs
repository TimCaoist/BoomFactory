using BoomFactory.Model.FAttribute;
using System.Collections.Generic;
using System.Linq;

namespace BoomFactory.FilterCompare
{
    public class ValueCompare
    {
        private static readonly ValueCompare self = new ValueCompare();

        public static ValueCompare Instance => self;

        public bool IsMatch(IEnumerable<BaseFilterRuleAttribute> attributes, object[] filters, int configLen)
        {
            if (configLen == 0)
            {
                return false;
            }

            for (var i = 0; i < configLen; i++)
            {
                var attr = attributes.ElementAt(i);
                if (attr.IsMatch(filters.ElementAt(i)) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
