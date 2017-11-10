using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.CardForm
{
    public partial class FormSelectInClassificator : Form
    {
        private TableData table;
        private DataBase mainBase;

        public FormSelectInClassificator()
        {
            InitializeComponent();
        }

        public FieldData Field { get; set; }

        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                tableDataGridView1.Table = table;
                Text = $"Классификатор - {table?.DisplayName}";
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

        public object SelectedID
        {
            get => tableDataGridView1.SelectedID;
            set => tableDataGridView1.SelectedID = value;
        }

        public CardModel Model => tableDataGridView1.SelectedModel;

        private void FormSelectInClassificator_Load(object sender, EventArgs e)
        {

        }

        private void tableDataGridView1_PressedEnter(object sender, KeyEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tableDataGridView1.FillTable(true);
        }
    }
}
