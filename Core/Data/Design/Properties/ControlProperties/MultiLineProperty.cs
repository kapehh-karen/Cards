using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class MultiLineProperty : IControlProperty
    {
        public MultiLineProperty(Control control) : base(control) { }

        public override string Name => "MultiLine";

        public override string DisplayName => "Мультистрочность";

        public override object Value
        {
            get => (Control as TextBox).Multiline;
            set => (Control as TextBox).Multiline = Convert.ToBoolean(value);
        }

        public override object DefaultValue => false;

        public override bool ChangeValue(object sender = null)
        {
            Value = !(bool)Value;
            return true;
        }
    }
}
