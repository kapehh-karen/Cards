using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.FormBrushes
{
    public interface IFormBrush
    {
        Form Form { get; set; }
        Cursor Cursor { get; set; }

        void MouseDown(Form form, Control control, int x, int y);
        void MouseMove(Form form, Control control, int x, int y);
        void MouseUp(Form form, Control control, int x, int y);
    }
}
