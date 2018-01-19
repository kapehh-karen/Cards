using Core.Filter.Data.Condition;
using Core.Filter.Data.Condition.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data
{
    public class FilterData
    {
        public FilterTable FilterTable { get; set; } = null;
        
        public ICondition Where { get; set; } = new ContainerCondition();

        public FilterData Parent { get; set; } = null;

        public List<FilterData> Chields { get; set; } = new List<FilterData>();
    }
}
