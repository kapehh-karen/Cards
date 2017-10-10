using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class MaskProperty : IControlProperties
    {
        public MaskProperty(Control control) : base(control) { }

        public override string Name => "Mask";

        public override object Value { get => (Control as MaskedTextBox).Mask; set => (Control as MaskedTextBox).Mask = value as string; }

        public override object DefaultValue => string.Empty;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = (Control as MaskedTextBox).Mask;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    (Control as MaskedTextBox).Mask = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
