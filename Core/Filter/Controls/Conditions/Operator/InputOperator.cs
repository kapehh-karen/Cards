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

namespace Core.Filter.Controls.Conditions.Operator
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
                    case OperatorType.LIKE:
                        return "содержит";
                    case OperatorType.NOT_LIKE:
                        return "не содержит";
                    case OperatorType.GREATER:
                        return "больше";
                    case OperatorType.GREATER_EQUAL:
                        return "больше или равно";
                    case OperatorType.LESS:
                        return "меньше";
                    case OperatorType.LESS_EQUAL:
                        return "меньше или равно";
                }

                return base.ToString();
            }
        }

        private Dictionary<FieldType, OperatorType[]> _operators = new Dictionary<FieldType, OperatorType[]>
        {
            { FieldType.UNKNOWN, new OperatorType[0] },
            { FieldType.TEXT, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL, OperatorType.LIKE, OperatorType.NOT_LIKE } },
            { FieldType.NUMBER, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL, OperatorType.GREATER, OperatorType.GREATER_EQUAL, OperatorType.LESS, OperatorType.LESS_EQUAL } },
            { FieldType.DATE, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL, OperatorType.GREATER, OperatorType.GREATER_EQUAL, OperatorType.LESS, OperatorType.LESS_EQUAL } },
            { FieldType.BOOLEAN, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL } },
            { FieldType.BIND, new OperatorType[] { OperatorType.EQUAL, OperatorType.NOT_EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL } }
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
                    case OperatorType.NOT_EQUAL:
                        return new NotEqualOperator();
                    case OperatorType.IS_NULL:
                        return new NullOperator();
                    case OperatorType.IS_NOT_NULL:
                        return new NotNullOperator();
                    case OperatorType.LIKE:
                        return new LikeOperator();
                    case OperatorType.NOT_LIKE:
                        return new NotLikeOperator();
                    case OperatorType.GREATER:
                        return new GreaterOperator();
                    case OperatorType.GREATER_EQUAL:
                        return new GreaterEqualOperator();
                    case OperatorType.LESS:
                        return new LessOperator();
                    case OperatorType.LESS_EQUAL:
                        return new LessEqualOperator();
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
