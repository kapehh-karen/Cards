using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Field;
using Core.Data.Design.Controls.FieldControl;
using Core.Data.Model.Preprocessors;
using Core.Data.Model.Preprocessors.Impl;
using Core.Data.Design.Controls;
using Core.Filter.Data.Operand;
using Core.Filter.Data.Operand.Impl;

namespace Core.Filter.Controls
{
    public partial class InputValue : UserControl, IInputOperand
    {
        public InputValue()
        {
            InitializeComponent();
        }

        public event EventHandler OperandTypeChanged = (s, e) => { };

        private FieldType type = FieldType.UNKNOWN;
        public FieldType Type
        {
            get => type;
            set
            {
                var needUpdate = value != type;
                type = value;
                if (needUpdate)
                    UpdateComponent();

                // Оповещаем что тип мог измениться
                OperandTypeChanged(this, null);
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

            switch (Type)
            {
                case FieldType.TEXT:
                    InputControl = new TextControl();
                    Processor = new TextProcessor();

                    var ct = InputControl as TextControl;
                    ct.AutoSize = false;
                    ct.BorderStyle = BorderStyle.None;
                    break;

                case FieldType.NUMBER:
                    InputControl = new TextControl();
                    Processor = new NumberProcessor();

                    var cn = InputControl as TextControl;
                    cn.AutoSize = false;
                    cn.BorderStyle = BorderStyle.None;
                    break;

                case FieldType.DATE:
                    InputControl = new MaskedTextControl();
                    Processor = new DateProcessor();
                    
                    var cd = InputControl as MaskedTextControl;
                    cd.AutoSize = false;
                    cd.TextAlign = HorizontalAlignment.Center;
                    cd.BorderStyle = BorderStyle.None;
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
        }

        public object Value
        {
            get => Processor?.Value;
            set => Processor.Value = value;
        }

        public IFilterOperand Operand
        {
            get => new ValueOperand()
            {
                Value = Value,
                ValueType = Type
            };
            set
            {
                var operand = value as ValueOperand;
                Value = operand.Value;
                Type = operand.ValueType;
            }
        }
    }
}
