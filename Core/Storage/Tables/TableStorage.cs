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

namespace Core.Storage.Tables
{
    using TableWithTypeDictAlias = Dictionary<TableStorageType, Dictionary<TableData, TableStorageInformation>>;
    using TableDictAlias = Dictionary<TableData, TableStorageInformation>;

    public class TableStorage
    {
        public static readonly TableStorage Instance = new TableStorage();

        private object lockObject = new object();
        private TableWithTypeDictAlias cachedTypeTables = new TableWithTypeDictAlias
            {
                { TableStorageType.Table, new TableDictAlias() },
                { TableStorageType.Classificator, new TableDictAlias() },
                { TableStorageType.LinkedTable, new TableDictAlias() }
            };

        private string FilePathFromTable(TableData table, TableStorageType type)
            => Path.Combine(Consts.TableStorageFolder, $"{table.ParentBase.BaseName}_{type}_{table.Name}.xml");

        public TableStorageInformation Load(TableData table, TableStorageType type)
        {
            var cfg = new Configuration<TableStorageInformation>(new InternalDataSurrogate(table.ParentBase), true);
            var tableFileConfig = FilePathFromTable(table, type);

            // Если существует конфигурация для таблицы
            if (File.Exists(tableFileConfig))
            {
                return cfg.ReadFromFile(tableFileConfig);
            }
            return null;
        }

        public TableStorageInformation Get(TableData table, TableStorageType type)
        {
            lock (lockObject)
            {
                var cachedTables = cachedTypeTables[type];

                if (cachedTables.ContainsKey(table))
                {
                    return cachedTables[table];
                }
                else
                {
                    var item = Load(table, type) ?? new TableStorageInformation() { IsNew = true };
                    item.Table = table;
                    cachedTables.Add(table, item);
                    return item;
                }
            }
        }

        public void Save(TableStorageInformation tableInformation, TableStorageType type)
        {
            var cfg = new Configuration<TableStorageInformation>(new InternalDataSurrogate(tableInformation.Table.ParentBase), true);
            var tableFileConfig = FilePathFromTable(tableInformation.Table, type);

            cfg.WriteToFile(tableInformation, tableFileConfig);
            tableInformation.IsNew = false;
        }

        public void SaveDefault(TableStorageInformation tableStorageInformation, TableStorageType linkedTable)
        {
            // fill defaults
            var i = 0;
            tableStorageInformation.Columns.ForEach(col =>
            {
                col.Width = 100;
                col.Order = i++;
            });

            Save(tableStorageInformation, linkedTable);
        }
    }
}
