using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class CompentAttribute : ClassConfigAttribute
    {
        public CompentMode CompentMode { get; set; }

        public override void SetConfigValue(ClassConfig classConfig)
        {
            classConfig.CompentMode = CompentMode;
        }
    }
}
