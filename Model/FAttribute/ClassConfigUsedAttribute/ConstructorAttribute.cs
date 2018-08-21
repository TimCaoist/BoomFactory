using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute.ClassConfigUsedAttribute
{
    public class ConstructorAttribute : ClassConfigAttribute
    {
        public int[] ArgsIndex { get; set; }

        public override void SetConfigValue(ClassConfig classConfig)
        {
            classConfig.ConstructorArgsIndex = this.ArgsIndex;
            classConfig.ConstructorArgsDefine = true;
        }
    }
}
