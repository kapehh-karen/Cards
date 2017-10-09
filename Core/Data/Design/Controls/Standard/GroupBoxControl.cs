﻿using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls.Standard
{
    public class GroupBoxControl : GroupBox, IDesignControl
    {
        public GroupBoxControl()
        {
            Properties.Add(new TextProperties() { Control = this });
            Properties.Add(new SizeProperties() { Control = this });
            Properties.Add(new PositionProperties() { Control = this });
            
            DefaultColor = BackColor;
        }

        public DesignControlType ControlType => DesignControlType.CONTAINER;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public Color DefaultColor { get; set; }
    }
}
