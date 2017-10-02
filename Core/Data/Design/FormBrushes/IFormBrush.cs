using Core.Forms.Design;
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
        public virtual void ActivateBrush(FormEmpty form) { }

        public virtual void DeactivateBrush(FormEmpty form) { }

        public virtual void MouseDown(FormEmpty form, Control control, Point coord) { }

        public virtual void MouseMove(FormEmpty form, Control control, Point coord) { }

        public virtual void MouseUp(FormEmpty form, Control control, Point coord) { }
    }
}
