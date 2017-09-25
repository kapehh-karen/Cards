using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Core.Data.Config;
using Core.DataBase;
using Core.Data.Table;

namespace Core.Forms.DateBase
{
    public partial class frmBindSetting : Form
    {
        public frmBindSetting()
        {
            InitializeComponent();
        }

        private void SelectDB(string fileName)
        {
            var dataBaseConfigLoader = new DataBaseConfigLoader(fileName);
            var dbc = dataBaseConfigLoader.Load();

            gbDateBase.Enabled = true;
            gbDateBase.Text = $"База - {fileName}";

            cmbTables.Items.Clear();
            foreach (var item in dbc.Tables)
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
                if (idField == null && fieldData.Name.Equals("ID", StringComparison.InvariantCulture))
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

            // TODO: tableData.LinkedTables
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
    }
}
