﻿using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Field
{
    /// <summary>
    /// Предоставляет связку "таблица - поле",
    /// используется в связанном значении и в списках данных
    /// </summary>
    public class BindField
    {
        /// <summary>
        /// Какая таблица
        /// </summary>
        public TableData Table { get; set; }

        /// <summary>
        /// С каким полем
        /// </summary>
        public FieldData Field { get; set; }

        public override string ToString()
        {
            return $"Таблица \"{Table.Name}\", поле \"{Field.Name}\"";
        }
    }
}