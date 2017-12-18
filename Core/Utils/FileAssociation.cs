using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Utils
{
    public class FileAssociation
    {
        public string Extension { get; set; }

        /// <summary>
        /// Ключ в реестре (без пробелов и прочих плохих символов)
        /// </summary>
        public string KeyName { get; set; }

        public string OpenExecutablePath { get; set; }

        public string EditExecutablePath { get; set; }

        /// <summary>
        /// Описание расширения
        /// </summary>
        public string FileDescription { get; set; }

        /// <summary>
        /// Зарегистрировано ли расширение в системе
        /// </summary>
        public bool IsExists => Registry.ClassesRoot.OpenSubKey(Extension, false) != null;

        /// <summary>
        /// Ассоциировать расширение с программой
        /// </summary>
        public void SetAssociation()
        {
            var BaseKey = Registry.ClassesRoot.CreateSubKey(Extension);
            BaseKey.SetValue(string.Empty, KeyName);

            // Пункт для "Создать"
            var ShellNew = BaseKey.CreateSubKey("ShellNew");
            ShellNew.SetValue(string.Empty, KeyName);
            ShellNew.SetValue("NullFile", string.Empty); // Файл создаваться будет пустым

            var OpenMethod = Registry.ClassesRoot.CreateSubKey(KeyName);
            OpenMethod.SetValue(string.Empty, FileDescription);
            OpenMethod.CreateSubKey("DefaultIcon").SetValue(string.Empty, $"\"{OpenExecutablePath}\",0");

            // Ассоциация с программой для "Открыть" и "Изменить"
            var Shell = OpenMethod.CreateSubKey("Shell");
            Shell.CreateSubKey("open").CreateSubKey("command").SetValue(string.Empty, $"\"{OpenExecutablePath}\" \"%1\"");
            Shell.CreateSubKey("edit").CreateSubKey("command").SetValue(string.Empty, $"\"{EditExecutablePath}\" \"%1\"");

            BaseKey.Close();
            OpenMethod.Close();
            Shell.Close();

            // Delete the key instead of trying to change it
            var CurrentUser = Registry.CurrentUser.OpenSubKey($@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\{Extension}", true);
            CurrentUser.DeleteSubKey("UserChoice", false);
            CurrentUser.Close();

            // Tell explorer the file association has been changed
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
    }
}
