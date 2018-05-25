using Core.API;
using Core.Connection;
using Core.Data.Base;
using Core.Notification;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Core.Config
{
    public class CardsFile
    {
        private static CardsFile cardsInstance = null;
        public static CardsFile Current
        {
            get
            {
                if (cardsInstance == null)
                    throw new InvalidOperationException("CardsFile не инициализирован! Сначала инициализируйте CardsFile.Initialize");
                return cardsInstance;
            }
        }

        public static void Initialize(string fileName)
        {
            cardsInstance = new CardsFile(fileName);
        }

        // ************************************************************************

        private readonly string InternalConfigName = "config.xml";

        private CardsFile(string fileName)
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
        /// Ресурсы из архива
        /// </summary>
        public Dictionary<string, byte[]> Resource { get; private set; }

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

            Resource = new Dictionary<string, byte[]>();
            foreach (var entry in zip.Entries)
            {
                // Сами папки нас не интересуют
                if (entry.IsDirectory)
                    continue;

                if (entry.FileName.StartsWith("res/"))
                {
                    var keyName = entry.FileName.Substring(4);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.OpenReader().CopyTo(ms);
                        Resource[keyName] = ms.ToArray();
                    }
                    continue;
                }

                // Если требуется загрузить плагины
                if (allowLoadPlugins)
                {
                    // Загружаем библиотеки для плагинов
                    if (entry.FileName.StartsWith("lib/") && entry.FileName.EndsWith(".dll"))
                        AssemblyManager.Instance.LoadFromStream(entry.OpenReader());
                    else if (entry.FileName.StartsWith("plugins/") && entry.FileName.EndsWith(".dll"))
                        PluginManager.Instance.LoadPlugin(AssemblyManager.Instance.LoadFromStream(entry.OpenReader()));
                }
            }

            zip.Dispose();
            Loaded = true;

            // Вызываем событие CardsFileLoaded у плагинов
            PluginListener.Instance.EventCardsFileLoaded(this);
            return true;
        }

        public void Save()
        {
            using (var zip = Loaded ? new ZipFile(FileName) : new ZipFile())
            {
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
}
