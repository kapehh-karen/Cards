using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core.Storage
{
    public class TableStorage
    {
        private static object lockObject = new object();
        private static Dictionary<TableData, TableStorageInformation> cachedTables = new Dictionary<TableData, TableStorageInformation>();

        public static TableStorageInformation Get(TableData table)
        {
            lock (lockObject)
            {
                return cachedTables.ContainsKey(table) ? cachedTables[table] : null;
            }
        }

        public static TableStorageInformation Set(TableData table, DataTable dataTable)
        {
            lock (lockObject)
            {
                if (cachedTables.ContainsKey(table))
                {
                    var itemExists = cachedTables[table];
                    itemExists.Data = dataTable;
                    return itemExists;
                }
                else
                {
                    var itemNew = new TableStorageInformation() { Data = dataTable };
                    cachedTables[table] = itemNew;
                    return itemNew;
                }
            }
        }
    }
}
