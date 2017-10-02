using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.FormBrushes
{
    public abstract class IFormBrush
    {
        public virtual void ActivateBrush(Form form) { }

        public virtual void DeactivateBrush(Form form) { }

        public virtual void MouseDown(Form form, Control control, Point coord) { }

        public virtual void MouseMove(Form form, Control control, Point coord) { }

        public virtual void MouseUp(Form form, Control control, Point coord) { }
    }
}
