using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class SizeProperties : IControlProperties
    {
        public override string Name => "Size";

        public override object Value { get => Control.Size; set => Control.Size = (Size)value; }

        public override bool ChangeValue()
        {
            Control.Size = new Size(50, 50);
            return true;
        }
    }
}
