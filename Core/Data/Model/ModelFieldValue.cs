﻿using Core.Connection;
using Core.Data.Field;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelFieldValue : ICloneable
    {
        private bool EqualsObjectValues(object a, object b) =>
            a?.Equals(b) ?? b?.Equals(a) ?? (a == null && b == null);

        public ModelValueState State => EqualsObjectValues(OldValue, Value) ? ModelValueState.UNCHANGED : ModelValueState.CHANGED;

        public FieldData Field { get; set; } = null;

        public CardModel BindData { get; set; } = null;

        public object Value { get; set; } = null;

        public object OldValue { get; set; } = null;

        public bool UpdateBindData()
        {
            // Если тип не BIND или Value пустое, то нафиг
            if (Field?.Type != FieldType.BIND || Value == null)
            {
                BindData = null;
                return true;
            }

            // Если тип поля BIND и bindData пустое, или у нас Value поменялось, то начинаем творить вакханалию
            if (BindData == null || !EqualsObjectValues(BindData.ID.Value, Value))
            {
                ModelHelper.Get(Field.BindData.Table, Value, out var bindData);
                if (bindData != null)
                {
                    BindData = bindData;
                    return true;
                }
            }
            return false;
        }

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
            switch (Field?.Type)
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
            switch (Field?.Type)
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
