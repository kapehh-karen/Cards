﻿using Core.Data.Design.Properties;
using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls
{
    public abstract class IDesignControl
    {
        /// <summary>
        /// Контрол располагаемый на форме (реально)
        /// </summary>
        public abstract Control FormControl { get; }

        public virtual List<IControlProperties> Properties { get; } = new List<IControlProperties>();

        
        /// <summary>
        /// Тип виртуального контрола (просто элементы, либо связаны с полями БД)
        /// </summary>
        public abstract DesignControlType ControlType { get; }

        /// <summary>
        /// С чем связано (Fields или LinkedTables в TableData)
        /// </summary>
        public virtual string ConfigItemName { get; set; }
    }
}
