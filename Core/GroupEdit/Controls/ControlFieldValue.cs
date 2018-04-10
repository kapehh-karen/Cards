using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Field;
using Core.Data.Model.Preprocessors;
using Core.Data.Design.Controls.FieldControl;
using Core.Data.Model.Preprocessors.Impl;
using Core.Data.Model;
using Core.Data.Design.Controls;

namespace Core.GroupEdit.Controls
{
    public partial class ControlFieldValue : UserControl
    {
        public ControlFieldValue()
        {
            InitializeComponent();
        }

        private FieldData field = null;
        public FieldData Field
        {
            get => field;
            set
            {
                field = value;

                if (field != null)
                    UpdateComponent();
            }
        }

        public Control InputControl { get; set; }

        public IFieldProcessor Processor { get; set; }

        private void UpdateComponent()
        {
            if (InputControl != null)
            {
                InputControl.Dispose();
                InputControl = null;
            }

            if (Processor != null)
            {
                Processor.Detach();
                Processor = null;
            }

            switch (Field.Type)
            {
                case FieldType.TEXT:
                    InputControl = new TextControl();
                    Processor = new TextProcessor();

                    var ct = InputControl as TextControl;
                    ct.AutoSize = false;
                    break;

                case FieldType.NUMBER:
                    InputControl = new TextControl();
                    Processor = new NumberProcessor();

                    var cn = InputControl as TextControl;
                    cn.AutoSize = false;
                    break;

                case FieldType.DATE:
                    InputControl = new MaskedTextControl();
                    Processor = new DateProcessor();

                    var cd = InputControl as MaskedTextControl;
                    cd.AutoSize = false;
                    cd.TextAlign = HorizontalAlignment.Center;
                    break;

                case FieldType.BOOLEAN:
                    InputControl = new BooleanControl();
                    Processor = new BooleanProcessor();

                    var cb = InputControl as BooleanControl;
                    cb.AutoSize = false;
                    cb.Text = string.Empty;
                    cb.CheckAlign = ContentAlignment.MiddleCenter;
                    break;

                case FieldType.BIND:
                    InputControl = new BindControl();
                    Processor = new BindProcessor();
                    break;

                default:
                    return;
            }

            InputControl.Dock = DockStyle.Fill;
            Controls.Add(InputControl);
            Processor.Control = InputControl as IDesignControl;
            Processor.ModelField = new ModelFieldValue() { Field = Field };
        }

        public object Value => Processor?.Value;
    }
}
