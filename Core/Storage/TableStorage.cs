using Core.Config;
using Core.Config.Surrogate;
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
        public static readonly TableStorage Instance = new TableStorage();

        private object lockObject = new object();
        private Dictionary<TableData, TableStorageInformation> cachedTables
            = new Dictionary<TableData, TableStorageInformation>();

        private string FilePathFromTable(TableData table)
            => Path.Combine(Consts.TableStorageFolder, $"{table.ParentBase.BaseName}_{table.Name}.xml");

        public TableStorageInformation Load(TableData table)
        {
            var cfg = new Configuration<TableStorageInformation>(new InternalDataSurrogate(table.ParentBase));
            var tableFileConfig = FilePathFromTable(table);

            // Если существует конфигурация для таблицы
            if (File.Exists(tableFileConfig))
            {
                var item = cfg.ReadFromFile(tableFileConfig);
                item.IsNew = false;
                return item;
            }
            return null;
        }

        public TableStorageInformation Get(TableData table)
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
                    item.Table = table;
                    cachedTables.Add(table, item);
                    return item;
                }
            }
        }

        public void Save(TableStorageInformation tableInformation)
        {
            var cfg = new Configuration<TableStorageInformation>(new InternalDataSurrogate(tableInformation.Table.ParentBase));
            var tableFileConfig = FilePathFromTable(tableInformation.Table);

            cfg.WriteToFile(tableInformation, tableFileConfig);
            tableInformation.IsNew = false;
        }
    }
}
