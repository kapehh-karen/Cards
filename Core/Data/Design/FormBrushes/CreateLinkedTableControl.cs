using Core.Data.Design.Controls;
using Core.Data.Design.Controls.LinkedTableControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.FormBrushes
{
    public class CreateLinkedTableControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new LinkedTableControl();
    }
}
