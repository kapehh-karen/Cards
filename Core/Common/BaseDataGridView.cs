using Core.Storage.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Common
{
    public abstract class BaseDataGridView : DataGridView
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
        }

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
    }
}
