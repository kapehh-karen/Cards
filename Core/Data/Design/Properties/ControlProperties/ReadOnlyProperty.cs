using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class ReadOnlyProperty : IControlProperty
    {
        public ReadOnlyProperty(Control control) : base(control) { }

        public override string Name => "ReadOnly";

        public override string DisplayName => "Неизменяемое";

        public override object Value
        {
            get => (Control as TextBox).ReadOnly;
            set => (Control as TextBox).ReadOnly = Convert.ToBoolean(value);
        }

        public override object DefaultValue => false;

        public override bool ChangeValue(object sender = null)
        {
            Value = !(bool)Value;
            return true;
        }
    }
}
