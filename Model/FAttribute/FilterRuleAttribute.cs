namespace BoomFactory.Model.FAttribute
{
    public class FilterRuleAttribute : BaseFilterRuleAttribute
    {
        public object Value { get; set; }

        public override bool IsMatch(object obj)
        {
            return Value.Equals(obj);
        }
    }
}
