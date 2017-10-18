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
using Core.Forms.Design;

namespace Core.Forms.DateBase
{
    public partial class FormBindSetting : Form
    {
        private DataBaseConfigLoader dataBaseConfigLoader;
        private DataBase _dataBase;
        private TableData _tableData;
        private bool hasChanged = false;
        private bool initializeChanges = false;

        public FormBindSetting()
        {
            InitializeComponent();
        }

        private void SelectDB(string fileName)
        {
            _dataBase = null;
            dataBaseConfigLoader = new DataBaseConfigLoader(fileName);
            LoadDBInfo();
        }

        private void LoadDBInfo()
        {
            _dataBase = dataBaseConfigLoader.Load(_dataBase);

            gbDateBase.Enabled = _dataBase.IsConnected;
            gbDateBase.Text = $"База: Server={_dataBase.Sever ?? "***"}, Port={_dataBase.Port}, User={_dataBase.UserName ?? "***"}, Password={_dataBase.Password ?? "***"}, Basename={_dataBase.BaseName ?? "***"}";
            
            cmbTables.Items.Clear();
            _dataBase.Tables.ForEach(td => cmbTables.Items.Add(td));

            ClearTableElementInfo();
        }

        private void ClearTableElementInfo()
        {
            gbDateTable.Enabled = false;
            gbDateTable.Text = string.Empty;
            checkClassif.Checked = false;
            txtTableDisplayName.Text = string.Empty;
            cmbIDField.Items.Clear();
            lvFields.Items.Clear();
            lvDataList.Items.Clear();
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
                lvi.SubItems.Add(fieldData.DisplayName);
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
            initializeChanges = true;

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
                cmbIDField.SelectedItem = idField;
            }
            
            RedrawFields(tableData);
            RedrawLinkedData(tableData);
            
            checkClassif.Checked = tableData.IsClassifier;
            txtTableDisplayName.Text = tableData.DisplayName;

            initializeChanges = false;
        }

        private void frmBindSetting_Load(object sender, EventArgs e)
        {
            FillDBList();
        }

        private void FillDBList()
        {
            if (!Directory.Exists("BASE"))
                Directory.CreateDirectory("BASE");

            cmbBasesList.Items.Clear();
            foreach (var fileName in Directory.GetFiles("BASE")
                                              .Where(fname => Path.GetExtension(fname).Equals(".cards", StringComparison.CurrentCultureIgnoreCase)))
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
            if (initializeChanges)
                return;

            if (cmbIDField.SelectedItem == null)
                return;

            var fieldData = cmbIDField.SelectedItem as FieldData;
            _tableData.Fields.ForEach(fd => fd.IsIdentifier = fd == fieldData);
            hasChanged = true;

            RedrawFields(_tableData);
        }

        private void checkClassif_CheckedChanged(object sender, EventArgs e)
        {
            if (initializeChanges)
                return;

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
                var frmDialog = new FormChangeFieldData() { Field = field, Base = _dataBase };

                if (frmDialog.ShowDialog() == DialogResult.OK)
                {
                    field.Type = frmDialog.SelectedType;
                    field.Visible = frmDialog.SelectedVisible;
                    field.Required = frmDialog.SelectedRequire;
                    field.BindData = frmDialog.SelectedBindField;
                    field.DisplayName = frmDialog.SelectedDisplayName;
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
            this.Close();
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
                        MessageBox.Show($"В таблице \"{td.Name}\" у связанного поля \"{fd.Name}\" отсутствует информация о таблице или поле",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                foreach (var lt in td.LinkedTables)
                {
                    if (lt.Field == null || lt.Table == null)
                    {
                        MessageBox.Show($"В таблице \"{td.Name}\" имеются внешние данные, у которых отсутствует информация о таблице или поле",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // SUCCESSFUL!!! Save it to *.conf file

            if (dataBaseConfigLoader != null)
            {
                dataBaseConfigLoader.Save(_dataBase);
                MessageBox.Show("Конфигурация успешно сохранена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            hasChanged = false;
            //this.Close();
        }

        private void lvDataList_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            LinkedTable linkedTable;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (listView.SelectedItems.Count == 1)
                    {
                        var lvi = listView.SelectedItems[0];
                        linkedTable = lvi.Tag as LinkedTable;
                        var formDialogChange = new FormChangeLinkedTable() { BindData = linkedTable, Base = _dataBase };

                        if (formDialogChange.ShowDialog() == DialogResult.OK)
                        {
                            linkedTable.Table = formDialogChange.SelectedTable;
                            linkedTable.Field = formDialogChange.SelectedField;
                            hasChanged = true;
                        }
                    }
                    break;

                case Keys.Delete:
                    if (listView.SelectedItems.Count == 1)
                    {
                        var lvi = listView.SelectedItems[0];
                        linkedTable = lvi.Tag as LinkedTable;

                        if (MessageBox.Show($"Удалить связь?\r\n\r\n{linkedTable}", "Подтверждение",
                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            _tableData.LinkedTables.Remove(linkedTable);
                            hasChanged = true;
                        }
                    }
                    break;

                case Keys.Insert:
                    linkedTable = new LinkedTable();
                    var formDialogNew = new FormChangeLinkedTable() { Base = _dataBase };

                    if (formDialogNew.ShowDialog() == DialogResult.OK)
                    {
                        linkedTable.Table = formDialogNew.SelectedTable;
                        linkedTable.Field = formDialogNew.SelectedField;

                        _tableData.LinkedTables.Add(linkedTable);
                        hasChanged = true;
                    }
                    break;
            }

            RedrawLinkedData(_tableData);
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            using (var formDesign = new FormDesigner() { FormData = _tableData.Form, TableData = _tableData })
            {
                if (formDesign.ShowDialog() == DialogResult.OK)
                {
                    _tableData.Form = formDesign.FormData;
                    hasChanged = true;
                }
            }
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {
            using (var dialogFile = new SaveFileDialog()
            {
                Filter = "CARDS Config|*.cards",
                InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "BASE")
            })
            {
                if (dialogFile.ShowDialog() != DialogResult.OK)
                    return;

                if (!File.Exists(dialogFile.FileName))
                {
                    File.Create(dialogFile.FileName);

                    FillDBList();
                }
            }
        }

        private void btnEditDB_Click(object sender, EventArgs e)
        {
            if (_dataBase == null)
                return;

            using (var dialog = new FormEditConnection()
            {
                Server = _dataBase.Sever,
                Port = _dataBase.Port,
                UserName = _dataBase.UserName,
                Password = _dataBase.Password,
                BaseName = _dataBase.BaseName
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _dataBase.Sever = dialog.Server;
                    _dataBase.Port = dialog.Port;
                    _dataBase.UserName = dialog.UserName;
                    _dataBase.Password = dialog.Password;
                    _dataBase.BaseName = dialog.BaseName;

                    hasChanged = true;

                    LoadDBInfo();
                }
            }
        }

        private void txtTableDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (initializeChanges)
                return;

            _tableData.DisplayName = txtTableDisplayName.Text;
            hasChanged = true;
        }
    }
}
