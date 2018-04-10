using Core.API;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Table;
using Core.Filter.Data;
using Core.Filter.Forms;
using Core.Forms.Main.CardForm;
using Core.GroupEdit.Forms;
using Core.Helper;
using Core.Notification;
using Core.Storage.Documents;
using System;
using System.Collections;
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
                LastUsedFilterData = tableDataGridView1.Filter;
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

        public FilterData LastUsedFilterData { get; set; }

        public void FillTable()
        {
            tableDataGridView1.FillTable();
            toolStripStatusLabelAmount.Text = $"Всего записей: {tableDataGridView1.CurrentDataView.Count}";
        }

        private void tableDataGridView1_RedSelectingChanged(object sender, EventArgs e)
        {
            var amount = tableDataGridView1.CountSelectedItems;
            toolStripStatusLabelSelectedAmount.Text = $"Выбрано записей: {amount}";
            toolStripStatusLabelSelectedAmount.ForeColor = amount > 0 ? Color.Red : Color.Black;
        }

        private void FormTableView_Load(object sender, EventArgs e)
        {
            PluginListener.Instance.EventFormTableCreated(this);
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

        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormFilter())
            {
                dialog.CurrentTable = Table;
                dialog.FilterData = LastUsedFilterData;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tableDataGridView1.Filter = LastUsedFilterData = dialog.FilterData;
                    FillTable();
                    toolStripButtonFilter.Image = Properties.Resources.funnel_on;
                }
            }
        }

        private void toolStripButtonFilterReset_Click(object sender, EventArgs e)
        {
            tableDataGridView1.ResetFilter();
            FillTable();
            toolStripButtonFilter.Image = Properties.Resources.funnel;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void documentsExploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocStorage.Instance.OpenDocumentsFolder();
        }

        private void toolStripButtonGroupEdit_Click(object sender, EventArgs e)
        {
            if (tableDataGridView1.CountSelectedItems > 0)
            {
                using (var dialog = new FormGroupEdit()
                {
                    Table = this.Table,
                    SelectedIDs = tableDataGridView1.SelectedItems
                })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        FillTable();
                    }
                }
            }
            else
            {
                NotificationMessage.Warning("Для групповой корректировки требуется выбрать хотя бы одну запись.");
            }
        }

        #region API

        /// <summary>
        /// Добавляет новый пункт в меню формы
        /// </summary>
        /// <param name="menuItem"></param>
        public void AddMenuItem(ToolStripMenuItem menuItem) => mainMenuStrip.Items.Add(menuItem);

        /// <summary>
        /// Возвращает ID выбранной записи
        /// </summary>
        /// <returns></returns>
        public object SelectedID() => tableDataGridView1.SelectedID;

        /// <summary>
        /// Возвращает количество выбранных записей
        /// </summary>
        /// <returns></returns>
        public int CountSelectedItems() => tableDataGridView1.CountSelectedItems;

        /// <summary>
        /// Возвращает ID выделенных записей
        /// </summary>
        /// <returns></returns>
        public HashSet<object> GetSelectedItems() => tableDataGridView1.SelectedItems;

        #endregion
    }
}
