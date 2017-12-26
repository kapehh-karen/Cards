using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Common
{
    public class BaseDataGridView : DataGridView
    {
        private readonly KeyEventArgs enterKeyEventArgs = new KeyEventArgs(Keys.Enter);
        public event KeyEventHandler PressedEnter;
        public event KeyEventHandler PressedKey;

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
                PressedEnter?.Invoke(this, enterKeyEventArgs);
            }
        }
    }
}
