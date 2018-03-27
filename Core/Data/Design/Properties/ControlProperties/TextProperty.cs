using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class TextProperty : IControlProperty
    {
        private bool protectByEmpty;

        public TextProperty(Control control, bool protectByEmpty = false) : base(control)
        {
            this.protectByEmpty = protectByEmpty;
        }

        public override string Name => "Text";

        public override string DisplayName => "Текст";

        public override object Value
        {
            get => Control.Text;
            set
            {
                var txt = (value as string)?.Trim();

                if (protectByEmpty && string.IsNullOrEmpty(txt))
                    Control.Text = "--";
                else
                    Control.Text = txt;
            }
        }

        public override object DefaultValue => string.Empty;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = Value as string;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Value = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
