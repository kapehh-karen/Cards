using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class MaxLengthProperty : IControlProperty
    {
        public MaxLengthProperty(Control control) : base(control) { }

        public override string Name => "MaxLength";

        public override object DefaultValue => 0;

        public override object Value { get => (Control as TextBox).MaxLength; set => (Control as TextBox).MaxLength = (int)value; }

        public override bool ChangeValue(object sender = null)
        {
            using (var dialog = new FormEditNumber())
            {
                dialog.EnteredNumber = (Control as TextBox).MaxLength;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    (Control as TextBox).MaxLength = (int)dialog.EnteredNumber;
                    return true;
                }
                return false;
            }
        }
    }
}
