using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using System.Drawing;

namespace Core.Data.Design.Controls.FieldControl
{
    public class TextControl : MaskedTextBox, IDesignControl
    {
        public TextControl()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new MaskProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new FieldProperty(this));

            DefaultColor = BackColor;
            InsertKeyMode = InsertKeyMode.Overwrite;
        }
        
        public DesignControlType ControlType => DesignControlType.FIELD;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public Color DefaultColor { get; set; }
    }
}
