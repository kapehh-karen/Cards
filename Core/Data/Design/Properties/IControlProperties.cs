using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties
{
    public abstract class IControlProperties
    {
        public IControlProperties(Control control)
        {
            this.Control = control;
            this.Value = DefaultValue;
        }

        public abstract string Name { get; }

        public Control Control { get; set; }

        public virtual object Value { get; set; }

        public abstract object DefaultValue { get; }

        /// <summary>
        /// Вызвать форму редактирования проперти
        /// </summary>
        /// <returns>true - если пропертя поменялась</returns>
        public abstract bool ChangeValue();
    }
}
