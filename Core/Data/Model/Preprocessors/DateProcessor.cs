using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors
{
    public class DateProcessor : IFieldProcessor
    {
        private TextControl control;

        public override void Attach()
        {
            if (control != null)
            {
                control.TextChanged += Control_TextChanged;
                control.LostFocus += Control_LostFocus;
            }
        }

        public override void Detach()
        {
            if (control != null)
            {
                control.TextChanged -= Control_TextChanged;
                control.LostFocus -= Control_LostFocus;
            }
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
            get => control.MaskCompleted ? DateTime.TryParse(control.Text, out var date) ? (object)date : null : null;
            set => control.Text = (value != null && value is DateTime) ? ((DateTime)value).ToString(@"dd/MM/yyyy") : string.Empty;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Control_LostFocus(object sender, EventArgs e)
        {
            if (control.MaskCompleted && !DateTime.TryParse(control.Text, out var date))
            {
                if (MessageBox.Show($"Не удается преобразовать \"{control.Text}\" в дату.", "Ошибка ввода",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    control.Focus();
                else
                    control.Text = null;
            }
        }
    }
}
