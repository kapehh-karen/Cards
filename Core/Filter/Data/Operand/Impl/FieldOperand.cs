﻿using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operand.Impl
{
    public class FieldOperand : IFilterOperand
    {
        public override OperandType Type => OperandType.FIELD;

        /// <summary>
        /// Таблица
        /// </summary>
        public FilterTable FilterTable { get; set; }

        /// <summary>
        /// Поле
        /// </summary>
        public FilterField FilterField { get; set; }
    }
}