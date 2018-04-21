using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class NumberProcessor : IFieldProcessor
    {
        private Control control;

        public override void Attach()
        {
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
            get => int.TryParse(control.Text, out int x) ? (int?)x : null;
            set => control.Text = value?.ToString();
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
