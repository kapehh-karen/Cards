using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
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
        private FieldData selectedField;

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
            UpdateUiText();
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
        
        private void tableDataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (tableDataGridView1.CurrentCell == null)
                return;

            selectedField = tableDataGridView1.CurrentCell.OwningColumn.GetTag().Field;
            lblSelectedCell.Text = $"по полю '{selectedField.DisplayName}'";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (selectedField == null)
                return;

            var text = textBox1.Text;

            if (tableDataGridView1.CurrentCell != null)
            {
                tableDataGridView1.KeepSelectedColumn = tableDataGridView1.CurrentCell.OwningColumn;
            }

            if (string.IsNullOrEmpty(text))
            {
                tableDataGridView1.CurrentDataView.RowFilter = string.Empty;
            }
            else
            {
                tableDataGridView1.CurrentDataView.RowFilter = $"Convert([{selectedField.Name}], System.String) like '%{EscapeLikeValue(text)}%'";
            }

            UpdateUiText();
        }

        private string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private void tableDataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Если символы не управляющие, то передаем их, а то иначе закроется FormCardView
            if (!char.IsControl(e.KeyChar))
            {
                textBox1.Focus();
                SendKeys.Send(e.KeyChar.ToString());
            }
        }

        private void FormSelectInClassificator_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    break;

                case Keys.Up:
                case Keys.Down:
                    tableDataGridView1.Focus();
                    SendKeys.Send($"{{{e.KeyCode}}}");
                    break;
            }
        }

        private void UpdateUiText()
        {
            toolStripStatusLabelAmount.Text = $"Всего записей: {tableDataGridView1.CurrentDataView.Count}";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableDataGridView1.FillTable(true);
            UpdateUiText();
        }

        private void useNullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }
    }
}
