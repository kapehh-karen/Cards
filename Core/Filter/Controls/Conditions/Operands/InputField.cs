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

namespace Core.Filter.Controls
{
    public partial class InputField : UserControl, IInputOperand
    {
        private class MenuItemTag
        {
            public FilterTable FilterTable { get; set; }

            public FieldData FieldData { get; set; }

            public override string ToString()
            {
                return $"[{FilterTable.AliasName}].{FieldData.DisplayName}";
            }
        }
        
        public event EventHandler OperandTypeChanged = (s, e) => { };

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
                var needUpdate = value != type;
                type = value;

                // Оповещаем что тип мог измениться
                OperandTypeChanged(this, null);
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

            var menuItem = new ToolStripMenuItem("Текущая таблица") { ForeColor = Color.Green };
            FilterData.FilterTable.Table.Fields.ForEach(field =>
            {
                var item = menuItem.DropDownItems.Add(field.DisplayName, null, fieldMenu_Click);
                item.Tag = new MenuItemTag() { FilterTable = FilterData.FilterTable, FieldData = field };
            });
            contextMenu.Items.Add(menuItem);

            FilterData cursor = FilterData;
            while ((cursor = cursor.Parent) != null)
            {
                menuItem = new ToolStripMenuItem(cursor.FilterTable.ToString());
                cursor.FilterTable.Table.Fields.ForEach(field =>
                {
                    var item = menuItem.DropDownItems.Add(field.DisplayName, null, fieldMenu_Click);
                    item.Tag = new MenuItemTag() { FilterTable = cursor.FilterTable, FieldData = field };
                });
                contextMenu.Items.Add(menuItem);
            }

            contextMenu.Show(btnSelectField, 0, btnSelectField.Height);
        }

        private bool removedStylesButton = false;

        private void fieldMenu_Click(object sender, EventArgs e)
        {
            var tag = (sender as ToolStripMenuItem).Tag as MenuItemTag;

            if (!removedStylesButton)
            {
                btnSelectField.ForeColor = Color.Black;
                btnSelectField.Font = new Font(btnSelectField.Font, FontStyle.Regular);
                removedStylesButton = true;
            }
            btnSelectField.Text = tag.FieldData.DisplayName;
            Type = tag.FieldData.Type;
        }
    }
}
