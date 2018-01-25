using Core.Filter.Data.Condition;
using Core.Filter.Data.Condition.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Filter.Data
{
    [DataContract]
    public class FilterData
    {
        [DataMember]
        public FilterTable FilterTable { get; set; } = null;

        [DataMember]
        public ICondition Where { get; set; } = new ContainerCondition();

        [DataMember]
        public FilterData Parent { get; set; } = null;

        [DataMember]
        public List<FilterData> Chields { get; set; } = new List<FilterData>();

        public bool IsRoot => Parent == null;

        public void MoveTo(FilterData newParent)
        {
            if (Parent != null) Parent.Chields.Remove(this);
            newParent.Chields.Add(this);
        }
    }
}
