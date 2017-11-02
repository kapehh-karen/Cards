using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.LinkedTableControl;
using System.Data;
using Core.Helper;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors
{
    public class LinkedTableProcessor : ILinkedTableProcessor
    {
        private LinkedTableControl control;
        private DataTable data;

        public override IDesignControl Control { get => control; set => control = value as LinkedTableControl; }

        public override void Load()
        {
            if (ModelLinkedTable == null)
                return;

            data = new DataTable();
            ModelLinkedTable.Table.Table.Fields.ForEach(f => data.Columns.Add(f.Name, FieldHelper.GetTypeFromField(f)));
            ModelLinkedTable.Items.ForEach(item =>
            {
                var row = data.NewRow();
                row[item.ID.Field.Name] = item.ID.Value;
                item.FieldValues.ForEach(fv => row[fv.Field.Name] = fv.ToDataGridValue());
                data.Rows.Add(row);
            });

            control.DataSource = data;

            ModelLinkedTable.Table.Table.Fields.ForEach(field =>
            {
                // Hide ID and ForeignKey column
                if (field.IsIdentifier || field.Equals(ModelLinkedTable.Table.Field))
                {
                    control.Columns[field.Name].Visible = false;
                    return;
                }

                // Renaming columns header
                var column = control.Columns[field.Name];
                column.HeaderText = field.DisplayName;
                column.Tag = field;
            });
        }
    }
}
