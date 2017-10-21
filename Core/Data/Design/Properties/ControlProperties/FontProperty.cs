using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class FontProperty : IControlProperty
    {
        public FontProperty(Control control) : base(control) { }

        public override string Name => "Font";

        public override object Value { get => Control.Font; set => Control.Font = value as Font; }

        public override object DefaultValue => Control.Font;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FontDialog())
            {
                dialog.Font = Control.Font;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.Font = dialog.Font;
                    return true;
                }
                return false;
            }
        }
    }
}
