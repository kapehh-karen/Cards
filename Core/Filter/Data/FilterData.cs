using Core.Data.Table;
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
        // TODO: Это тоже сериализировать в файл при сохранении, а то будет пиздень, циферки поедут
        private static int countEntities = 0;

        public static FilterData CreateBy(TableData table, FilterData parent = null)
        {
            var data = new FilterData();
            countEntities++;
            data.FilterTable.Table = table;
            data.FilterTable.AliasName = $"{table.Name}_{countEntities}";
            data.Parent = parent;
            if (parent != null) parent.Chields.Add(data);
            return data;
        }

        [DataMember]
        public FilterTable FilterTable { get; set; } = new FilterTable();

        [DataMember]
        public ICondition Where { get; set; } = new ContainerCondition();

        [DataMember]
        public FilterData Parent { get; set; } = null;

        [DataMember]
        public List<FilterData> Chields { get; set; } = new List<FilterData>();
        
        public void MoveTo(FilterData newParent)
        {
            if (Parent != null) Parent.Chields.Remove(this);
            Parent = newParent;
            Parent?.Chields.Add(this);
        }

        public bool IsRoot => Parent == null;

        public void Remove() => MoveTo(null);

        public override string ToString()
        {
            return FilterTable.ToString();
        }
    }
}
