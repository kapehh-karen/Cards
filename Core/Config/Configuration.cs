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
        private DataContractSerializer serializer;
        private XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

        public Configuration()
        {
            serializer = new DataContractSerializer(typeof(T));
        }

        public Configuration(IDataContractSurrogate surrogate)
        {
            serializer = new DataContractSerializer(typeof(T), new List<Type>(), int.MaxValue, false, true, surrogate);
        }

        public void WriteToFile(T obj, string filename)
        {
            using (var writer = XmlWriter.Create(filename, settings))
            {
                serializer.WriteObject(writer, obj);
            }
        }

        public void WriteToStream(T obj, Stream stream)
        {
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.WriteObject(writer, obj);
            }
        }

        public T ReadFromFile(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                return ReadFromFile(stream);
            }
        }

        public T ReadFromFile(Stream inStream)
        {
            try
            {
                return (T)serializer.ReadObject(inStream);
            }
            catch (Exception e)
            {
                NotificationMessage.Error($"Произошла ошибка при считывании {typeof(T).Name}: {e.Message}", e);
            }
            return default(T);
        }
    }
}
