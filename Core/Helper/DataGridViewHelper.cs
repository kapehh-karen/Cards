using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class DataGridViewHelper
    {
        public static string BuildRowFilter(FieldData field, string text)
            => BuildRowFilter(field.Name, text);

        public static string BuildRowFilter(string fieldName, string text)
            => $"Convert([{fieldName}], System.String) like '%{EscapeLikeValue(text)}%'";

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
