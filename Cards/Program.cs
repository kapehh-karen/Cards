using Core.Data.Base;
using Core.Data.Table;
using Core.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cards
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataBase selectedBase = null;
            TableData selectedTable = null;

            using (var dialogSelectTable = new FormSelectTable())
            {
                if (dialogSelectTable.ShowDialog() == DialogResult.OK)
                {
                    selectedBase = dialogSelectTable.SelectedDataBase;
                    selectedTable = dialogSelectTable.SelectedTableData;
                }
            }

            if (selectedBase != null && selectedTable != null)
            {
                var dialog = new FormTableView()
                {
                    Base = selectedBase,
                    Table = selectedTable
                };
                dialog.FillTable();
                Application.Run(dialog);
            }
        }
    }
}
