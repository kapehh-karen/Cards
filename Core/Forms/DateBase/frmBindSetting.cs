using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Core.Data.Base;
using Core.Data.Table;
using Core.Config;
using Core.Data.Field;

namespace Core.Forms.DateBase
{
    public partial class frmBindSetting : Form
    {
        private DataBaseConfigLoader dataBaseConfigLoader;
        private DataBase _dataBase;
        private TableData _tableData;
        private bool hasChanged = false;

        public frmBindSetting()
        {
            InitializeComponent();
        }

        private void SelectDB(string fileName)
        {
            dataBaseConfigLoader = new DataBaseConfigLoader(fileName);
            _dataBase = dataBaseConfigLoader.Load();

            gbDateBase.Enabled = true;
            gbDateBase.Text = $"База - {fileName}";

            cmbTables.Items.Clear();
            _dataBase.Tables.ForEach(td => cmbTables.Items.Add(td));
        }

        private void RedrawFields(TableData tableData)
        {
            lvFields.Items.Clear();

            foreach (var fieldData in tableData.Fields)
            {
                var lvi = new ListViewItem();
                lvi.Text = fieldData.IsIdentifier ? "*" : "";
                lvi.SubItems.Add(fieldData.Name);
                lvi.SubItems.Add(fieldData.Type != FieldType.BIND ? fieldData.Type.ToString() : fieldData.BindData?.ToString());
                lvi.SubItems.Add(fieldData.Visible ? "Да" : "Нет");
                lvi.SubItems.Add(fieldData.Required ? "Да" : "Нет");
                lvi.Tag = fieldData;

                lvFields.Items.Add(lvi);
            }
        }

        private void RedrawLinkedData(TableData tableData)
        {
            lvDataList.Items.Clear();

            foreach (var linkedTable in tableData.LinkedTables)
            {
                var lvi = new ListViewItem();
                lvi.Text = linkedTable.Table.Name;
                lvi.SubItems.Add(linkedTable.Field.Name);
                lvi.Tag = linkedTable;

                lvDataList.Items.Add(lvi);
            }
        }

        private void SelectTable(TableData tableData)
        {
            gbDateTable.Enabled = true;
            gbDateTable.Text = $"Таблица - {tableData.Name}";

            var idField = tableData.IdentifierField;

            cmbIDField.Items.Clear();
            tableData.Fields.ForEach(fd => cmbIDField.Items.Add(fd));

            // if ID field not exists, try find it
            if (idField == null)
            {
                idField = tableData.Fields.FirstOrDefault(fd => fd.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase));
                
                if (idField != null)
                {
                    idField.IsIdentifier = true;
                    cmbIDField.SelectedItem = idField;
                }
            }
            else
            {
                cmbIDField.SelectedValueChanged -= cmbIDField_SelectedValueChanged;
                cmbIDField.SelectedItem = idField;
                cmbIDField.SelectedValueChanged += cmbIDField_SelectedValueChanged;
            }
            
            RedrawFields(tableData);
            RedrawLinkedData(tableData);

            // is classificator
            checkClassif.CheckedChanged -= checkClassif_CheckedChanged;
            checkClassif.Checked = tableData.IsClassifier;
            checkClassif.CheckedChanged += checkClassif_CheckedChanged;
        }

        private void frmBindSetting_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("BASE"))
                Directory.CreateDirectory("BASE");

            foreach (var fileName in Directory.GetFiles("BASE")
                                              .Where(fname => Path.GetExtension(fname).Equals(".mdb", StringComparison.CurrentCultureIgnoreCase)))
            {
                cmbBasesList.Items.Add(fileName);
            }
        }

        private void cmbBasesList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBasesList.SelectedItem == null)
                return;

            SelectDB(cmbBasesList.SelectedItem.ToString());
        }

        private void cmbTables_SelectedValueChanged(object sender, EventArgs e)
        {
            var td = cmbTables.SelectedItem as TableData;

            if (td == null)
                return;

            _tableData = td;
            SelectTable(td);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DoCancel();
        }

        private void btnSaveApply_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void cmbIDField_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbIDField.SelectedItem == null)
                return;

            var fieldData = cmbIDField.SelectedItem as FieldData;
            _tableData.Fields.ForEach(fd => fd.IsIdentifier = fd == fieldData);
            hasChanged = true;

            RedrawFields(_tableData);
        }

        private void checkClassif_CheckedChanged(object sender, EventArgs e)
        {
            _tableData.IsClassifier = checkClassif.Checked;
            hasChanged = true;
        }

        private void lvFields_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;

            if (e.KeyCode == Keys.Enter && listView.SelectedItems.Count == 1)
            {
                var lvi = listView.SelectedItems[0];
                var field = lvi.Tag as FieldData;
                var frmDialog = new frmChangeFieldData() { Field = field, Base = _dataBase };

                if (frmDialog.ShowDialog() == DialogResult.OK)
                {
                    field.Type = frmDialog.SelectedType;
                    field.Visible = frmDialog.SelectedVisible;
                    field.Required = frmDialog.SelectedRequire;
                    field.BindData = frmDialog.SelectedBindField;
                    hasChanged = true;

                    RedrawFields(_tableData);
                }
            }
        }

        private void frmBindSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && hasChanged)
            {
                e.Cancel = DoCancel();
            }
        }

        private bool DoCancel()
        {
            if (hasChanged)
            {
                if (MessageBox.Show("Вы уверены? Все несохраненные изменения будут утеряны.",
                        "Предупреждение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return true;
                }
            }

            hasChanged = false;
            this.DialogResult = DialogResult.Cancel;
            return false;
        }

        private void DoSave()
        {
            if (_dataBase == null)
                return;

            // check IDs
            var tableDataWithoutID = _dataBase.Tables.FirstOrDefault(td => td.IdentifierField == null);
            if (tableDataWithoutID != null)
            {
                MessageBox.Show($"Не выбрано поле идентификатора в таблице \"{tableDataWithoutID.Name}\".", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check BindData
            foreach (var td in _dataBase.Tables)
            {
                foreach (var fd in td.Fields)
                {
                    if (fd.Type != FieldType.BIND)
                        continue;

                    if (fd.BindData == null || fd.BindData.Table == null || fd.BindData.Field == null)
                    {
                        MessageBox.Show($"В таблице \"{td.Name}\" у связанного поля \"{fd.Name}\" отсутствует информация о таблице и поле",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // SUCCESSFUL!!! Save it to *.conf file

            if (dataBaseConfigLoader != null)
                dataBaseConfigLoader.Save(_dataBase);

            hasChanged = false;
            this.DialogResult = DialogResult.OK;
        }

        private void lvDataList_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            BindField bindField;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (listView.SelectedItems.Count == 1)
                    {
                        var lvi = listView.SelectedItems[0];
                        bindField = lvi.Tag as BindField;
                        var formDialogChange = new frmChangeLinkedTable() { BindData = bindField, Base = _dataBase };

                        if (formDialogChange.ShowDialog() == DialogResult.OK)
                        {
                            bindField.Table = formDialogChange.SelectedTable;
                            bindField.Field = formDialogChange.SelectedField;
                            hasChanged = true;
                        }
                    }
                    break;

                case Keys.Delete:
                    if (listView.SelectedItems.Count == 1)
                    {
                        var lvi = listView.SelectedItems[0];
                        bindField = lvi.Tag as BindField;

                        if (MessageBox.Show($"Удалить связь?\r\n\r\n{bindField}", "Подтверждение",
                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            _tableData.LinkedTables.Remove(bindField);
                            hasChanged = true;
                        }
                    }
                    break;

                case Keys.Insert:
                    bindField = new BindField();
                    var formDialogNew = new frmChangeLinkedTable() { Base = _dataBase };

                    if (formDialogNew.ShowDialog() == DialogResult.OK)
                    {
                        bindField.Table = formDialogNew.SelectedTable;
                        bindField.Field = formDialogNew.SelectedField;

                        _tableData.LinkedTables.Add(bindField);
                        hasChanged = true;
                    }
                    break;
            }

            RedrawLinkedData(_tableData);
        }
    }
}
