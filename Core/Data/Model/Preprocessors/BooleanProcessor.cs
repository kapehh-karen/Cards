using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public class BooleanProcessor : IFieldProcessor
    {
        private BooleanControl control;

        public override IDesignControl Control
        {
            get => control as IDesignControl;
            set
            {
                if (control != null)
                {
                    control.CheckedChanged -= Control_CheckedChanged;
                }

                control = value as BooleanControl;
                control.CheckedChanged += Control_CheckedChanged;
            }
        }

        public override object Value { get => control.Checked; set => control.Checked = (bool)value; }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
