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

        public static TableStorageInformation Set(TableData table, TableStorageInformation tableInfo)
        {
            lock (lockObject)
            {
                if (cachedTables.ContainsKey(table))
                {
                    var itemExists = cachedTables[table];
                    itemExists.Data = tableInfo.Data;
                    itemExists.View = tableInfo.View;
                    return itemExists;
                }
                else
                {
                    var itemNew = new TableStorageInformation()
                    {
                        Data = tableInfo.Data,
                        View = tableInfo.View
                    };
                    cachedTables[table] = itemNew;
                    return itemNew;
                }
            }
        }
    }
}
