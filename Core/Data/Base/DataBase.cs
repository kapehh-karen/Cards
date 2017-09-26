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
        /// <summary>
        /// Список таблиц базы
        /// </summary>
        [DataMember]
        public List<TableData> Tables { get; set; } = new List<TableData>();
    }
}
