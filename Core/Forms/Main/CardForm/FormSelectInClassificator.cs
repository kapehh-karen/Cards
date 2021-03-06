﻿using Core.Data.Base;
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
        private FieldData parentField;
        private FieldData selectedField;

        public FormSelectInClassificator()
        {
            InitializeComponent();
        }

        public FieldData Field
        {
            get => parentField;
            set
            {
                parentField = value;
                tableDataGridView1.ParentField = parentField;
            }
        }

        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                tableDataGridView1.Table = table;
                Text = table?.FullDisplayName;
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
        
        private void tableDataGridView1_PressedEnter(object sender, KeyEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void tableDataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (tableDataGridView1.CurrentCell == null)
                return;

            selectedField = tableDataGridView1.CurrentCell.OwningColumn.GetTag().Field;

            if (selectedField != null)
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
                tableDataGridView1.CurrentDataView.RowFilter = DataGridViewHelper.BuildRowFilter(selectedField, text);
            }

            UpdateUiText();
        }

        private void tableDataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send(e.KeyChar.ToString());
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
