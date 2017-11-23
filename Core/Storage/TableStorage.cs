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
                if (cachedTables.ContainsKey(table))
                {
                    return cachedTables[table];
                }
                else
                {
                    var item = new TableStorageInformation();
                    cachedTables.Add(table, item);
                    return item;
                }
            }
        }
    }
}
