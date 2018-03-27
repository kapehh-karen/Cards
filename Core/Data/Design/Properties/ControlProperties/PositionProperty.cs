using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class PositionProperty : IControlProperty
    {
        public PositionProperty(Control control) : base(control) { }

        public override string Name => "Position";

        public override string DisplayName => "Положение";

        public override object Value { get => Control.Location; set => Control.Location = (Point)value; }

        public override object DefaultValue => new Point(0, 0);

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditPosition())
            {
                dialog.EnteredPosition = Control.Location;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.Location = dialog.EnteredPosition;
                    return true;
                }
                return false;
            }
        }
    }
}
