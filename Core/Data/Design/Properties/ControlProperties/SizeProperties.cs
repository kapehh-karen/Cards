using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class SizeProperties : IControlProperties
    {
        public SizeProperties(Control control) : base(control) { }

        public override string Name => "Size";

        public override object Value { get => Control.Size; set => Control.Size = (Size)value; }

        public override object DefaultValue => new Size(100, 20);

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditSize())
            {
                dialog.EnteredSize = Control.Size;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.Size = dialog.EnteredSize;
                    return true;
                }
                return false;
            }
        }
    }
}
