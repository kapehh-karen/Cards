using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Field;
using Core.Filter.Data.Operand;
using Core.Filter.Data;
using Core.Notification;
using Core.Filter.Data.Operand.Impl;

namespace Core.Filter.Controls.Conditions.Operands
{
    public partial class InputSubquery : UserControl, IInputOperand
    {
        private class MenuItemTag
        {
            public FilterData Current { get; set; }
        }

        public InputSubquery()
        {
            InitializeComponent();
        }

        public event EventHandler OperandTypeChanged = (s, e) => { };
        public event EventHandler OperandFieldChanged = (s, e) => { };

        /// <summary>
        /// Всегда возвращает NUMBER
        /// </summary>
        public FieldType Type
        {
            get => FieldType.NUMBER;
            set => OperandTypeChanged(this, null); // Оповещаем всех
        }

        public FieldData Field { get; set; }

        public OperandType OperandType => OperandType.SUBQUERY;

        private MenuItemTag SelectedItem { get; set; }

        public IFilterOperand Operand
        {
            get
            {
                var res = new SubqueryOperand()
                {
                    CurrentFilter = SelectedItem?.Current
                };
                return res;
            }
            set
            {
                var subquery = value as SubqueryOperand;
                SetSubquery(new MenuItemTag()
                {
                    Current = subquery.CurrentFilter
                });
            }
        }

        public FilterData FilterData { get; set; }

        private void btnSelectSubquery_Click(object sender, EventArgs e)
        {
            if (FilterData == null)
            {
                NotificationMessage.SystemError("Данные для выборки неопределены");
                return;
            }

            if (FilterData.Chields.Count == 0)
                return;

            var contextMenu = new ContextMenuStrip();

            FilterData.Chields.ForEach(filterData =>
            {
                var menuItem = new ToolStripMenuItem(filterData.ToString()) { ForeColor = Color.Blue };
                menuItem.Tag = new MenuItemTag() { Current = filterData };
                menuItem.Click += selectSubquery;
                contextMenu.Items.Add(menuItem);
            });
            
            contextMenu.Show(btnSelectSubquery, 0, btnSelectSubquery.Height);
        }

        private void selectSubquery(object sender, EventArgs e)
        {
            var tag = (sender as ToolStripMenuItem).Tag as MenuItemTag;
            SetSubquery(tag);
        }

        private bool removedStylesButton = false;
        private void SetSubquery(MenuItemTag itemTag)
        {
            if (itemTag.Current == null)
            {
                return;
            }

            if (!removedStylesButton)
            {
                btnSelectSubquery.ForeColor = Color.Black;
                btnSelectSubquery.Font = new Font(btnSelectSubquery.Font, FontStyle.Regular);
                removedStylesButton = true;
            }

            btnSelectSubquery.Text = itemTag.Current.FilterTable.Table.DisplayName;
            Type = FieldType.NUMBER; // Бесполезное присвоение, нужно только для вызова события
            SelectedItem = itemTag;
        }
    }
}
