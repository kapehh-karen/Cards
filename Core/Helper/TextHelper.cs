using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class TextHelper
    {
        public static string GetTextFieldType(this FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.BIND:
                    return "Связанное";
                case FieldType.BOOLEAN:
                    return "Логическое";
                case FieldType.DATE:
                    return "Дата";
                case FieldType.NUMBER:
                    return "Числовое";
                case FieldType.TEXT:
                    return "Текстовое";
            }
            return fieldType.ToString();
        }
    }
}
