using Core;
using Core.Config;
using Core.Data.Base;
using Core.Forms.DateBase;
using Core.Helper;
using Core.Utils;
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
            UnhandledException.Init();

            if (ArgumentsUtil.GetCardsFileName(args, out var fileName))
            {
                CardsFile.Initialize(fileName);
                WaitDialog.Run($"Идет загрузка конфигурационного файла \"{CardsFile.Current.ShortFileName}\"",
                               () => CardsFile.Current.Load(false));
                Application.Run(new FormBindSetting() { CardsLoader = CardsFile.Current });
            }
        }
    }
}
