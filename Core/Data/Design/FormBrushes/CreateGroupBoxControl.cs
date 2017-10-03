using Core.Data.Design.Controls;
using Core.Data.Design.Controls.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.FormBrushes
{
    public class CreateGroupBoxControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new GroupBoxControl();
    }
}
