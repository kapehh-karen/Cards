using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelFieldValue : ICloneable
    {
        public ModelValueState State =>
            OldValue?.Equals(Value) ?? Value?.Equals(OldValue) ?? (OldValue == null && Value == null)
            ? ModelValueState.UNCHANGED : ModelValueState.CHANGED;

        public FieldData Field { get; set; } = null;

        public CardModel BindData { get; set; } = null;

        public object Value { get; set; } = null;

        public object OldValue { get; set; } = null;

        public object Clone()
        {
            return new ModelFieldValue()
            {
                Field = Field,
                BindData = BindData,
                Value = Value,
                OldValue = OldValue
            };
        }

        public object ToDataGridValue()
        {
            switch (Field.Type)
            {
                case FieldType.BIND:
                    return BindData?[Field.BindData?.Field];
                case FieldType.BOOLEAN:
                case FieldType.DATE:
                case FieldType.NUMBER:
                case FieldType.TEXT:
                default:
                    return Value;
            }
        }

        public override string ToString()
        {
            switch (Field.Type)
            {
                case FieldType.BIND:
                    return BindData?[Field.BindData?.Field]?.ToString();
                case FieldType.BOOLEAN:
                case FieldType.DATE:
                case FieldType.NUMBER:
                case FieldType.TEXT:
                default:
                    return Value?.ToString();
            }
        }
    }
}
