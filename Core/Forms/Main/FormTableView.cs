using Core.API;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.ExportData.Forms;
using Core.Filter.Data;
using Core.Filter.Forms;
using Core.Forms.Main.CardForm;
using Core.GroupEdit.Forms;
using Core.Helper;
using Core.Notification;
using Core.SimpleFilter.Forms;
using Core.Storage.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormTableView : Form
    {
        private TableData table;
        private Dictionary<FieldData, string> prevSimpleFilterData;

        public FormTableView()
        {
            InitializeComponent();
            tableDataGridView1.FillCompleted += UpdateAmount;
        }

        public void SendEventFormCreated()
        {
            // Вызываем единожды, при создании формы
            PluginListener.Instance.EventFormTableCreated(this);
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
        
        public FilterData LastUsedFilterData { get; set; }
        
        private void UpdateAmount(object sender, EventArgs e)
        {
            toolStripStatusLabelAmount.Text = $"Всего записей: {tableDataGridView1.CurrentDataView.Count}";
        }

        public void FillTable()
        {
            tableDataGridView1.FillTable();
        }

        private void tableDataGridView1_RedSelectingChanged(object sender, EventArgs e)
        {
            var amount = tableDataGridView1.CountSelectedItems;
            toolStripStatusLabelSelectedAmount.Text = $"Выбрано записей: {amount}";
            toolStripStatusLabelSelectedAmount.ForeColor = amount > 0 ? Color.Red : Color.Black;
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
            if (!Table.AllowNew)
            {
                NotificationMessage.Error("В таблице \"{0}\" запрещено создавать новые записи.", Table.DisplayName);
                return;
            }

            var dialog = Table.CardView;
            dialog.IsLinkedModel = false;
            dialog.InitializeModel();
            if (dialog.ShowDialog() != DialogResult.Abort)
            {
                tableDataGridView1.SelectedID = dialog.Model.ID.Value;
                FillTable();
            }
        }

        private void PerformChange()
        {
            if (!Table.AllowEdit)
            {
                NotificationMessage.Error("В таблице \"{0}\" запрещено изменять записи.", Table.DisplayName);
                return;
            }

            var selectedID = tableDataGridView1.SelectedID;
            if (selectedID == null)
                return;

            var tableRowIndex = TableRowIndex.Create(tableDataGridView1);

            var dialog = Table.CardView;
            dialog.IsLinkedModel = false;
            dialog.InitializeModel(selectedID, tableIndex: tableRowIndex);
            if (dialog.ShowDialog() != DialogResult.Abort)
            {
                var nowSelectedID = dialog.Model.ID.Value;
                if (!ModelFieldValue.EqualsObjectValues(nowSelectedID, selectedID))
                    tableDataGridView1.SelectedID = nowSelectedID;
                FillTable();
            }
        }

        private void PerformDelete()
        {
            if (!Table.AllowDelete)
            {
                NotificationMessage.Error("В таблице \"{0}\" запрещено удалять записи.", Table.DisplayName);
                return;
            }

            var selectedID = tableDataGridView1.SelectedID;
            if (selectedID == null)
                return;

            using (var frmAskDelete = new FormAskDelete())
            {
                if (frmAskDelete.ShowDialog() != DialogResult.OK)
                    return;
            }

            if (ModelHelper.Delete(Table, selectedID))
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
                dialog.Filter = LastUsedFilterData;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tableDataGridView1.Filter = LastUsedFilterData = dialog.Filter;
                    FillTable();
                    toolStripButtonFilter.Image = Properties.Resources.funnel_on;
                }
            }
        }
        
        private void toolStripButtonFilterReset_Click(object sender, EventArgs e)
        {
            prevSimpleFilterData = null;
            tableDataGridView1.ResetFilter();
            FillTable();
            toolStripButtonFilter.Image = Properties.Resources.funnel;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }

        private void documentsExploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocStorage.Instance.OpenDocumentsFolder();
        }

        private void toolStripButtonGroupEdit_Click(object sender, EventArgs e)
        {
            if (tableDataGridView1.CountSelectedItems > 0)
            {
                using (var dialog = new FormGroupEdit
                {
                    Table = this.Table,
                    SelectedIDs = tableDataGridView1.SelectedIDs
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

        private void saveToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileName = DocStorage.Instance.GenerateFileName("Экспорт данных", "xlsx");
            WaitDialog.Run("Экспортируются данные...", dialog => ExcelHelper.SaveDataGridViewToExcel(dialog, fileName, tableDataGridView1));
            DocStorage.Instance.OpenDocumentFile(fileName);
        }

        private void FormTableView_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохраняем настройки таблицы
            tableDataGridView1.TableStorageInformationSave();
        }

        private void extendedExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormSelectTableFields { Table = Table, FormTable = this })
            {
                dialog.ShowDialog();
            }
        }

        private void toolStripSimpleFilter_Click(object sender, EventArgs e)
        {
            var columns = tableDataGridView1.TableStorageInformation.Columns;
            
            using (var dialog = new SimpleFilterForm())
            {
                dialog.InitializeWithFields(columns.Select(x => x.Field)
                    .Where(x => x.Visible)
                    .ToArray());
                dialog.Data = prevSimpleFilterData;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tableDataGridView1.CurrentDataView.RowFilter = dialog.ResultFilter;
                    prevSimpleFilterData = dialog.Data;
                    UpdateAmount(tableDataGridView1, EventArgs.Empty);
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Tab)
            {
                DialogResult = DialogResult.Ignore;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #region API

        /// <summary>
        /// Добавляет новый пункт в меню формы
        /// </summary>
        /// <param name="menuItem"></param>
        public void AddMenuItem(ToolStripMenuItem menuItem) => mainMenuStrip.Items.Add(menuItem);

        /// <summary>
        /// Добавляет новую кнопку
        /// </summary>
        /// <param name="button"></param>
        public void AddButton(ToolStripItem button) => toolStripHeader.Items.Add(button);

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
        /// Возвращает общее количество записей в таблице
        /// </summary>
        /// <returns></returns>
        public int CountAllItems() => tableDataGridView1.CurrentDataView.Count;

        /// <summary>
        /// Возвращает ID всех записей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetAllIDs() => tableDataGridView1.AllIDs;

        /// <summary>
        /// Возвращает ID выделенных записей
        /// </summary>
        /// <returns></returns>
        public ICollection<object> GetSelectedIDs() => tableDataGridView1.SelectedIDs;

        #endregion
    }
}
