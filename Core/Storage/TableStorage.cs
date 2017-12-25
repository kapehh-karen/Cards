using Core.Config;
using Core.Data.Base;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Storage
{
    public class TableStorage
    {
        private static object lockObject = new object();
        private static Dictionary<TableData, TableStorageInformation> cachedTables
            = new Dictionary<TableData, TableStorageInformation>();

        public static TableStorageInformation Load(TableData table)
        {
            var cfg = new Configuration<TableStorageInformation>();
            var tableFileConfig = Path.Combine(Consts.TableStorageFolder, table.Name);

            // Если существует конфигурация для таблицы
            if (File.Exists(tableFileConfig))
            {
                return cfg.ReadFromFile(tableFileConfig);
            }
            return null;
        }

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
                    var item = Load(table) ?? new TableStorageInformation();
                    cachedTables.Add(table, item);
                    return item;
                }
            }
        }

        public static void Save(TableData table)
        {
            var cfg = new Configuration<TableStorageInformation>();
            var tableFileConfig = Path.Combine(Consts.TableStorageFolder, table.Name);

            cfg.WriteToFile(Get(table), tableFileConfig);
        }
    }
}
