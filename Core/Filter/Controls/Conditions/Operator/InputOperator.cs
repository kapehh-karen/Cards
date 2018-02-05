using Core.Data.Field;
using Core.Filter.Data.Operator;
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
                    case OperatorType.BETWEEN:
                        return "между";
                    case OperatorType.IS_NULL:
                        return "пустое";
                    case OperatorType.IS_NOT_NULL:
                        return "не пустое";
                }

                return base.ToString();
            }
        }

        private Dictionary<FieldType, OperatorType[]> _operators = new Dictionary<FieldType, OperatorType[]>
        {
            { FieldType.UNKNOWN, new OperatorType[0] },
            { FieldType.TEXT, new OperatorType[] { OperatorType.EQUAL, OperatorType.IS_NULL, OperatorType.IS_NOT_NULL } },
            { FieldType.NUMBER, new OperatorType[] { OperatorType.EQUAL, OperatorType.BETWEEN } },
            { FieldType.DATE, new OperatorType[] { OperatorType.EQUAL } },
            { FieldType.BOOLEAN, new OperatorType[] { OperatorType.EQUAL } },
            { FieldType.BIND, new OperatorType[] { OperatorType.EQUAL } }
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

        public OperatorType Type => SelectedItem != null ? (SelectedItem as OperatorItem).OperatorType : OperatorType.UNKNOWN;
    }
}
