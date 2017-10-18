using Core.Data.Base;
using Core.Data.Table;
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

                if (table != null)
                {
                    Text = $"Таблица - {table.Name}";

                    tableDataGridView1.Base = Base;
                    tableDataGridView1.Table = table;
                    tableDataGridView1.FillTable();
                }
            }
        }

        public DataBase Base { get; set; }

        private void FormTableView_Load(object sender, EventArgs e)
        {

        }

        private void tableDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is TableDataGridView gridView && gridView.CurrentRow != null)
            {
                Text = gridView.Rows[gridView.CurrentRow.Index].Cells[Table.IdentifierField.Name].Value.ToString();
                //this.Text = string.Join(" / ", (from DataGridViewCell col in gridView.SelectedCells select col.OwningColumn.Name).Distinct().ToArray());
            }
        }
    }
}
