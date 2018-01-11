using Core.Data.Design.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using System.Drawing;

namespace Core.Forms.Design
{
    public class CardTabPage : TabPage, IDesignControl
    {
        public CardTabPage()
        {
            AutoScroll = true;
            AutoScrollMargin = new Size(5, 5);
            DoubleBuffered = true;
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
                c.KeyDown += Form.FormEmpty_KeyDown;
            }

            if (container == null)
                DesignControls.Add(control);
            else
                container.DesignControls.Add(control);
        }

        /// <summary>
        /// For save strings for this tab
        /// </summary>
        public string TempString { get; set; }

        public bool InDesigner { get; set; }

        public List<IControlProperty> Properties => throw new NotImplementedException();

        public DesignControlType ControlType => throw new NotImplementedException();

        public IDesignControl ParentControl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Color DefaultColor => throw new NotImplementedException();

        public override string ToString()
        {
            return Text;
        }
    }
}
