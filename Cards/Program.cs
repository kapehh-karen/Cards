using Core;
using Core.API;
using Core.Config;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
using Core.Forms.Main;
using Core.Helper;
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
            UnhandledException.Init();

            if (ArgumentsUtil.GetCardsFileName(args, out var fileName))
            {
                CardsFile.Initialize(fileName);
                if (WaitDialog.Run(
                        $"Идет загрузка конфигурационного файла \"{CardsFile.Current.ShortFileName}\"",
                        () => !CardsFile.Current.Load()))
                    return;
            }
            else
            {
                return;
            }

            bool needSelectTable;
            do
            {
                needSelectTable = false;

                using (var dialogSelectTable = new FormSelectTable()
                {
                    Base = SQLServerConnection.DefaultDataBase,
                    FileName = CardsFile.Current.ShortFileName
                })
                {
                    if (dialogSelectTable.ShowDialog() == DialogResult.OK)
                    {
                        var selectedTable = dialogSelectTable.SelectedTableData;
                        var formView = selectedTable.TableView;
                        formView.FillTable();
                        needSelectTable = formView.ShowDialog() == DialogResult.Ignore;
                    }
                }
            } while (needSelectTable);

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

            MessageBox.Show(param.Length == 0 ? message : string.Format(message, param), title, MessageBoxButtons.OK, msgBoxIcon);
        }
    }
}
