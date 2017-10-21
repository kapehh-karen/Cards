using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using System.Drawing;
using Core.Data.Field;

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
            Properties.Add(new FieldProperty(this) { AccessTypes = new FieldType[] { FieldType.TEXT, FieldType.NUMBER, FieldType.DATE } });
            Properties.Add(new TabIndexProperty(this));

            DefaultColor = BackColor;
            InsertKeyMode = InsertKeyMode.Overwrite;
        }
        
        public DesignControlType ControlType => DesignControlType.FIELD;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }
    }
}
