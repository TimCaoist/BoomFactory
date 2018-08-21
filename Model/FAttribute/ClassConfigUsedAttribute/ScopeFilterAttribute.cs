using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public class ScopeFilterAttribute : ClassConfigAttribute
    {
        public string Scope { get; set; }

        public override void SetConfigValue(ClassConfig classConfig)
        {
            classConfig.ScopeName = Scope;
        }
    }
}
