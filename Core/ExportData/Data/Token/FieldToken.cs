using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExportData.Data.Token
{
    public class FieldToken
    {
        private static int Index = 1;

        public static void ResetIndex()
        {
            Index = 1;
        }

        // End static

        public FieldToken(FieldData field)
        {
            InternalName = $"F{Index++}";
            Field = field;
        }

        public FieldData Field { get; set; }

        public string InternalName { get; private set; }
    }
}
