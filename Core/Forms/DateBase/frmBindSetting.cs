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

namespace Core.Forms.DateBase
{
    public partial class frmBindSetting : Form
    {
        private DataBaseConfigLoader dataBaseConfigLoader;
        private DataBase dataBase;

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
            foreach (var item in dataBase.Tables)
            {
                cmbTables.Items.Add(item);
            }
        }

        private void SelectTable(TableData tableData)
        {
            gbDateTable.Enabled = true;
            gbDateTable.Text = $"Таблица - {tableData.Name}";

            var idField = tableData.IdentifierField;
            lvFields.Items.Clear();
            cmbIDField.Items.Clear();

            foreach (var fieldData in tableData.Fields)
            {
                if (idField == null && fieldData.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase))
                {
                    idField = fieldData;
                    fieldData.IsIdentifier = true;
                }

                var lvi = new ListViewItem();
                lvi.Text = fieldData.IsIdentifier ? "*" : "";
                lvi.SubItems.Add(fieldData.Name);
                lvi.SubItems.Add(fieldData.Type != Data.Field.FieldType.BIND ? fieldData.Type.ToString() : fieldData.BindData.ToString());
                lvi.SubItems.Add(fieldData.Visible ? "Да" : "Нет");
                lvi.SubItems.Add(fieldData.Required ? "Да" : "Нет");

                lvFields.Items.Add(lvi);
                cmbIDField.Items.Add(fieldData);
            }

            if (idField != null)
            {
                cmbIDField.SelectedItem = idField;
            }

            lvDataList.Items.Clear();
            foreach (var linkedTable in tableData.LinkedTables)
            {
                var lvi = new ListViewItem();
                lvi.Text = linkedTable.Table.Name;
                lvi.SubItems.Add(linkedTable.Field.Name);

                lvDataList.Items.Add(lvi);
            }
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

            SelectTable(td);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSaveApply_Click(object sender, EventArgs e)
        {
            // TODO: check ID fields for tables

            if (dataBaseConfigLoader != null)
                dataBaseConfigLoader.Save(dataBase);

            this.DialogResult = DialogResult.OK;
        }
    }
}
