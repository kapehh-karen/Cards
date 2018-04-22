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
            if (control != null)
            {
                control.TextChanged += Control_TextChanged;
                control.LostFocus += Control_LostFocus;
                UpdateMaxLength();
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

        public override FieldData Field
        {
            get => base.Field;
            set
            {
                base.Field = value;
                UpdateMaxLength();
            }
        }

        private void UpdateMaxLength()
        {
            if (control != null && Field != null)
            {
                // Максимальная длина
                if (control is TextControl textControl)
                    textControl.MaxLength = Field.Size;
            }
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

        private void Control_LostFocus(object sender, EventArgs e)
        {
            if (Field != null && control.Text.Length > Field.Size)
            {
                if (MessageBox.Show($"Количество символов в значении поля \"{Field.DisplayName}\" больше допустимого размера равного {Field.Size}.", Consts.ProgramTitle,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    control.Focus();
                else
                    control.Text = control.Text.Substring(0, Field.Size);
            }
        }
    }
}
