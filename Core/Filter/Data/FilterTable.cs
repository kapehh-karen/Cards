using Core.Data.Table;

namespace Core.Filter.Data
{
    public class FilterTable
    {
        /// <summary>
        /// Таблица
        /// </summary>
        public TableData Table { get; set; }

        /// <summary>
        /// Алиас таблицы в запросах. Например 'People_1'
        /// </summary>
        public string TableAliasName { get; set; }
    }
}
