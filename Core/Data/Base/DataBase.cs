using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.Data.Table;
using System.Data.OleDb;
using System.Data;
using Core.Data.Field;
using System.Runtime.Serialization;

namespace Core.Data.Base
{
    [DataContract(IsReference = true)]
    public class DataBase
    {
        [DataMember]
        public string Sever { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string BaseName { get; set; }

        /// <summary>
        /// Список таблиц базы
        /// </summary>
        [DataMember]
        public List<TableData> Tables { get; set; } = new List<TableData>();
    }
}
