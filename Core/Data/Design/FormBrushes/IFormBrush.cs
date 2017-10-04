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
        public virtual void ActivateBrush(CardTabPage sender) { }

        public virtual void DeactivateBrush(CardTabPage sender) { }

        public virtual void MouseDown(CardTabPage sender, Control control, Point coord) { }

        public virtual void MouseMove(CardTabPage sender, Control control, Point coord) { }

        public virtual void MouseUp(CardTabPage sender, Control control, Point coord) { }
    }
}
