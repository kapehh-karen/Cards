using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public class DateProcessor : IFieldProcessor
    {
        private TextControl control;

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
                control = value as TextControl;
                Attach();
            }
        }

        public override object Value
        {
            get => DateTime.TryParse(control.Text, out var date) ? (object)date : null;
            set => control.Text = (value != null && value is DateTime) ? ((DateTime)value).ToString(@"dd/MM/yyyy") : string.Empty;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
