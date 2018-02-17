using Core.Config;
using Core.Config.Surrogate;
using Core.Connection;
using Core.Data.Base;
using Core.Filter.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Storage.Filter
{
    public class FilterStorage
    {
        public static readonly FilterStorage Instance = new FilterStorage();

        private string FilterDialog = "Файл фильтра|*.cardsf";

        private Configuration<FilterData> CreateCfg() =>
            new Configuration<FilterData>(new InternalDataSurrogate(SQLServerConnection.DefaultDataBase), true);

        private void CheckDirectory()
        {
            if (!Directory.Exists(Consts.FilterStorageFolder))
            {
                Directory.CreateDirectory(Consts.FilterStorageFolder);
            }
        }

        public FilterData Load()
        {
            var cfg = CreateCfg();
            CheckDirectory();

            using (var dialog = new OpenFileDialog()
            {
                InitialDirectory = Consts.FilterStorageFolder,
                Filter = FilterDialog
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    return cfg.ReadFromFile(dialog.FileName);
                else
                    return null;
            }
        }

        public void Save(FilterData filterData)
        {
            var cfg = CreateCfg();
            CheckDirectory();

            using (var dialog = new SaveFileDialog()
            {
                InitialDirectory = Consts.FilterStorageFolder,
                Filter = FilterDialog
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    cfg.WriteToFile(filterData, dialog.FileName);
            }
        }
    }
}
