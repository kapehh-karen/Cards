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
            get
            {
                var ctrl = Control as TextBox;
                ctrl.ScrollBars = ScrollBars.Vertical;
                return ctrl.Multiline;
            }
            set
            {
                var ctrl = Control as TextBox;
                ctrl.ScrollBars = ScrollBars.None;
                ctrl.Multiline = Convert.ToBoolean(value);
            }
        }

        public override object DefaultValue => false;

        public override bool ChangeValue(object sender = null)
        {
            Value = !(bool)Value;
            return true;
        }
    }
}
