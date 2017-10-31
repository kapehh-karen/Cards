﻿using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using Core.Forms.Main.CardForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public class BindProcessor : IFieldProcessor
    {
        private BindControl control;

        public override void Attach()
        {
            base.Attach();
            
            if (control != null)
                control.Click += Control_Click;
        }

        public override void Detach()
        {
            if (control != null)
                control.Click -= Control_Click;
        }

        public override IDesignControl Control
        {
            get => control as IDesignControl;
            set
            {
                Detach();
                control = value as BindControl;
                Attach();
            }
        }

        public override object Value
        {
            get => ModelField.Value;
            set
            {
                ModelField.Value = value;
                if (control != null)
                    control.Text = ModelField.ToString();
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormSelectInClassificator()
            {
                Field = ModelField.Field,
                Table = ModelField.Field.BindData?.Table,
                Base = this.Base
            })
            {
                dialog.FillTable();
                dialog.SelectedID = Value;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ModelField.BindData = dialog.Model;
                    Value = dialog.SelectedID;
                }
            }
        }
    }
}
