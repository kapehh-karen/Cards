using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class NameProperties : IControlProperties
    {
        public NameProperties() : base() { }

        public override string Name => "Name";

        public override object Value { get => Control.Name; set => Control.Name = value as string; }

        public override object DefaultValue => $"{Name}_{DateTime.Now.Ticks.ToString("x")}";

        public override bool ChangeValue()
        {
            using (var dialog = new FormEditText())
            {
                dialog.EnteredText = Control.Name;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Control.Name = dialog.EnteredText;
                    return true;
                }
                return false;
            }
        }
    }
}
