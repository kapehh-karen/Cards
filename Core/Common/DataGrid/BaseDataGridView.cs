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
using Core.Data.Field;

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
            AllowUserToOrderColumns = true;
            ReadOnly = true;
            StandardTab = true;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AutoGenerateColumns = false; // По-дефолту вырубаю, а там где надо включаю

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
            // Перед открытием настроек, актуализируем настройки текущей таблицы
            TableStorageInformationSave(false);

            // Сначала в свойствах присваивается TableStorageType, иначе будет баг
            using (var formSettings = new TableColumnSettingForm() { TableStorageType = TableStorageType, Table = Table })
            {
                if (formSettings.ShowDialog() == DialogResult.OK)
                {
                    // Чтобы не сохранять настройки таблицы в DataSource Changed событии
                    initializedFirstData = false;

                    // Оповещаем дочерние классы об изменении конфигурации таблицы, следует обновить данные
                    OnTableStorageInformationUpdated();

                    // После применения настроек, сохраняем все в файл
                    TableStorage.Instance.Save(TableStorageInformation, TableStorageType);
                }
            }
        }

        protected virtual void OnTableStorageInformationUpdated() { }

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

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);

            if (CurrentRow != null)
            {
                PressedClick?.Invoke(this, enterKeyEventArgs);
            }
        }

        #endregion

        #region Table Storage Information

        /// <summary>
        /// Поле идентификатора
        /// </summary>
        public FieldData FieldID { get; private set; }

        private void BindingColumns()
        {
            var fields = TableStorageInformation.Columns.Select(item => item.Field);

            // Renaming columns header
            foreach (DataGridViewColumn column in this.Columns)
            {
                var fieldData = fields.SingleOrDefault(f => f.Name.Equals(column.Name));
                if (fieldData == null)
                {
                    column.Visible = false;
                    continue; // скипаем колонки для которых нету колонок в настройках
                }

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
                FieldID = value.IdentifierField;

                // Получаем настройки таблицы
                TableStorageInformation = TableStorage.Instance.Get(tableData, TableStorageType);

                if (!TableStorageInformation.HasColumns)
                {
                    InitializeFields();
                }
                else if (TableStorageInformation.Columns.Find(col => col.Field.IsIdentifier) == null)
                {
                    // Если куда-то спиздили поле с идентификатором, то его надо добавить, используется же в логике всегда
                    TableStorageInformation.AddColumn(FieldID);
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
        private void TableStorageInformationSave(bool saveToFile = true)
        {
            TableStorageInformation.SortData.Reset();

            TableStorageInformation.Columns.ForEach(col =>
            {
                var column = Columns[col.Field.Name];
                col.Width = column.Width;
                col.Order = column.DisplayIndex;

                // Сортировка допустима только в таблицах и классификаторах
                if (ViewType.Equals(DataGridType.TableAndClassificator) && SortedColumn == column)
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
            });

            if (saveToFile)
                TableStorage.Instance.Save(TableStorageInformation, TableStorageType);
        }

        /// <summary>
        /// Восстанавливаем инфу всех колонок
        /// </summary>
        private void TableStorageInformationApply()
        {
            // Сначала применяем визуальные параметры (сортируем по ордеру и применяем последовательно)
            TableStorageInformation.Columns.OrderBy(col => col.Order).ForEach(col =>
            {
                var column = Columns[col.Field.Name];
                column.Width = col.Width;
                column.DisplayIndex = col.Order;
            });

            // И только потом сортируем колонку
            // Сортировка допустима только в таблицах и классификаторах
            if (ViewType.Equals(DataGridType.TableAndClassificator) && TableStorageInformation.SortData.Exists)
            {
                var colData = TableStorageInformation.SortData.SortedColumn;
                var column = Columns[colData.Field.Name];
                switch (TableStorageInformation.SortData.Direction)
                {
                    case SortDirection.Ascending:
                        Sort(column, ListSortDirection.Ascending);
                        break;
                    case SortDirection.Descending:
                        Sort(column, ListSortDirection.Descending);
                        break;
                }
            }
        }

        private bool initializedFirstData = false;
        protected override void OnDataSourceChanged(EventArgs e)
        {
            if (DataSource == null)
                return;

            if (initializedFirstData)
            {
                // Перед изменением DataSource, актуализируем настройки текущей таблицы
                TableStorageInformationSave(false);
            }
            else initializedFirstData = true;

            // Пофиксил баг с AutoGenerateColumns, из-за него порядок столбцов был упоротым и поехавшим
            AutoGenerateColumns = true;
            base.OnDataSourceChanged(e);
            AutoGenerateColumns = false;

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
