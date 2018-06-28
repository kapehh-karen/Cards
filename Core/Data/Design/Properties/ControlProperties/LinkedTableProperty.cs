using Core.Data.Table;
using Core.Forms.Design.FormProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Properties.ControlProperties
{
    public class LinkedTableProperty : IControlProperty
    {
        public LinkedTableProperty(Control control) : base(control) { }

        public override string Name => "LinkedTable";

        public override string DisplayName => "Внешние данные";

        public override object DefaultValue => null;

        public override bool ChangeValue(object sender)
        {
            if (sender is TableData tableData)
            {
                using (var dialog = new FormEditLinkedTable()
                {
                    TableData = tableData,
                    SelectedLinkedTable = tableData.GetLinkedTableByName(Value as string)
                })
                {
                    var res = dialog.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        Value = dialog.SelectedLinkedTable?.Name;
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

