using Core.Data.Field;
using Core.Data.Table;
using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class FieldProperty : IControlProperties
    {
        public FieldProperty(Control control) : base(control) { }

        public override string Name => "Field";

        public override object DefaultValue => null;

        public override bool ChangeValue(object sender)
        {
            if (sender is TableData tableData)
            {
                using (var dialog = new FormEditFieldProperty() { TableData = tableData, SelectedField = Value as FieldData })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Value = dialog.SelectedField;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
