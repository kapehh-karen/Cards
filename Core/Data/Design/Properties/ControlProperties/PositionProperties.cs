using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class PositionProperties : IControlProperties
    {
        public override string Name => "Position";

        public override object Value { get => Control.Location; set => Control.Location = (Point)value; }

        public override bool ChangeValue()
        {
            using (var dialog = new FormEditPosition())
            {
                dialog.EnteredPosition = Control.Location;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Control.Location = dialog.EnteredPosition;
                    return true;
                }
                return false;
            }
        }
    }
}
