using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
using Core.Filter.Forms;
using Core.Forms.Main.CardForm;
using Core.Helper;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
                Text = table?.FullDisplayName;
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
            toolStripStatusLabelAmount.Text = $"Всего записей: {tableDataGridView1.CurrentDataView.Count}";
        }

        private void FormTableView_Load(object sender, EventArgs e)
        {

        }

        private void tableDataGridView1_PressedKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    PerformChange();
                    break;

                case Keys.Insert:
                    PerformNew();
                    break;

                case Keys.Delete:
                    PerformDelete();
                    break;
            }
        }

        private void PerformNew()
        {
            using (var dialog = new FormCardView() { Table = this.Table, Base = this.Base })
            {
                dialog.InitializeModel();
                var res = dialog.ShowDialog();

                if (res != DialogResult.Abort)
                {
                    tableDataGridView1.SelectedID = dialog.Model.ID.Value;
                    FillTable();
                }
            }
        }

        private void PerformChange()
        {
            var selectedID = tableDataGridView1.SelectedID;
            if (selectedID == null)
                return;

            using (var dialog = new FormCardView() { Table = this.Table, Base = this.Base })
            {
                dialog.InitializeModel(selectedID);
                var res = dialog.ShowDialog();

                if (res != DialogResult.Abort)
                {
                    FillTable();
                }
            }
        }

        private void PerformDelete()
        {
            var selectedID = tableDataGridView1.SelectedID;
            if (selectedID == null)
                return;

            if (MessageBox.Show("Удалить запись?", Consts.ProgramTitle,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            if (ModelHelper.Delete(Base, Table, selectedID))
            {
                FillTable();
            }
        }
        
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            PerformNew();
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            PerformChange();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            PerformDelete();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if (MessageBox.Show("Выйти из программы?", Consts.ProgramTitle,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.Close();
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormFilter())
            {
                dialog.InitializeFilter(Table);

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}
