using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;

namespace Core.Data.Design.Controls.FieldControl
{
    public class BindControl : Button, IDesignControl
    {
        public BindControl()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new FieldProperty(this) { AccessTypes = new FieldType[] { FieldType.BIND } });
            Properties.Add(new TabIndexProperty(this));

            BackColor = Color.White;
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            DefaultColor = BackColor;
            TextAlign = ContentAlignment.MiddleLeft;
        }

        public DesignControlType ControlType => DesignControlType.FIELD;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }
    }
}
