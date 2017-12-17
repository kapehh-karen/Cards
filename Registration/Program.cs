using Core;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Registration
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
            
            // Проверка регистрации программы
            CheckFileAssoc();

            MessageBox.Show("Программа успешно зарегистрирована в системе!", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void CheckFileAssoc()
        {
            var dirName = Path.GetDirectoryName(Application.ExecutablePath);

            var fileAssoc = new FileAssociation()
            {
                Extension = Consts.FileAssociationExtension,
                KeyName = Consts.FileAssociationRegKey,
                FileDescription = Consts.FileAssociationDescription,
                OpenExecutablePath = Path.Combine(dirName, "Cards.exe"),
                EditExecutablePath = Path.Combine(dirName, "Settings.exe")
            };
            
            fileAssoc.SetAssociation();
        }
    }
}
