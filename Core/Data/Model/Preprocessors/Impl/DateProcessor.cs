﻿using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class DateProcessor : IFieldProcessor
    {
        private MaskedTextControl control;

        public override void Attach()
        {
            base.Attach();

            if (control != null)
            {
                control.TextChanged += Control_TextChanged;
                control.LostFocus += Control_LostFocus;
            }
        }

        public override void Detach()
        {
            base.Detach();

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
                control = value as MaskedTextControl;
                Attach();
            }
        }

        public override object Value
        {
            get => control.MaskCompleted ? DateTime.TryParse(control.Text, out var date) ? (object)date : null : null;
            set
            {
                if (value != null && value is DateTime dtVal)
                    control.Text = dtVal.ToString(@"dd/MM/yyyy");
                else
                    control.Clear();
            }
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Control_LostFocus(object sender, EventArgs e)
        {
            if (control.MaskCompleted && !DateTime.TryParse(control.Text, out var date))
            {
                if (MessageBox.Show($"Не удается преобразовать \"{control.Text}\" в дату.", Consts.ProgramTitle,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    control.Focus();
                else
                    control.Text = null;
            }
        }
    }
}
