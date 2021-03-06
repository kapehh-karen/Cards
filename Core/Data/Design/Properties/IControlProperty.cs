﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties
{
    public abstract class IControlProperty
    {
        public IControlProperty(Control control)
        {
            this.Control = control;
            this.Value = DefaultValue;
        }

        public abstract string Name { get; }

        public virtual string DisplayName => Name;

        public virtual string Description => string.Empty;

        public Control Control { get; set; }

        public virtual object Value { get; set; }

        public abstract object DefaultValue { get; }

        /// <summary>
        /// Вызвать форму редактирования проперти
        /// </summary>
        /// <returns>true - если пропертя поменялась</returns>
        public abstract bool ChangeValue(object sender = null);
    }
}
