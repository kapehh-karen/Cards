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
    public class FieldProperty : IControlProperty
    {
        public FieldProperty(Control control) : base(control) { }

        public override string Name => "Field";

        public override string DisplayName => "Поле таблицы";

        public override object DefaultValue => null;

        public FieldType[] AccessTypes { get; set; }

        public override bool ChangeValue(object sender)
        {
            if (sender is TableData tableData)
            {
                using (var dialog = new FormEditFieldProperty(AccessTypes)
                {
                    TableData = tableData,
                    SelectedField = Value as FieldData
                })
                {
                    var res = dialog.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        Value = dialog.SelectedField;
                        return true;
                    }
                    else if (res == DialogResult.Ignore)
                    {
                        Value = null;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
