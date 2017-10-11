﻿using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls.FieldControl
{
    public class BooleanControl : CheckBox, IDesignControl
    {
        public BooleanControl()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new FieldProperty(this) { AccessTypes = new FieldType[] { FieldType.BOOLEAN } });
            Properties.Add(new TabIndexProperty(this));

            AutoSize = true;
            DefaultColor = BackColor;
        }

        public DesignControlType ControlType => DesignControlType.FIELD;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }
    }
}
