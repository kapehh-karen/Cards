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
        private DataBase dataBase;
        private TableData tableData;
        private bool hasChanged = false;

        public frmBindSetting()
        {
            InitializeComponent();
        }

        private void SelectDB(string fileName)
        {
            dataBaseConfigLoader = new DataBaseConfigLoader(fileName);
            dataBase = dataBaseConfigLoader.Load();

            gbDateBase.Enabled = true;
            gbDateBase.Text = $"База - {fileName}";

            cmbTables.Items.Clear();
            dataBase.Tables.ForEach(td => cmbTables.Items.Add(td));
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
            foreach (var fileName in Directory.GetFiles(".").Where(fname => Path.GetExtension(fname).Equals(".mdb")))
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

            tableData = td;
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
            tableData.Fields.ForEach(fd => fd.IsIdentifier = fd == fieldData);
            hasChanged = true;

            RedrawFields(tableData);
        }

        private void checkClassif_CheckedChanged(object sender, EventArgs e)
        {
            tableData.IsClassifier = checkClassif.Checked;
            hasChanged = true;
        }

        private void lvFields_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lvFields.SelectedItems.Count == 1)
            {
                var lvi = lvFields.SelectedItems[0];
                var field = lvi.Tag as FieldData;
                var frmDialog = new frmChangeFieldData() { Field = field, Base = dataBase };

                if (frmDialog.ShowDialog() == DialogResult.OK)
                {
                    field.Type = frmDialog.SelectedType;
                    field.Visible = frmDialog.SelectedVisible;
                    field.Required = frmDialog.SelectedRequire;
                    field.BindData = frmDialog.SelectedBindField;
                    hasChanged = true;

                    RedrawFields(tableData);
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
            // check IDs
            var tableDataWithoutID = dataBase.Tables.FirstOrDefault(td => td.IdentifierField == null);
            if (tableDataWithoutID != null)
            {
                MessageBox.Show($"Не выбрано поле идентификатора в таблице \"{tableDataWithoutID.Name}\".", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check BindData
            foreach (var td in dataBase.Tables)
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
                dataBaseConfigLoader.Save(dataBase);

            this.DialogResult = DialogResult.OK;
        }
    }
}
