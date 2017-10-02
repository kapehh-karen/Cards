using Core.Data.Design.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.FormBrushes
{
    public abstract class CreateControlBrush : IFormBrush
    {
        private Cursor prevCurs;
        private Point startLocation;
        private Size size;

        public override void ActivateBrush(Form form)
        {
            prevCurs = form.Cursor;
            form.Cursor = Cursors.Cross;
        }

        public override void DeactivateBrush(Form form)
        {
            form.Cursor = prevCurs;
        }

        public override void MouseDown(Form form, Control control, Point coord)
        {
            startLocation = coord;
            size = new Size(0, 0);
        }

        public override void MouseMove(Form form, Control control, Point coord)
        {
            size.Height = Math.Abs(startLocation.Y - coord.Y);
            size.Width = Math.Abs(startLocation.X - coord.X);
        }

        public override void MouseUp(Form form, Control control, Point coord)
        {
            form.Controls.Add(DesignControl as Control);
        }

        public abstract IDesignControl DesignControl { get; }
    }
}
