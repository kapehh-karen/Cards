using Core.Data.Table;
using Core.Filter.Data.Condition;
using Core.Filter.Data.Condition.Impl;
using Core.Filter.Data.Operand.Impl;
using Core.Filter.Data.Operator.Impl;
using Core.Filter.SQLBuilders;
using Core.Filter.SQLBuilders.Impl;
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
        /// <summary>
        /// Создаем FilterData для основной таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static FilterData CreateRoot(TableData table, FilterData parent = null) =>
            CreateBy(table, parent, new FullSQLBuilder());

        /// <summary>
        /// Создаем FilterData для выборок
        /// </summary>
        /// <param name="table"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static FilterData CreateSubquery(TableData table, FilterData parent = null) =>
            CreateBy(table, parent, new CountSQLBuilder());

        private static FilterData CreateBy(TableData table, FilterData parent = null, ISQLBuilder sqlBuilder = null)
        {
            var data = new FilterData()
            {
                Parent = parent,
                SQLBuilder = sqlBuilder
            };
            
            if (parent != null)
            {
                parent.Chields.Add(data);
                data.StaticData = parent.StaticData;
            }

            if (sqlBuilder != null) sqlBuilder.Filter = data;
            if (data.StaticData == null) data.StaticData = new FilterStaticData();

            data.StaticData.CountEntities++;
            data.FilterTable.Table = table;
            data.FilterTable.AliasName = $"{table.Name}_{data.StaticData.CountEntities}";
            
            return data;
        }

        /// <summary>
        /// Текущая таблица
        /// </summary>
        [DataMember]
        public FilterTable FilterTable { get; set; } = new FilterTable();

        /// <summary>
        /// Конструкция WHERE
        /// </summary>
        [DataMember]
        public ICondition Where { get; set; } = new ContainerCondition();

        /// <summary>
        /// Родительская таблица
        /// </summary>
        [DataMember]
        public FilterData Parent { get; set; }

        /// <summary>
        /// Вложенные запросы
        /// </summary>
        [DataMember]
        public List<FilterData> Chields { get; set; } = new List<FilterData>();

        /// <summary>
        /// Требуется для построения SELECT запроса
        /// </summary>
        [DataMember]
        public ISQLBuilder SQLBuilder { get; set; }

        [DataMember]
        public FilterStaticData StaticData { get; set; }

        public string SQLExpression => SQLBuilder?.SQLExpression;

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
