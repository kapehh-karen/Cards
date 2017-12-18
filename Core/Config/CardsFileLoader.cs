using Core.Data.Base;
using Core.Notification;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Config
{
    public class CardsFileLoader
    {
        public CardsFileLoader(string fileName)
        {
            this.FileName = fileName;
            this.ShortFileName = Path.GetFileName(fileName);
        }

        public string FileName { get; private set; }

        public string ShortFileName { get; private set; }

        public bool Load()
        {
            ZipFile zip = new ZipFile(FileName);

            var configFile = zip["config.xml"];
            if (configFile == null)
            {
                // Конфиг не найден
                NotificationMessage.SystemError("Не найден файл конфигурации!");
                return false;
            }

            Base = new Configuration<DataBase>().ReadFromFile(configFile.OpenReader());
            if (Base == null)
            {
                // База не загружена
                return false;
            }

            // Получаем список всех плагинов
            foreach (var entry in zip.Entries.Where(item => item.FileName.StartsWith("plugins/") && item.FileName.EndsWith(".dll")))
            {
                //MessageBox.Show(entry.FileName);
            }

            return true;
        }

        public DataBase Base { get; set; }
    }
}
