using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties
{
    public abstract class IControlProperties
    {
        public abstract string Name { get; }

        public virtual Control Control { get; set; }

        public virtual object Value { get; set; }

        /// <summary>
        /// Вызвать форму редактирования проперти
        /// </summary>
        /// <returns>true - если пропертя поменялась</returns>
        public abstract bool ChangeValue();
    }
}
