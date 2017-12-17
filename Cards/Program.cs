using Core;
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
        static void Main()
        {
            NotificationMessage.ReceiveMessage += NotificationMessage_ReceiveMessage;

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

            MessageBox.Show(message, title, MessageBoxButtons.OK, msgBoxIcon);
        }
    }
}
