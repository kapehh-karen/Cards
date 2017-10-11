using Core.Data.Design.Properties.ControlProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using System.Drawing;

namespace Core.Data.Design.Controls.Standard
{
    public class LabelControl : Label, IDesignControl
    {
        public LabelControl()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));

            Text = "Текст";
            AutoSize = true;
            DefaultColor = BackColor;
        }
        
        public DesignControlType ControlType => DesignControlType.STANDARD;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }
    }
}
