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
        private const double MOD_POS = 5;
        private Cursor prevCurs;
        private Point startPoint;

        public override string Name => "Указатель";

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
            startPoint = coord;

            if (sender.FindForm() is FormEmpty form)
            {
                if (control is IDesignControl cc)
                    form.SelectedControl = cc;
                else
                    form.SelectedControl = null;
            }
        }

        public override void MouseMove(CardTabPage sender, Control control, Point coord)
        {
            if (control != null)
            {
                var x = control.Left + (coord.X - startPoint.X);
                var y = control.Top + (coord.Y - startPoint.Y);

                control.Left = Math.Max((int)(Math.Round(x / MOD_POS) * MOD_POS), 0);
                control.Top = Math.Max((int)(Math.Round(y / MOD_POS) * MOD_POS), 0);
            }
        }

        public override void KeyPress(CardTabPage sender, Control control, Keys key)
        {
            if (control != null)
            {
                switch (key)
                {
                    case Keys.Up:
                        control.Top -= (int)MOD_POS;
                        break;

                    case Keys.Down:
                        control.Top += (int)MOD_POS;
                        break;

                    case Keys.Left:
                        control.Left -= (int)MOD_POS;
                        break;

                    case Keys.Right:
                        control.Left += (int)MOD_POS;
                        break;
                }
            }
        }
    }
}
