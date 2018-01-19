using Core.Data.Table;
using System.Runtime.Serialization;

namespace Core.Filter.Data
{
    [DataContract]
    public class FilterTable
    {
        /// <summary>
        /// Таблица
        /// </summary>
        [DataMember]
        public TableData Table { get; set; }
        
        /// <summary>
        /// Алиас таблицы в запросах. Например 'XCARDS_1'
        /// </summary>
        [DataMember]
        public string AliasName { get; set; }
    }
}
