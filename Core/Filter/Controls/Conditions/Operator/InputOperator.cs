using Core.Data.Field;
using Core.Filter.Data.Operand;
using Core.Filter.Data.Operator;
using Core.Filter.Data.Operator.Impl;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Filter.Controls
{
    public class InputOperator : ComboBox
    {
        private class OperatorItem
        {
            public OperatorType OperatorType { get; set; }

            public override string ToString()
            {
                switch (OperatorType)
                {
                    case OperatorType.EQUAL:
                        return "равно";
                    case OperatorType.NOT_EQUAL:
                        return "не равно";
                    case OperatorType.IS_NULL:
                        return "пусто";
                    case OperatorType.IS_NOT_NULL:
                        return "не пусто";
                }

                return base.ToString();
            }
        }

        private Dictionary<FieldType, OperatorType[]> _operators = new Dictionary<FieldType, OperatorType[]>
        {
            { FieldType.UNKNOWN, new OperatorType[0] },
            { FieldType.TEXT, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL } },
            { FieldType.NUMBER, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL } },
            { FieldType.DATE, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL } },
            { FieldType.BOOLEAN, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL } },
            { FieldType.BIND, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL } }
        };

        public InputOperator()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private FieldType dependentType = FieldType.UNKNOWN;
        public FieldType DependentType
        {
            get => dependentType;
            set
            {
                if (dependentType != value)
                {
                    dependentType = value;

                    Items.Clear();
                    Items.AddRange(_operators[value]
                        .Select(t => new OperatorItem() { OperatorType = t })
                        .ToArray());
                }
            }
        }

        public OperatorType Type
        {
            get => SelectedItem != null ? (SelectedItem as OperatorItem).OperatorType : OperatorType.UNKNOWN;
            set => SelectedItem = Items.Cast<OperatorItem>().FirstOrDefault(i => i.OperatorType == value);
        }

        public IFilterOperator Operator
        {
            get
            {
                switch (Type)
                {
                    case OperatorType.EQUAL:
                        return new EqualOperator();
                }
                return null;
            }
            set
            {
                // Если не задан оператор
                if (value == null)
                    return;

                Type = value.Type;
            }
        }
    }
}
