using Core.Notification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Core.Config
{
    public class Configuration<T>
    {
        private DataContractSerializer serializer = new DataContractSerializer(typeof(T));
        private XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

        public void WriteToFile(T obj, string filename)
        {
            using (var writer = XmlWriter.Create(filename, settings))
            {
                serializer.WriteObject(writer, obj);
            }
        }

        public T ReadFromFile(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                try
                {
                    return (T)serializer.ReadObject(stream);
                }
                catch (Exception e)
                {
                    NotificationMessage.Error($"Произошла ошибка при считывании конфигурации {typeof(T).Name}: {e.Message}", e);
                }
                return default(T);
            }
        }
    }
}
