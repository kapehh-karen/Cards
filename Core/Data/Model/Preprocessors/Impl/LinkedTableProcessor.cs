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
using Core.Forms.Main;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class LinkedTableProcessor : ILinkedTableProcessor
    {
        private List<CardModel> displayedItems;
        private LinkedTableControl control;
        private DataTable data;

        public override IDesignControl Control { get => control; set => control = value as LinkedTableControl; }

        public override void Attach()
        {
            if (control != null)
            {
                control.PressedKey += Control_KeyDown;
                control.PressedClick += Control_KeyDown;
                control.DataBindingComplete += Control_DataBindingComplete;
            }
        }

        public override void Detach()
        {
            if (control != null)
            {
                control.PressedKey -= Control_KeyDown;
                control.PressedClick -= Control_KeyDown;
                control.DataBindingComplete -= Control_DataBindingComplete;
            }
        }

        public override void Load()
        {
            if (ModelLinkedTable == null)
                return;

            var initData = false;

            // Для сохраненных настроек столбцов
            control.Table = ModelLinkedTable.LinkedTable.Table;
            
            if (data == null)
            {
                data = new DataTable();
                ModelLinkedTable.LinkedTable.Table.Fields.ForEach(field => data.Columns.Add(field.Name, FieldHelper.GetTypeFromField(field)));
                initData = true;
            }
            else
            {
                data.Clear();
            }

            displayedItems = ModelLinkedTable.Items.Where(item => item.LinkedState != ModelLinkedItemState.DELETED).ToList();
            displayedItems.ForEach(item =>
            {
                var row = data.NewRow();
                row[item.ID.Field.Name] = item.ID.Value ?? DBNull.Value;
                item.FieldValues.ForEach(fv => row[fv.Field.Name] = fv.ToDataGridValue() ?? DBNull.Value);
                data.Rows.Add(row);
            });

            data.AcceptChanges();

            if (initData)
            {
                // После всех манипуляций делаем это
                control.DataSource = data;
            }
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

                var model = displayedItems[selectedIndex];
                if (model == null)
                    return;

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        using (var dialog = new FormCardView() { Table = ModelLinkedTable.LinkedTable.Table, Base = Base, IsLinkedModel = true })
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
                using (var dialog = new FormCardView() { Table = ModelLinkedTable.LinkedTable.Table, Base = Base, IsLinkedModel = true })
                {
                    dialog.InitializeModel();

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var newModel = dialog.Model;
                        ModelLinkedTable.Items.Add(newModel);
                        newModel.LinkedState = ModelLinkedItemState.ADDED;
                        Load();
                    }
                }
            }
        }
        
        private void Control_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            control.ClearSelection();
        }
    }
}
