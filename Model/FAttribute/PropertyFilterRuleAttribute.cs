using BoomFactory.FilterCompare;

namespace BoomFactory.Model.FAttribute
{
    public class PropertyFilterRuleAttribute : FilterRuleAttribute
    {
        public string PropertyName { get; set; }

        public override bool IsMatch(object obj)
        {
            var type = obj.GetType();
            var property = type.GetProperty(PropertyName);
            if (property == null)
            {
                return false;
            }
            
            var data = property.GetValue(obj, null);
            return data.Equals(Value);
        }
    }
}
