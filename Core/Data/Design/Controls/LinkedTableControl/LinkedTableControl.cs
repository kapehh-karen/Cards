using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;

namespace Core.Data.Design.Controls.LinkedTableControl
{
    public class LinkedTableControl : ListView, IDesignControl
    {
        public LinkedTableControl()
        {
            Properties.Add(new NameProperties(this));
            Properties.Add(new TextProperties(this));
            Properties.Add(new SizeProperties(this));
            Properties.Add(new PositionProperties(this));
            Properties.Add(new LinkedTableProperties(this));

            DefaultColor = BackColor;
        }

        public DesignControlType ControlType => DesignControlType.LINKED_TABLE;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public Color DefaultColor { get; set; }
    }
}
