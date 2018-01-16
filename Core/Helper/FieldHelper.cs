using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class FieldHelper
    {
        public static object CastValueForField(FieldData field, object value)
        {
            if (value is DBNull)
                return null;

            switch (field.Type)
            {
                case FieldType.BIND:
                    return CastValueForField(field.BindData.Table.IdentifierField, value);
                case FieldType.BOOLEAN:
                    return value is bool ? value : Convert.ToBoolean(value);
                case FieldType.DATE:
                    return value is DateTime ? value : Convert.ToDateTime(value);
                case FieldType.NUMBER:
                    return value is int ? value : Convert.ToInt32(value);
                case FieldType.TEXT:
                    return value is string ? value : value.ToString();
                default:
                    return null;
            }
        }

        public static Type GetTypeFromField(FieldData field)
        {
            switch (field.Type)
            {
                case FieldType.BIND:
                    return GetTypeFromField(field.BindData.Field);
                case FieldType.BOOLEAN:
                    return typeof(Boolean);
                case FieldType.DATE:
                    return typeof(DateTime);
                case FieldType.NUMBER:
                    return typeof(Int32);
                case FieldType.TEXT:
                    return typeof(String);
            }
            return null;
        }

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

        public static IEnumerable<FieldType> GetFieldTypes()
        {
            return EnumHelper.GetValues<FieldType>();
        }

        /// <summary>
        /// Значения по-умолчанию для полей, которые ЛОГИЧЕСКИ не могут принимать значение NULL
        /// </summary>
        /// <param name="field">Поле</param>
        /// <returns>Значение</returns>
        public static object GetDefaultValue(this FieldData field)
        {
            switch (field.Type)
            {
                case FieldType.BOOLEAN:
                    return true;
                default:
                    return null;
            }
        }
    }
}
