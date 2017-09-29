using Core.Data.Design.Properties.ControlProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls.Standard
{
    public class LabelControl : IDesignControl
    {
        public LabelControl()
        {
            Properties.Add(new TextProperties() { Control = FormControl, Value = "Label" });
        }

        public override Control FormControl { get; } = new Label();
    }
}
