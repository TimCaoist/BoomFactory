using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomFactory.Model
{
    public enum CompentMode : byte
    {
        SingleFirst = 0,

        PerRequest = 1,

        BySetting = 2
    }
}
