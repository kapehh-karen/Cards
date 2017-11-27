using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
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
                dialog.ShowDialog();

                tableDataGridView1.SelectedID = dialog.Model.ID.Value;
                FillTable();
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
                dialog.ShowDialog();

                tableDataGridView1.SelectedID = selectedID;
                FillTable();
            }
        }

        private void PerformDelete()
        {
            var selectedID = tableDataGridView1.SelectedID;
            if (selectedID == null)
                return;

            if (MessageBox.Show("Удалить запись?", "Подтверждение действия",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            // Make SQL request
            using (var dbc = new SQLServerConnection(Base))
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    SqlModelHelper.DeleteFull(connection, transaction, Table, selectedID);
                    transaction.Commit();
                    NotificationMessage.Info("Удалено!");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error($"Ошибка при удалении:\r\n\r\n{ex.Message}");
                }

                transaction.Dispose();
            }

            FillTable();
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
    }
}
