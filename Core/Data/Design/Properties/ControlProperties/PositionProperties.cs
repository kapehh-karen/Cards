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
            Value = new Point(50, 50);
            return true;
        }
    }
}
