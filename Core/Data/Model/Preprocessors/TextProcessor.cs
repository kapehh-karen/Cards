﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Design.Controls;
using Core.Data.Field;
using Core.Data.Design.Controls.FieldControl;

namespace Core.Data.Model.Preprocessors
{
    public class TextProcessor : IFieldProcessor
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

        public override object Value { get => control.Text; set => control.Text = value as string; }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
