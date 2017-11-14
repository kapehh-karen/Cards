using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;

namespace Core.Data.Design.FormBrushes
{
    public class CreateMaskedTextControl : CreateControlBrush
    {
        public override IDesignControl DesignControl() => new MaskedTextControl();
    }
}
