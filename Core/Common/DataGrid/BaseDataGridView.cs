using Core.Data.Design.Controls;
using Core.Data.Table;
using Core.Storage.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Helper;
using Core.Storage.Tables.TableStorageData;
using System.ComponentModel;
using Core.Forms.Main;
using Core.Forms.Main.TableSetting;

namespace Core.Common.DataGrid
{
    public abstract class BaseDataGridView : DataGridView, IDesignControl
    {
        public BaseDataGridView()
        {
            BackgroundColor = Color.White;
            MultiSelect = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            ReadOnly = true;
            StandardTab = true;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Set the selection background color for all the cells.
            DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            DefaultCellStyle.SelectionForeColor = Color.White;

            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            RowsDefaultCellStyle.BackColor = Color.White;
            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Контекстное меню
            InitializeMenu();
        }

        public abstract DataGridType ViewType { get; }

        #region Context Menu

        private ContextMenuStrip Menu { get; set; }

        private void InitializeMenu()
        {
            Menu = new ContextMenuStrip();

            var itemMenu = new ToolStripMenuItem() { Text = "Настройки столбцов" };
            itemMenu.Click += ItemMenu_Click;

            Menu.Items.Add(itemMenu);

            this.ContextMenuStrip = Menu;
        }

        private void ItemMenu_Click(object sender, EventArgs e)
        {
            using (var formSettings = new TableColumnSettingForm() { Table = Table, TableStorageType = TableStorageType })
            {
                if (formSettings.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        #endregion

        #region IDesignControl

        public virtual List<IControlProperty> Properties { get; }
        public virtual DesignControlType ControlType { get; }
        public virtual IDesignControl ParentControl { get; set; }
        public virtual bool InDesigner { get; set; } = false;
        public virtual List<IDesignControl> DesignControls { get; set; }

        #endregion

        #region Keys bindings

        // Заглушка для кнопки мыши, action как и у кнопки Enter
        private readonly KeyEventArgs enterKeyEventArgs = new KeyEventArgs(Keys.Enter);

        public event KeyEventHandler PressedEnter;

        public event KeyEventHandler PressedKey;

        // KeyEventHandler для использования уже имеющегося обработчика PressedKey или PressedEnter
        public event KeyEventHandler PressedClick;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
            {
                if (CurrentRow != null)
                {
                    PressedEnter?.Invoke(this, e);
                }
            }
            else
                base.OnKeyDown(e);

            PressedKey?.Invoke(this, e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (CurrentRow != null)
            {
                PressedClick?.Invoke(this, enterKeyEventArgs);
            }
        }

        #endregion

        #region Table Storage Information
        
        private void BindingColumns()
        {
            var fields = TableStorageInformation.Columns.Select(item => item.Field);

            // Renaming columns header
            foreach (DataGridViewColumn column in this.Columns)
            {
                var fieldData = fields.Single(f => f.Name.Equals(column.Name));
                var tag = new TableColumnTag() { Field = fieldData };
                column.HeaderText = fieldData.DisplayName;
                column.Tag = tag;
                column.Visible = fieldData.Visible;
                column.SortMode = ViewType.Equals(DataGridType.TableAndClassificator) ?
                    DataGridViewColumnSortMode.Automatic : DataGridViewColumnSortMode.NotSortable;
            }
        }

        public TableStorageInformation TableStorageInformation { get; set; }

        public virtual TableStorageType TableStorageType { get; set; }

        protected abstract bool InitializeFields();

        private TableData tableData = null;
        public virtual TableData Table
        {
            get => tableData;
            set
            {
                if (value == null || value == tableData)
                    return;

                tableData = value;

                // Получаем настройки таблицы
                TableStorageInformation = TableStorage.Instance.Get(tableData, TableStorageType);

                if (!TableStorageInformation.HasColumns)
                {
                    InitializeFields();
                }

                // Если новая, сразу сохраним, в пизду нах
                if (TableStorageInformation.IsNew)
                {
                    TableStorage.Instance.SaveDefault(TableStorageInformation, TableStorageType);
                }
            }
        }
        
        /// <summary>
        /// Сохраняем инфу всех колонок
        /// </summary>
        private void TableStorageInformationSave()
        {
            TableStorageInformation.SortData.Reset();

            TableStorageInformation.Columns.ForEach(col =>
            {
                foreach (DataGridViewColumn column in Columns)
                    if (column.GetTag().Field?.Equals(col.Field) ?? false)
                    {
                        col.Width = column.Width;
                        col.Order = column.DisplayIndex;

                        if (ViewType.Equals(DataGridType.TableAndClassificator) &&
                            SortedColumn == column)
                        {
                            TableStorageInformation.SortData.SortedColumn = col;
                            switch (SortOrder)
                            {
                                case SortOrder.Ascending:
                                    TableStorageInformation.SortData.Direction = SortDirection.Ascending;
                                    break;
                                case SortOrder.Descending:
                                    TableStorageInformation.SortData.Direction = SortDirection.Descending;
                                    break;
                                default:
                                    TableStorageInformation.SortData.Direction = SortDirection.None;
                                    break;
                            }
                        }
                        break;
                    }
            });

            TableStorage.Instance.Save(TableStorageInformation, TableStorageType);
        }

        /// <summary>
        /// Восстанавливаем инфу всех колонок
        /// </summary>
        private void TableStorageInformationApply()
        {
            // Сначала применяем визуальные параметры
            TableStorageInformation.Columns.ForEach(col =>
            {
                foreach (DataGridViewColumn column in Columns)
                    if (column.GetTag().Field?.Equals(col.Field) ?? false)
                    {
                        column.Width = col.Width;
                        column.DisplayIndex = col.Order;
                        break;
                    }
            });

            // И только потом сортируем колонку
            if (ViewType.Equals(DataGridType.TableAndClassificator) && TableStorageInformation.SortData.Exists)
            {
                var colData = TableStorageInformation.SortData.SortedColumn;

                foreach (DataGridViewColumn column in Columns)
                {
                    if (column.GetTag().Field?.Equals(colData.Field) ?? false)
                    {
                        switch (TableStorageInformation.SortData.Direction)
                        {
                            case SortDirection.Ascending:
                                Sort(column, ListSortDirection.Ascending);
                                break;
                            case SortDirection.Descending:
                                Sort(column, ListSortDirection.Descending);
                                break;
                        }
                        break;
                    }
                }
            }
        }
        
        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            if (InDesigner)
                return;

            // Привязываем к колонкам тег и переименовываем их
            BindingColumns();

            // Применить все настройки ширины столбцов и т.п.
            TableStorageInformationApply();
        }

        protected override void Dispose(bool disposing)
        {
            if (!InDesigner && disposing)
            {
                // Сохраняем перед вызовом базового метода Dispose
                // (base.Dispose удалит колонки и хрен мы что сохраним)
                TableStorageInformationSave();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
