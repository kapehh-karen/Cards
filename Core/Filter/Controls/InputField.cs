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

namespace Core.Filter.Controls
{
    public partial class InputField : UserControl
    {
        private class MenuItemTag
        {
            public FilterTable FilterTable { get; set; }

            public FieldData FieldData { get; set; }

            public override string ToString()
            {
                return $"{FilterTable.AliasName} -> {FieldData.DisplayName}";
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
                var needUpdate = value != type;
                type = value;
            }
        }

        private void btnSelectField_Click(object sender, EventArgs e)
        {
            var contextMenu = new ContextMenuStrip();

            var menuItem = new ToolStripMenuItem("Текущая таблица");
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

            contextMenu.Show(btnSelectField, 0, 0);
        }

        private void fieldMenu_Click(object sender, EventArgs e)
        {
            var tag = (sender as ToolStripMenuItem).Tag as MenuItemTag;

            btnSelectField.ForeColor = Color.Black;
            btnSelectField.Text = tag.ToString();
        }
    }
}
