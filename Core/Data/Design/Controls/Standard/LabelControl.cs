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
            Properties.Add(new TextProperties() { Control = this });
            Properties.Add(new SizeProperties() { Control = this });
            Properties.Add(new PositionProperties() { Control = this });
            
            AutoSize = true;
            DefaultColor = BackColor;
        }
        
        public DesignControlType ControlType => DesignControlType.STANDARD;

        public List<IControlProperties> Properties { get; set; } = new List<IControlProperties>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public Color DefaultColor { get; set; }
    }
}
