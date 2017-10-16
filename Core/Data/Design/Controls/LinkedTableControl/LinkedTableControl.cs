﻿using System;
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
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new LinkedTableProperty(this));
            Properties.Add(new TabIndexProperty(this));

            DefaultColor = BackColor;
            View = View.Details;
            MultiSelect = false;
        }

        public DesignControlType ControlType => DesignControlType.LINKED_TABLE;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }
    }
}