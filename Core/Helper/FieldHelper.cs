using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class FieldHelper
    {
        public static object CastValue(FieldData field, object value)
        {
            if (value is DBNull)
                return null;

            switch (field.Type)
            {
                case FieldType.BIND:
                    return value is int ? value : Convert.ToInt32(value);
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
    }
}
