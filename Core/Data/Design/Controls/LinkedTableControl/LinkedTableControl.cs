using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Table;
using Core.Common;
using Core.Storage.Tables;
using Core.Helper;
using Core.Forms.Main;

namespace Core.Data.Design.Controls.LinkedTableControl
{
    // TODO: Добавление, изменение и удаление записей можно сделать на клавиши и на контекстное меню
    public class LinkedTableControl : BaseDataGridView, IDesignControl
    {
        public LinkedTableControl() : base()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new FontProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new LinkedTableProperty(this));
            Properties.Add(new TabIndexProperty(this));

            BorderStyle = BorderStyle.FixedSingle;
            DefaultColor = BackgroundColor;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Override for red color in FormDesigner
        public override Color BackColor { get => base.BackgroundColor; set => base.BackgroundColor = value; }

        public DesignControlType ControlType => DesignControlType.LINKED_TABLE;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        #region Table Storage Information

        private readonly TableStorageType TableStorageType = TableStorageType.LinkedTable;

        private TableData _table = null;
        public TableData Table
        {
            get => _table;
            set
            {
                if (value == null || value == _table)
                    return;

                _table = value;

                // Получаем настройки таблицы
                TableStorageInformation = TableStorage.Instance.Get(_table, TableStorageType);

                if (!TableStorageInformation.HasColumns)
                {
                    _table.Fields.ForEach(TableStorageInformation.AddColumn);
                }

                // Если новая, сразу сохраним, в пизду нах
                if (TableStorageInformation.IsNew)
                {
                    TableStorage.Instance.SaveDefault(TableStorageInformation, TableStorageType);
                }
            }
        }

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
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public TableStorageInformation TableStorageInformation { get; set; }

        /// <summary>
        /// Сохраняем инфу всех колонок
        /// </summary>
        private void TableStorageInformationSave()
        {
            TableStorageInformation.Columns.ForEach(col =>
            {
                foreach (DataGridViewColumn column in Columns)
                    if (column.GetTag().Field?.Equals(col.Field) ?? false)
                    {
                        col.Width = column.Width;
                        col.Order = column.DisplayIndex;
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
        }
        
        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            // Привязываем к колонкам тег и переименовываем их
            BindingColumns();

            // Применить все настройки ширины столбцов и т.п.
            TableStorageInformationApply();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
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
