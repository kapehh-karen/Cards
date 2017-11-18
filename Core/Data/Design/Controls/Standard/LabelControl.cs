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
            Properties.Add(new TextProperty(this, true));
            Properties.Add(new FontProperty(this));
            Properties.Add(new ColorProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new PositionProperty(this));

            Text = "Текст";
            AutoSize = true;
            DefaultColor = BackColor;
            TabStop = false;
        }
        
        public DesignControlType ControlType => DesignControlType.STANDARD;

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
