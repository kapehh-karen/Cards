using Core.Config;
using Core.Data.Base;
using Core.Data.Table;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormSelectTable : Form
    {
        private class ComboBoxTableItem
        {
            public TableData Table { get; set; }

            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public FormSelectTable()
        {
            InitializeComponent();
        }

        public DataBase SelectedDataBase { get; set; }

        public TableData SelectedTableData { get; set; }

        private void FormSelectTable_Load(object sender, EventArgs e)
        {
            cmbConfigs.Items.AddRange(FileSystemHelper.GetFilesFromFolder(Consts.DirectoryBase, Consts.ConfigBaseExtension).ToArray());
        }

        private void cmbConfigs_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combo && combo.SelectedItem != null)
            {
                SelectedDataBase = new Configuration<DataBase>().ReadFromFile(combo.SelectedItem.ToString());

                lbTables.Items.Clear();
                SelectedDataBase?.Tables.ForEach(t => lbTables.Items.Add(new ComboBoxTableItem() { Table = t, Text = t.DisplayName }));
            }
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            if (lbTables.SelectedItem is ComboBoxTableItem item && item != null)
            {
                SelectedTableData = item.Table;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
