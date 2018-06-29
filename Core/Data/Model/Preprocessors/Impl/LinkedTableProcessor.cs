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
using System.Drawing;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class LinkedTableProcessor : ILinkedTableProcessor
    {
        // Генерируем случайное имя поля, чтобы оно случайно не совпало с реальным (мало ли)
        private readonly string internalIndexField = $"IndexField_{DateTime.Now.Ticks.ToString("x")}";

        private List<CardModel> displayedItems;
        private LinkedTableControl control;

        public override IDesignControl Control
        {
            get => control;
            set => control = value as LinkedTableControl;
        }

        public override void Attach()
        {
            if (control != null)
            {
                control.PressedKey += Control_KeyDown;
                control.PressedClick += Control_KeyDown;
                control.DataBindingComplete += Control_DataBindingComplete;
                control.TableStorageInformationUpdated += Control_TableStorageInformationUpdated;
            }
        }

        public override void Detach()
        {
            if (control != null)
            {
                control.PressedKey -= Control_KeyDown;
                control.PressedClick -= Control_KeyDown;
                control.DataBindingComplete -= Control_DataBindingComplete;
                control.TableStorageInformationUpdated -= Control_TableStorageInformationUpdated;
            }
        }

        public override void Load()
        {
            if (ModelLinkedTable == null)
                return;

            // Для сохраненных настроек столбцов
            control.Table = ModelLinkedTable.LinkedTable.Table;

            var data = new DataTable();
            ModelLinkedTable.LinkedTable.Table.Fields.ForEach(field => data.Columns.Add(field.Name, FieldHelper.GetTypeFromField(field)));
            data.Columns.Add(internalIndexField, typeof(Int32));
            displayedItems = ModelLinkedTable.Items.Where(item => item.LinkedState != ModelLinkedItemState.DELETED).ToList();
            for (var i = 0; i < displayedItems.Count; i++)
            {
                var item = displayedItems[i];
                var row = data.NewRow();
                row[item.ID.Field.Name] = item.ID.Value ?? DBNull.Value;
                item.FieldValues.ForEach(fv => row[fv.Field.Name] = fv.DisplayValue ?? DBNull.Value);
                row[internalIndexField] = i; // По индексу будем обращаться к элементу в displayedItems
                data.Rows.Add(row);
            }
            data.AcceptChanges();

            // После всех манипуляций делаем это
            control.DataSource = data;
        }

        public int SelectedIndex => control.CurrentRow == null ? -1 : (int)control.CurrentRow.Cells[internalIndexField].Value;

        private void Control_TableStorageInformationUpdated(object sender, EventArgs e)
        {
            Load();
        }

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
                        var dialog = ModelLinkedTable.LinkedTable.Table.CardView;
                        dialog.IsLinkedModel = true;
                        dialog.InitializeModel(model);
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            var newModel = dialog.Model;
                            var index = ModelLinkedTable.Items.IndexOf(model);
                            ModelLinkedTable.Items[index] = newModel;
                            Load();
                            OnValueChanged(this);
                        }
                        break;

                    case Keys.Delete:
                        if (MessageBox.Show("Удалить запись?", Consts.ProgramTitle,
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            model.CheckDeleteFull();
                            Load();
                            OnValueChanged(this);
                        }
                        break;
                }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                var dialog = ModelLinkedTable.LinkedTable.Table.CardView;
                dialog.IsLinkedModel = true;
                dialog.InitializeModel();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var newModel = dialog.Model;
                    ModelLinkedTable.Items.Add(newModel);
                    newModel.LinkedState = ModelLinkedItemState.ADDED;
                    Load();
                    OnValueChanged(this);
                }
            }
        }

        private void Control_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            control.ClearSelection();
        }
    }
}
