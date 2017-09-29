using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class TextProperties : IControlProperties
    {
        public override string Name => "Text";
        
        public override bool ChangeValue()
        {
            Value = $"Label example: {DateTime.Now}";
            Control.Text = Value as string;
            return true;
        }
    }
}
