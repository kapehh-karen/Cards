using Core.Forms.Design.FormProperties;
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
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = Control.Text;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Control.Text = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
