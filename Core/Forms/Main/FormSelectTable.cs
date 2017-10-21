using Core.Config;
using Core.Data.Base;
using Core.Data.Table;
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
            if (!Directory.Exists("BASE"))
                Directory.CreateDirectory("BASE");

            foreach (var fileName in Directory.GetFiles("BASE")
                                              .Where(fname => Path.GetExtension(fname).Equals(".cards", StringComparison.CurrentCultureIgnoreCase)))
            {
                cmbConfigs.Items.Add(fileName);
            }
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
