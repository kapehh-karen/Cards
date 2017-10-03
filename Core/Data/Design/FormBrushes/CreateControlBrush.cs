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
    public abstract class CreateControlBrush : IFormBrush
    {
        private Cursor prevCurs;
        private Point startLocation;
        private Size size;

        public override void ActivateBrush(FormEmpty form)
        {
            prevCurs = form.Cursor;
            form.Cursor = Cursors.Cross;
        }

        public override void DeactivateBrush(FormEmpty form)
        {
            form.Cursor = prevCurs;
        }

        public override void MouseDown(FormEmpty form, Control control, Point coord)
        {
            startLocation = coord;
            size = new Size(0, 0);
        }

        public override void MouseMove(FormEmpty form, Control control, Point coord)
        {
            size.Height = Math.Abs(startLocation.Y - coord.Y);
            size.Width = Math.Abs(startLocation.X - coord.X);
        }

        public override void MouseUp(FormEmpty form, Control control, Point coord)
        {
            var c = DesignControl() as Control;
            var dc = c as IDesignControl;
            c.Location = new Point(Math.Min(startLocation.X, coord.X), Math.Min(startLocation.Y, coord.Y));
            c.Size = new Size(Math.Max(size.Width, 26), Math.Max(size.Height, 13));

            if (control != null && control is IDesignControl pc && pc.ControlType == DesignControlType.CONTAINER)
                control.Controls.Add(c);
            else
                form.Controls.Add(c);

            c.BringToFront();
            form.AddDesignControl(dc);
            form.SelectedControl = dc;
        }

        public abstract IDesignControl DesignControl();
    }
}
