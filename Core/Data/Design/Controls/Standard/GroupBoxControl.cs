using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls.Standard
{
    public class GroupBoxControl : GroupBox, IDesignControl
    {
        private List<IControlProperties> props = new List<IControlProperties>();

        public GroupBoxControl()
        {
            Properties.Add(new TextProperties() { Control = this });
            Properties.Add(new SizeProperties() { Control = this });
            Properties.Add(new PositionProperties() { Control = this });

            Text = "GroupBox";
        }

        public DesignControlType ControlType => DesignControlType.CONTAINER;

        public List<IControlProperties> Properties => props;

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();
    }
}
