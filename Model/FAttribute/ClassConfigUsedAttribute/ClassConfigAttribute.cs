using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model.FAttribute
{
    public abstract class ClassConfigAttribute : FactoryAttribute
    {
        public abstract void SetConfigValue(ClassConfig classConfig);
    }
}
