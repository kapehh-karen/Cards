using Core.Forms.DateBase;
using Core.Forms.Design;
using Core.Helper;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //NotificationMessage.ReceiveMessage += NotificationMessage_ReceiveMessage;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormBindSetting());
        }

        private static void NotificationMessage_ReceiveMessage(string message, object[] param, NotificationLevel level)
        {
            //if (level.ToString().StartsWith("SYSTEM"))
            //    return;

            if (param != null)
            {
                MessageBox.Show($"{level}:\r\n{message}\r\n\r\n{param}", level.ToString());
            }
            else
            {
                MessageBox.Show(message, level.ToString());
            }
        }
    }
}
