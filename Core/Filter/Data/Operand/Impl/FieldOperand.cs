using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand.Impl
{
    public class FieldOperand : IFilterOperand
    {
        public override OperandType Type => OperandType.FIELD;

        public override FieldType ValueType
        {
            get => FilterField.Field.Type;
            set { /* ignoring */ }
        }

        /// <summary>
        /// Таблица
        /// </summary>
        public FilterTable FilterTable { get; set; }

        /// <summary>
        /// Поле
        /// </summary>
        public FilterField FilterField { get; set; }

        public override string SQLExpression => $"[{FilterTable.AliasName}].[{FilterField.Field.Name}]";
    }
}
