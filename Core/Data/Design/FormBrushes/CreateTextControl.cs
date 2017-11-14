using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;

namespace Core.Data.Design.FormBrushes
{
    public class CreateTextControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new TextControl();
    }
}
