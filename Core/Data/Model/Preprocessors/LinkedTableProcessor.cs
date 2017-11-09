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
using Core.Data.Field;

namespace Core.Data.Model.Preprocessors
{
    public class LinkedTableProcessor : ILinkedTableProcessor
    {
        private LinkedTableControl control;
        private DataTable data;
        private FieldData fieldId;

        public override IDesignControl Control { get => control; set => control = value as LinkedTableControl; }

        public override void Attach()
        {
            base.Attach();

            if (control != null)
                control.PressedKey += Control_KeyDown;
        }

        public override void Detach()
        {
            if (control != null)
                control.PressedKey -= Control_KeyDown;
        }

        public override void Load()
        {
            if (ModelLinkedTable == null)
                return;

            fieldId = ModelLinkedTable.Table.Table.IdentifierField;

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

            ModelLinkedTable.Items.Where(item => item.LinkedState != ModelLinkedItemState.DELETED).ForEach(item =>
            {
                var row = data.NewRow();
                row[item.ID.Field.Name] = item.ID.Value ?? DBNull.Value;
                item.FieldValues.ForEach(fv => row[fv.Field.Name] = fv.ToDataGridValue() ?? DBNull.Value);
                data.Rows.Add(row);
            });

            data.AcceptChanges();

            ModelLinkedTable.Table.Table.Fields.ForEach(field =>
            {
                // Renaming columns header
                var column = control.Columns[field.Name];
                column.HeaderText = field.DisplayName;
                column.Tag = field;
                column.Visible = field.Visible;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            });
        }

        public int SelectedIndex => control.CurrentRow == null ? -1 : control.CurrentRow.Index;

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModelLinkedTable == null)
                return;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete)
            {
                var selectedIndex = SelectedIndex;
                if (selectedIndex < 0)
                    return;

                var model = ModelLinkedTable.Items[selectedIndex];
                if (model == null)
                    return;

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        using (var dialog = new FormCardView() { Table = ModelLinkedTable.Table.Table, Base = Base, IsLinkedModel = true })
                        {
                            dialog.InitializeModel(model);

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                var newModel = dialog.Model;
                                var index = ModelLinkedTable.Items.IndexOf(model);
                                ModelLinkedTable.Items[index] = newModel;
                                Load();
                            }
                        }
                        break;

                    case Keys.Delete:
                        if (MessageBox.Show("Удалить запись?", "Подтверждение действия",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            model.CheckDeleteFull();
                            Load();
                        }
                        break;
                }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                using (var dialog = new FormCardView() { Table = ModelLinkedTable.Table.Table, Base = Base, IsLinkedModel = true })
                {
                    dialog.InitializeModel();

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var newModel = dialog.Model;
                        ModelLinkedTable.Items.Add(newModel);
                        newModel.LinkedState = ModelLinkedItemState.ADDED;
                        //newModel[ModelLinkedTable.Table.Field] = ParentModel.ID.Value;
                        Load();
                    }
                }
            }
        }
    }
}
