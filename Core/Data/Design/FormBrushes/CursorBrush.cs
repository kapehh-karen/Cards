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

        public override void ActivateBrush(FormEmpty form)
        {
            prevCurs = form.Cursor;
            form.Cursor = Cursors.Default;
        }

        public override void DeactivateBrush(FormEmpty form)
        {
            form.Cursor = prevCurs;
        }

        public override void MouseDown(FormEmpty form, Control control, Point coord)
        {
            if (control is IDesignControl cc)
                form.SelectedControl = cc;
            else
                form.SelectedControl = null;
        }
    }
}
