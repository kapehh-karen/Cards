using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using Core.Forms.Main.CardForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class BindProcessor : IFieldProcessor
    {
        private BindControl control;
        //private ToolTip toolTip = new ToolTip();

        public override void Attach()
        {
            base.Attach();

            if (control != null)
                control.Click += Control_Click;
        }

        public override void Detach()
        {
            base.Detach();

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
                //toolTip.RemoveAll();
                Attach();
            }
        }

        public override object Value
        {
            get => ModelField.Value;
            set
            {
                ModelField.Value = value;
                ModelField.UpdateBindData(); // Обновляем, поменяли ведь Value все-таки
                if (control != null)
                {
                    var text = ModelField.ToString();
                    control.Text = text;
                    //toolTip.SetToolTip(control, text);
                }
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormSelectInClassificator()
            {
                Field = ModelField.Field,
                Table = ModelField.Field.BindData?.Table
            })
            {
                dialog.SelectedID = Value;
                dialog.FillTable();

                var result = dialog.ShowDialog();
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.OK:
                        Value = dialog.SelectedID;
                        OnValueChanged(Value, this);
                        break;

                    case System.Windows.Forms.DialogResult.Ignore:
                        Value = null;
                        OnValueChanged(null, this);
                        break;
                }
            }
        }
    }
}
