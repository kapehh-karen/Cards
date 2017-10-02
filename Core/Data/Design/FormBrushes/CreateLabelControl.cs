using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.Standard;

namespace Core.Data.Design.FormBrushes
{
    public class CreateLabelControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new LabelControl();
    }
}
