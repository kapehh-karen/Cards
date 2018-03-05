using Core.Config;
using Core.Data.Base;
using Core.Forms.DateBase;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Settings
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length > 0)
            {
                CardsFile.Initialize(args[0]);
                WaitDialog.Run($"Идет загрузка конфигурационного файла \"{CardsFile.Current.ShortFileName}\"",
                               () => CardsFile.Current.Load(false));
                Application.Run(new FormBindSetting() { CardsLoader = CardsFile.Current });
            }
            else
            {
                MessageBox.Show("Имя файла должно быть передано через командную строку", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
