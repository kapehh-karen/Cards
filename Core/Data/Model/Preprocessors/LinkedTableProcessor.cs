using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.LinkedTableControl;
using System.Data;
using Core.Helper;
using System.Windows.Forms;
using Core.Forms.Main.CardForm;

namespace Core.Data.Model.Preprocessors
{
    public class LinkedTableProcessor : ILinkedTableProcessor
    {
        private LinkedTableControl control;
        private DataTable data;

        public override IDesignControl Control { get => control; set => control = value as LinkedTableControl; }

        public override void Attach()
        {
            base.Attach();

            if (control != null)
                control.KeyDown += Control_KeyDown;
        }

        public override void Detach()
        {
            if (control != null)
                control.KeyDown -= Control_KeyDown;
        }

        public override void Load()
        {
            if (ModelLinkedTable == null)
                return;
            
            if (data == null)
            {
                data = new DataTable();
                ModelLinkedTable.Table.Table.Fields.ForEach(f => data.Columns.Add(f.Name, FieldHelper.GetTypeFromField(f)));
                control.DataSource = data;
            }
            else
            {
                data.Clear();
            }

            ModelLinkedTable.Items.ForEach(item =>
            {
                var row = data.NewRow();
                row[item.ID.Field.Name] = item.ID.Value;
                item.FieldValues.ForEach(fv => row[fv.Field.Name] = fv.ToDataGridValue());
                data.Rows.Add(row);
            });

            ModelLinkedTable.Table.Table.Fields.ForEach(field =>
            {
                // TODO: Может использовать свойство FieldData.Visible для определения видимости, а не так костыльно
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

        public object SelectedID => control.CurrentRow == null
            ? null
            : control.Rows[control.CurrentRow.Index].Cells[ModelLinkedTable.Table.Table.IdentifierField.Name].Value;

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: Сделать хорошо!
            using (var dialog = new FormCardView() { Table = ModelLinkedTable.Table.Table, Base = Base, IsLinkedModel = true })
            {
                // TODO: Возможно передавать клон модели и заменять в списке его
                var selectedId = SelectedID;
                var model = ModelLinkedTable.Items.FirstOrDefault(cm => cm.ID.Value.Equals(selectedId));

                if (model == null)
                    return;

                dialog.InitializeModel(model.Clone() as CardModel);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var index = ModelLinkedTable.Items.IndexOf(model);
                    ModelLinkedTable.Items[index] = dialog.Model;
                    
                    Load();
                }
            }
        }
    }
}
