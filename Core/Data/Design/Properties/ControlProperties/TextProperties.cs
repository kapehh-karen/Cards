using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class TextProperties : IControlProperties
    {
        public override string Name => "Text";
        
        public override object Value { get => Control.Text; set => Control.Text = value as string; }

        public override bool ChangeValue()
        {
            Value = $"Label example: {DateTime.Now}";
            return true;
        }
    }
}
