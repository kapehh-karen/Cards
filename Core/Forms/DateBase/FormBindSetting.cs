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
using Core.Helper;

namespace Core.Forms.DateBase
{
    public partial class FormBindSetting : Form
    {
        private DataBase _dataBase;
        private TableData _tableData;
        private bool initializeChanges = false;

        public FormBindSetting()
        {
            InitializeComponent();
        }

        public CardsFile CardsLoader { get; set; }
        
        private void LoadDBInfo()
        {
            _dataBase = new DataBaseLoader(CardsLoader.Base).RuntimeBase;

            gbDateBase.Enabled = _dataBase.IsConnected;
            gbDateBase.Text = $"База: Server={_dataBase.Sever ?? "***"}, Port={_dataBase.Port}, User={_dataBase.UserName ?? "***"}, Password={_dataBase.Password ?? "***"}, Basename={_dataBase.BaseName ?? "***"}";
            
            cmbTables.Items.Clear();
            _dataBase.Tables.OrderBy(table => table.Name).ForEach(td => cmbTables.Items.Add(td));

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

        private void RedrawFields(TableData tableData, bool restoreTop = true)
        {
            var topItemIndex = lvFields.TopItem?.Index ?? -1;
            lvFields.BeginUpdate();
            lvFields.Items.Clear();

            foreach (var fieldData in tableData.Fields.OrderBy(field => field.Name))
            {
                var lvi = new ListViewItem();
                lvi.Text = fieldData.IsIdentifier ? "*" : "";
                lvi.SubItems.Add(fieldData.Name);
                lvi.SubItems.Add(fieldData.Type != FieldType.BIND ? fieldData.Type.ToString() : fieldData.BindData?.ToString());
                lvi.SubItems.Add(fieldData.Visible ? "Да" : "-");
                lvi.SubItems.Add(fieldData.Required ? "Да" : "-");
                lvi.SubItems.Add(fieldData.DisplayName);
                lvi.Tag = fieldData;

                lvFields.Items.Add(lvi);
            }

            lvFields.EndUpdate();
            if (restoreTop && topItemIndex >= 0)
                lvFields.TopItem = lvFields.Items[topItemIndex];
        }

        private void RedrawLinkedData(TableData tableData, bool restoreTop = true)
        {
            var topItemIndex = lvDataList.TopItem?.Index ?? -1;
            lvDataList.BeginUpdate();
            lvDataList.Items.Clear();

            foreach (var linkedTable in tableData.LinkedTables.OrderBy(lt => lt.Table.Name))
            {
                var lvi = new ListViewItem();
                lvi.Text = linkedTable.Table != null ? linkedTable.Table.Name : " - Пусто - ";
                lvi.SubItems.Add(linkedTable.Field != null ? linkedTable.Field.Name : " - Пусто - ");
                lvi.SubItems.Add(linkedTable.Required ? "Да" : "-");
                lvi.Tag = linkedTable;

                lvDataList.Items.Add(lvi);
            }

            lvDataList.EndUpdate();
            if (restoreTop && topItemIndex >= 0)
                lvDataList.TopItem = lvDataList.Items[topItemIndex];
        }

        private void SelectTable(TableData tableData)
        {
            initializeChanges = true;

            gbDateTable.Enabled = true;
            gbDateTable.Text = $"Таблица - {tableData.Name}";

            var idField = tableData.IdentifierField;

            cmbIDField.Items.Clear();
            tableData.Fields.OrderBy(field => field.Name).ForEach(fd => cmbIDField.Items.Add(fd));

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
            
            RedrawFields(tableData, false);
            RedrawLinkedData(tableData, false);
            
            checkClassif.Checked = tableData.IsClassifier;
            txtTableDisplayName.Text = tableData.DisplayName;
            checkVisible.Checked = tableData.Visible;

            initializeChanges = false;
        }

        private void frmBindSetting_Load(object sender, EventArgs e)
        {
            this.Text = $"Настройки БД - {CardsLoader.ShortFileName}";
            LoadDBInfo();
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
            this.Close();
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

            RedrawFields(_tableData);
        }

        private void checkClassif_CheckedChanged(object sender, EventArgs e)
        {
            if (initializeChanges)
                return;

            _tableData.IsClassifier = checkClassif.Checked;
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

                    RedrawFields(_tableData);
                }
            }
        }

        private void frmBindSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Вы уверены? Все несохраненные изменения будут утеряны.",
                    Consts.ProgramTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void DoSave()
        {
            if (_dataBase == null)
                return;

            // check IDs
            var tableDataWithoutID = _dataBase.Tables.FirstOrDefault(td => td.IdentifierField == null);
            if (tableDataWithoutID != null)
            {
                MessageBox.Show($"Не выбрано поле идентификатора в таблице \"{tableDataWithoutID.Name}\".", Consts.ProgramTitle,
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
                            Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                foreach (var lt in td.LinkedTables)
                {
                    if (lt.Field == null || lt.Table == null)
                    {
                        MessageBox.Show($"В таблице \"{td.Name}\" имеются внешние данные, у которых отсутствует информация о таблице или поле",
                            Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // TODO
            CardsLoader.Save();
            MessageBox.Show("Конфигурация успешно сохранена!", Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lvDataList_KeyUp(object sender, KeyEventArgs e)
        {
            var listView = sender as ListView;
            LinkedTable linkedTable;
            var needRedraw = false;

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
                            linkedTable.Required = formDialogChange.SelectedRequire;
                        }
                    }

                    needRedraw = true;
                    break;

                case Keys.Delete:
                    if (listView.SelectedItems.Count == 1)
                    {
                        var lvi = listView.SelectedItems[0];
                        linkedTable = lvi.Tag as LinkedTable;

                        if (MessageBox.Show($"Удалить связь?\r\n\r\n{linkedTable}", Consts.ProgramTitle,
                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            _tableData.LinkedTables.Remove(linkedTable);
                        }
                    }

                    needRedraw = true;
                    break;

                case Keys.Insert:
                    linkedTable = new LinkedTable();
                    var formDialogNew = new FormChangeLinkedTable() { Base = _dataBase };

                    if (formDialogNew.ShowDialog() == DialogResult.OK)
                    {
                        linkedTable.Table = formDialogNew.SelectedTable;
                        linkedTable.Field = formDialogNew.SelectedField;
                        linkedTable.Required = formDialogNew.SelectedRequire;

                        _tableData.LinkedTables.Add(linkedTable);
                    }

                    needRedraw = true;
                    break;
            }

            if (needRedraw)
                RedrawLinkedData(_tableData);
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            using (var formDesign = new FormDesigner() { FormData = _tableData.Form, TableData = _tableData })
            {
                if (formDesign.ShowDialog() == DialogResult.OK)
                {
                    _tableData.Form = formDesign.FormData;
                }
            }
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {

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

                    CardsLoader.Base = _dataBase;
                    LoadDBInfo();
                }
            }
        }

        private void txtTableDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (initializeChanges)
                return;

            _tableData.DisplayName = txtTableDisplayName.Text;
        }

        private void checkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (initializeChanges)
                return;

            _tableData.Visible = checkVisible.Checked;
        }

        private void btnFormRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Удалить форму в таблице \"{_tableData.Name}\"?", Consts.ProgramTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                _tableData.Form = null;
            }
        }
    }
}
