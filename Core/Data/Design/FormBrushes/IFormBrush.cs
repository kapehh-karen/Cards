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
        public virtual void ActivateBrush(TabPage sender) { }

        public virtual void DeactivateBrush(TabPage sender) { }

        public virtual void MouseDown(TabPage sender, Control control, Point coord) { }

        public virtual void MouseMove(TabPage sender, Control control, Point coord) { }

        public virtual void MouseUp(TabPage sender, Control control, Point coord) { }
    }
}
