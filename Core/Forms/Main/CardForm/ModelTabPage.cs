using Core.Data.Design.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;

namespace Core.Forms.Main.CardForm
{
    public class ModelTabPage : TabPage, IDesignControl
    {
        public ModelTabPage()
        {
            AutoScroll = true;
            AutoScrollMargin = new Size(5, 5);
        }

        public List<IControlProperty> Properties => new List<IControlProperty>();

        public DesignControlType ControlType => throw new NotImplementedException();

        public IDesignControl ParentControl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Color DefaultColor => throw new NotImplementedException();

        public List<IDesignControl> DesignControls { get; set; }
    }
}
