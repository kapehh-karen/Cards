using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class ColorProperty : IControlProperty
    {
        public ColorProperty(Control control) : base(control) { }

        public override string Name => "Color";

        public override object Value { get => Control.ForeColor; set => Control.ForeColor = (Color)value; }

        public override object DefaultValue => Control.ForeColor;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = Control.ForeColor;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.ForeColor = dialog.Color;
                    return true;
                }
                return false;
            }
        }
    }
}
