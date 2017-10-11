using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.FormBrushes
{
    public class CreateBooleanControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new BooleanControl();
    }
}
