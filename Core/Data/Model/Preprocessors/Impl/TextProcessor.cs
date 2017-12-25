using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Design.Controls;
using Core.Data.Field;
using Core.Data.Design.Controls.FieldControl;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class TextProcessor : IFieldProcessor
    {
        private Control control;

        public override void Attach()
        {
            base.Attach();

            if (control != null)
                control.TextChanged += Control_TextChanged;
        }

        public override void Detach()
        {
            if (control != null)
                control.TextChanged -= Control_TextChanged;
        }

        public override IDesignControl Control
        {
            get => control as IDesignControl;
            set
            {
                Detach();
                control = value as Control;
                Attach();
            }
        }

        public override object Value
        {
            get => string.IsNullOrEmpty(control.Text) ? null : control.Text;
            set => control.Text = value as string;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
