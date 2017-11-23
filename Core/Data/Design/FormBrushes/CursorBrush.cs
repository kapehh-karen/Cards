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

        public override void KeyPress(CardTabPage sender, Control control, KeyEventArgs e)
        {
            if (control != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if ((e.Modifiers & Keys.Shift) != 0) // Расширение
                        {
                            control.Height += 1;
                            control.Top -= 1;
                        }
                        else if ((e.Modifiers & Keys.Control) != 0) // Сужение
                        {
                            control.Height -= 1;
                        }
                        else // Перемещение
                        {
                            control.Top -= 1;
                        }
                        break;

                    case Keys.Down:
                        if ((e.Modifiers & Keys.Shift) != 0) // Расширение
                        {
                            control.Height += 1;
                        }
                        else if ((e.Modifiers & Keys.Control) != 0) // Сужение
                        {
                            control.Top += 1;
                            control.Height -= 1;
                        }
                        else // Перемещение
                        {
                            control.Top += 1;
                        }
                        break;

                    case Keys.Left:
                        if ((e.Modifiers & Keys.Shift) != 0) // Расширение
                        {
                            control.Left -= 1;
                            control.Width += 1;
                        }
                        else if ((e.Modifiers & Keys.Control) != 0) // Сужение
                        {
                            control.Width -= 1;
                        }
                        else // Перемещение
                        {
                            control.Left -= 1;
                        }
                        break;

                    case Keys.Right:
                        if ((e.Modifiers & Keys.Shift) != 0) // Расширение
                        {
                            control.Width += 1;
                        }
                        else if ((e.Modifiers & Keys.Control) != 0) // Сужение
                        {
                            control.Left += 1;
                            control.Width -= 1;
                        }
                        else // Перемещение
                        {
                            control.Left += 1;
                        }
                        break;
                }
            }
        }
    }
}
