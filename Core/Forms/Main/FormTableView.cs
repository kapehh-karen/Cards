using Core.Data.Base;
using Core.Data.Table;
using Core.Forms.Main.CardForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormTableView : Form
    {
        private TableData table;
        private DataBase mainBase;

        public FormTableView()
        {
            InitializeComponent();
        }

        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                tableDataGridView1.Table = table;
                Text = $"Таблица - {table?.DisplayName}";
            }
        }

        public DataBase Base
        {
            get => mainBase;
            set
            {
                mainBase = value;
                tableDataGridView1.Base = Base;
            }
        }

        public void FillTable()
        {
            tableDataGridView1.FillTable();
        }

        private void FormTableView_Load(object sender, EventArgs e)
        {

        }

        private void tableDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is TableDataGridView gridView && gridView.CurrentRow != null)
            {
                //Text = gridView.RowCount.ToString();
                //Text = gridView.GetCurrentID().ToString();
                //this.Text = string.Join(" / ", (from DataGridViewCell col in gridView.SelectedCells select col.OwningColumn.Name).Distinct().ToArray());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        private void btnOpenForm_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormCardView() { Table = this.Table, Base = this.Base })
            {
                dialog.InitializeModel();
                dialog.ShowDialog();
            }
        }

        private void tableDataGridView1_PressedEnter(object sender, KeyEventArgs e)
        {
            using (var dialog = new FormCardView() { Table = this.Table, Base = this.Base })
            {
                var selectedID = tableDataGridView1.SelectedID;
                dialog.InitializeModel(selectedID);
                dialog.ShowDialog();
                tableDataGridView1.SelectedID = selectedID;
                FillTable();
            }
        }
    }
}
