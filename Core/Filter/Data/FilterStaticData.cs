using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data
{
    public class FilterStaticData : ICloneable
    {
        public int CountEntities { get; set; } = 0;

        public int CountVariables { get; set; } = 0;

        public object Clone() => new FilterStaticData()
        {
            CountEntities = CountEntities,
            CountVariables = CountVariables
        };
    }
}
