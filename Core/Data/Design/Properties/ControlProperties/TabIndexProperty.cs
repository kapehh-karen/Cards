using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class TabIndexProperty : IControlProperty
    {
        public TabIndexProperty(Control control) : base(control) { }

        public override string Name => "TabIndex";

        public override string DisplayName => "Индекс табуляции";

        public override object Value { get => Control.TabIndex; set => Control.TabIndex = (int)value; }

        public override object DefaultValue => 0;

        public override bool ChangeValue(object sender)
        {
            using (var dialog = new FormEditNumber())
            {
                dialog.EnteredNumber = Control.TabIndex;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Control.TabIndex = (int)dialog.EnteredNumber;
                    return true;
                }
                return false;
            }
        }
    }
}
