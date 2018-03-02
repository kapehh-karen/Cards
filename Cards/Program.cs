using Core;
using Core.API;
using Core.Config;
using Core.Data.Base;
using Core.Data.Table;
using Core.Forms.Main;
using Core.Notification;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
        static void Main(string[] args)
        {
            NotificationMessage.ReceiveMessage += NotificationMessage_ReceiveMessage;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CardsFileLoader cardsLoader = null;
            DataBase selectedBase = null;
            if (args.Length > 0)
            {
                cardsLoader = new CardsFileLoader(args[0]);
                if (!cardsLoader.Load())
                {
                    return;
                }
                selectedBase = cardsLoader.Base;
            }
            
            TableData selectedTable = null;
            using (var dialogSelectTable = new FormSelectTable()
            {
                SelectedDataBase = selectedBase,
                FileName = cardsLoader?.ShortFileName
            })
            {
                if (dialogSelectTable.ShowDialog() == DialogResult.OK)
                {
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

            // Выгружаем все плагины после работы
            PluginManager.Instance.UnloadAllPlugins();
        }
        
        private static void NotificationMessage_ReceiveMessage(string message, string title, object[] param, NotificationLevel level)
        {
            var msgBoxIcon = MessageBoxIcon.None;

            switch (level)
            {
                case NotificationLevel.INFO:
                case NotificationLevel.SYSTEM_INFO:
                    msgBoxIcon = MessageBoxIcon.Information;
                    break;

                case NotificationLevel.WARNING:
                case NotificationLevel.SYSTEM_WARNING:
                    msgBoxIcon = MessageBoxIcon.Exclamation;
                    break;

                case NotificationLevel.ERROR:
                case NotificationLevel.SYSTEM_ERROR:
                    msgBoxIcon = MessageBoxIcon.Error;
                    break;
            }

            MessageBox.Show(message, Consts.ProgramTitle, MessageBoxButtons.OK, msgBoxIcon);
        }
    }
}
