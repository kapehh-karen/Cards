﻿using Core.Data.Design.Controls;
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
        private const double MOD_POS = 5;
        private Cursor prevCurs;
        private Point startLocation;
        private Size size;

        public override string Name => "Создание элемента";

        public override void ActivateBrush(CardTabPage sender)
        {
            prevCurs = sender.Cursor;
            sender.Cursor = Cursors.Cross;
        }

        public override void DeactivateBrush(CardTabPage sender)
        {
            sender.Cursor = prevCurs;
        }

        public override void MouseDown(CardTabPage sender, Control control, Point coord)
        {
            startLocation = coord;
            startLocation.X = (int)(Math.Round(startLocation.X / MOD_POS) * MOD_POS);
            startLocation.Y = (int)(Math.Round(startLocation.Y / MOD_POS) * MOD_POS);
            size = new Size(0, 0);
        }

        public override void MouseMove(CardTabPage sender, Control control, Point coord)
        {
            size.Height = Math.Abs(startLocation.Y - coord.Y);
            size.Width = Math.Abs(startLocation.X - coord.X);
        }

        public override void MouseUp(CardTabPage sender, Control control, Point coord)
        {
            var c = DesignControl() as Control;
            var dc = c as IDesignControl;

            dc.ParentControl = control is IDesignControl ? control as IDesignControl : sender;
            dc.InDesigner = true;
            c.Location = new Point(Math.Min(startLocation.X, coord.X), Math.Min(startLocation.Y, coord.Y));
            c.Size = new Size(Math.Max(size.Width, 100), Math.Max(size.Height, 20));

            IDesignControl container = null;
            if (control != null && control is IDesignControl pc && pc.ControlType == DesignControlType.CONTAINER)
            {
                control.Controls.Add(c);
                container = pc;
            }
            else if (control != null)
            {
                c.Location = sender.PointToClient(control.PointToScreen(c.Location));
                sender.Controls.Add(c);
            }
            else
            {
                sender.Controls.Add(c);
            }

            c.BringToFront();

            if (sender.FindForm() is FormEmpty form)
            {
                sender.AddDesignControl(dc, container);
                form.SelectedControl = dc;
            }
        }

        public abstract IDesignControl DesignControl();
    }
}
