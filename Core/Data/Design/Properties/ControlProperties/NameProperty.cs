using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class NameProperty : IControlProperty
    {
        public NameProperty(Control control) : base(control) { }

        public override string Name => "Name";

        public override string DisplayName => "Имя элемента";

        public override object Value { get => Control.Name; set => Control.Name = value as string; }

        public override object DefaultValue => $"{Control.GetType().Name}__{DateTime.Now.Ticks.ToString("x")}";

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = Control.Name;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.Name = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
