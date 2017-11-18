using Core.Data.Design.Properties;
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
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new FontProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));

            Text = "Группа";
            DefaultColor = BackColor;
            TabStop = false;
        }

        public DesignControlType ControlType => DesignControlType.CONTAINER;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
