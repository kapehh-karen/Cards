using Core.Data.Design.Controls;
using Core.Data.Design.Controls.FieldControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors.Impl
{
    public class BooleanProcessor : IFieldProcessor
    {
        private BooleanControl control;

        public override void Load()
        {
            // Если значение NULL то устанавливаем значение по-умолчанию
            // NOTE: У логического значения ДОЛЖНО БЫТЬ только 2 состояния: True и False
            if (ModelField.Value == null)
                ModelField.Value = false;

            base.Load();
        }

        public override void Attach()
        {
            if (control != null)
                control.CheckedChanged += Control_CheckedChanged;
        }

        public override void Detach()
        {
            if (control != null)
                control.CheckedChanged -= Control_CheckedChanged;
        }

        public override IDesignControl Control
        {
            get => control as IDesignControl;
            set
            {
                Detach();
                control = value as BooleanControl;
                Attach();
            }
        }

        public override object Value { get => control.Checked; set => control.Checked = (bool)(value ?? false); }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
