using Core.API;
using Core.Connection;
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
        private readonly string InternalConfigName = "config.xml";

        public CardsFileLoader(string fileName)
        {
            this.FileName = fileName;
            this.ShortFileName = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Полное имя файла
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Имя файла без пути
        /// </summary>
        public string ShortFileName { get; private set; }
        
        public DataBase Base { get; set; }

        /// <summary>
        /// Указывает, загружена база или нет
        /// </summary>
        public bool Loaded { get; private set; }

        /// <summary>
        /// Помещает в свойство Base объект конфига
        /// </summary>
        /// <returns>true - если всё ок, иначе - false</returns>
        public bool Load(bool allowLoadPlugins = true)
        {
            Loaded = false;
            Base = new DataBase();

            ZipFile zip = null;
            try
            {
                zip = new ZipFile(FileName);
            }
            catch (ZipException)
            {
                NotificationMessage.SystemError("Файл поврежден или имеет неверный формат!");
                return false;
            }

            var configFile = zip[InternalConfigName];
            if (configFile == null)
            {
                NotificationMessage.SystemError("Не найден файл конфигурации!");
                return false;
            }

            Base = new Configuration<DataBase>().ReadFromFile(configFile.OpenReader());
            if (Base == null)
            {
                // База не загружена
                return false;
            }

            // База по-умолчанию
            SQLServerConnection.DefaultDataBase = Base;

            // fill parent-property
            Base.Tables.ForEach(t =>
            {
                t.ParentBase = Base;
                t.Fields.ForEach(f => f.ParentTable = t);
                t.LinkedTables.ForEach(lt => lt.ParentTable = t);
            });

            // Загружаем плагины
            if (allowLoadPlugins)
            {
                foreach (var entry in zip.Entries.Where(item => item.FileName.StartsWith("plugins/") && item.FileName.EndsWith(".dll")))
                {
                    PluginManager.Instance.LoadPlugin(entry.OpenReader());
                }
            }

            Loaded = true;
            return true;
        }

        public void Save()
        {
            ZipFile zip = Loaded ? new ZipFile(FileName) : new ZipFile();

            // Сериализация в памяти
            var memStream = new MemoryStream();
            new Configuration<DataBase>().WriteToStream(Base, memStream);
            memStream.Seek(0, SeekOrigin.Begin);

            // Замена конфига в архиве
            if (zip.ContainsEntry(InternalConfigName)) zip.RemoveEntry(InternalConfigName);
            zip.AddEntry(InternalConfigName, memStream);

            zip.Save(FileName);
        }
    }
}
