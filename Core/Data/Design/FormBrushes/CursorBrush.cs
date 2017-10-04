using Core.Data.Design.Controls;
using Core.Forms.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.FormBrushes
{
    public class CursorBrush : IFormBrush
    {
        private Cursor prevCurs;

        public override void ActivateBrush(CardTabPage sender)
        {
            prevCurs = sender.Cursor;
            sender.Cursor = Cursors.Default;
        }

        public override void DeactivateBrush(CardTabPage sender)
        {
            sender.Cursor = prevCurs;
        }

        public override void MouseDown(CardTabPage sender, Control control, Point coord)
        {
            if (sender.FindForm() is FormEmpty form)
            {
                if (control is IDesignControl cc)
                    form.SelectedControl = cc;
                else
                    form.SelectedControl = null;
            }
        }
    }
}
