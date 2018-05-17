using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Filter.Data;
using Core.Data.Field;
using Core.Notification;
using Core.Filter.Data.Operand;
using Core.Filter.Data.Operand.Impl;
using Core.Helper;
using Core.Common.Fields;
using Core.Data.Table;

namespace Core.Filter.Controls.Conditions.Operands
{
    public partial class InputField : UserControl, IInputOperand
    {
        private class MenuItemTag
        {
            public FilterTable FilterTable { get; set; }

            public FieldData FieldData { get; set; }

            public TableData Table { get; set; }
        }
        
        public event EventHandler OperandTypeChanged = (s, e) => { };

        public event EventHandler OperandFieldChanged = (s, e) => { };

        private MenuItemTag SelectedItem { get; set; }

        private FieldData field = null;
        public FieldData Field
        {
            get => field;
            set
            {
                field = value;

                // Оповещаем что поменялось поле
                OperandFieldChanged(this, null);
            }
        }

        public InputField()
        {
            InitializeComponent();
        }
        
        public FilterData FilterData { get; set; }

        private FieldType type = FieldType.UNKNOWN;
        public FieldType Type
        {
            get => type;
            set
            {
                type = value;

                // Оповещаем что тип мог измениться
                OperandTypeChanged(this, null);
            }
        }

        public OperandType OperandType => OperandType.FIELD;

        public IFilterOperand Operand
        {
            get => SelectedItem != null ? new FieldOperand()
            {
                FilterTable = SelectedItem.FilterTable,
                FilterField = new FilterField() { Field = SelectedItem.FieldData }
            } : null;
            set
            {
                var operand = value as FieldOperand;
                SetField(new MenuItemTag()
                {
                    FilterTable = operand.FilterTable,
                    FieldData = operand.FilterField.Field
                });
            }
        }

        private void btnSelectField_Click(object sender, EventArgs e)
        {
            if (FilterData == null)
            {
                NotificationMessage.SystemError("Данные для поля неопределены");
                return;
            }

            var contextMenu = new ContextMenuStrip();
            
            contextMenu.Items.Add(new ToolStripMenuItem("Текущая таблица", null, menuSelectField_Click)
            {
                ForeColor = Color.Green,
                Tag = new MenuItemTag()
                {
                    FilterTable = FilterData.FilterTable,
                    Table = FilterData.FilterTable.Table,
                    FieldData = null
                }
            });

            var cursor = FilterData;
            while ((cursor = cursor.Parent) != null)
            {
                contextMenu.Items.Add(new ToolStripMenuItem(cursor.FilterTable.ToString(), null, menuSelectField_Click)
                {
                    Tag = new MenuItemTag()
                    {
                        FilterTable = cursor.FilterTable,
                        Table = cursor.FilterTable.Table,
                        FieldData = null
                    }
                });
            }

            contextMenu.Show(btnSelectField, 0, btnSelectField.Height);
        }

        private bool removedStylesButton = false;

        private void menuSelectField_Click(object sender, EventArgs e)
        {
            var tag = (sender as ToolStripMenuItem).Tag as MenuItemTag;
            var dialog = new FormPopupSearchField() { Table = tag.Table };
            var scrP = this.PointToScreen(this.Location);
            var p = new Point(scrP.X, scrP.Y + this.Height);
            dialog.StartPosition = FormStartPosition.Manual;
            dialog.Location = p;
            dialog.Tag = tag;
            dialog.FieldSelected += Dialog_FieldSelected;
            dialog.Show();
        }

        private void Dialog_FieldSelected(object sender, EventArgs e)
        {
            var dialog = sender as FormPopupSearchField;
            var tag = dialog.Tag as MenuItemTag;
            tag.FieldData = dialog.SelectedField;
            SetField(tag);
            dialog.Dispose();
        }

        private void SetField(MenuItemTag itemTag)
        {
            if (!removedStylesButton)
            {
                btnSelectField.ForeColor = Color.Black;
                btnSelectField.Font = new Font(btnSelectField.Font, FontStyle.Regular);
                removedStylesButton = true;
            }
            btnSelectField.Text = itemTag.FieldData.DisplayName;
            SelectedItem = itemTag;
            Field = itemTag.FieldData;
            Type = itemTag.FieldData.Type;
        }
    }
}
