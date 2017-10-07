using Core.Data.Design.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Design
{
    public class CardTabPage : TabPage, IDesignContainer
    {
        public CardTabPage()
        {
            AutoScroll = true;
        }

        public FormEmpty Form { get; set; }

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public void AddDesignControl(IDesignControl control, IDesignControl container = null)
        {
            if (control is Control c)
            {
                c.MouseDown += Form.FormEmpty_MouseDown;
                c.MouseMove += Form.FormEmpty_MouseMove;
                c.MouseUp += Form.FormEmpty_MouseUp;
            }

            if (container == null)
                DesignControls.Add(control);
            else
                container.DesignControls.Add(control);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
