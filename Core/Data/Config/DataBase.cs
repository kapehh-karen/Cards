using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.Data.Table;
using System.Data.OleDb;
using Core.DataBase;
using System.Data;
using Core.Data.Field;

namespace Core.Data.Config
{
    public class DataBase
    {
        public List<TableData> Tables { get; set; } = new List<TableData>();
    }
}
