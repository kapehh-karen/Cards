using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class TextProperties : IControlProperties
    {
        public TextProperties(Control control) : base(control) { }

        public override string Name => "Text";
        
        public override object Value { get => Control.Text; set => Control.Text = value as string; }

        public override object DefaultValue => string.Empty;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = Control.Text;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.Text = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
