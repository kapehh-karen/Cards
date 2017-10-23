using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public class NumberProcessor : IFieldProcessor
    {
        private TextControl control;

        public override IDesignControl Control
        {
            get => control as IDesignControl;
            set
            {
                if (control != null)
                {
                    control.TextChanged -= Control_TextChanged;
                }

                control = value as TextControl;
                control.TextChanged += Control_TextChanged;
            }
        }

        public override object Value
        {
            get => int.TryParse(control.Text, out int x) ? x : 0;
            set => control.Text = value.ToString();
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
