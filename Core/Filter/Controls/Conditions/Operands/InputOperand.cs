using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Filter.Data;
using Core.Data.Field;
using Core.Filter.Data.Operand;
using Core.Filter.Controls.Conditions.Operands;

namespace Core.Filter.Controls
{
    public partial class InputOperand : UserControl, IInputOperand
    {
        public event EventHandler OperandTypeChanged = (s, e) => { };

        public event EventHandler OperandFieldChanged = (s, e) => { };

        public InputOperand()
        {
            InitializeComponent();
        }

        public FilterData FilterData { get; set; }

        // Тип от которого зависит операнд. Если не задан, то это ведущий операнд.
        private FieldType dependentType = FieldType.UNKNOWN;
        public FieldType DependentType
        {
            get => dependentType;
            set
            {
                if (dependentType != value)
                {
                    dependentType = value;
                    constToolStripMenuItem.Enabled = dependentType != FieldType.UNKNOWN;
                    Type = dependentType;
                }
            }
        }

        // Поле от которого зависит операнд
        private FieldData dependentField = null;
        public FieldData DependentField
        {
            get => dependentField;
            set
            {
                if (dependentField != value)
                {
                    dependentField = value;
                    Field = dependentField;
                }
            }
        }

        private Control inputControl;
        public Control InputControl
        {
            get => inputControl;
            set
            {
                IInputOperand inputOperand;

                if (inputControl != null)
                {
                    inputOperand = inputControl as IInputOperand;
                    inputOperand.OperandTypeChanged -= OperandTypeChanged;
                    inputOperand.OperandFieldChanged -= OperandFieldChanged;
                    inputControl.Dispose();
                    inputControl = null;
                }

                if (value == null)
                    return;

                inputControl = value;
                inputControl.Dock = DockStyle.Fill;
                inputOperand = inputControl as IInputOperand;
                inputOperand.OperandTypeChanged += OperandTypeChanged;
                inputOperand.OperandFieldChanged += OperandFieldChanged;
                panel1.Controls.Add(inputControl);

                UpdateSelectedInputType();
            }
        }

        private void UpdateSelectedInputType()
        {
            constToolStripMenuItem.ForeColor = inputControl is InputValue ? Color.DarkViolet : Color.Black;
            fieldToolStripMenuItem.ForeColor = inputControl is InputField ? Color.DarkViolet : Color.Black;
            subqueryToolStripMenuItem.ForeColor = inputControl is InputSubquery ? Color.DarkViolet : Color.Black;
        }

        public FieldType Type
        {
            get => InputControl != null ? (InputControl as IInputOperand).Type : FieldType.UNKNOWN;
            set
            {
                if (InputControl != null)
                    (InputControl as IInputOperand).Type = value;
            }
        }

        public FieldData Field
        {
            get => (InputControl as IInputOperand)?.Field;
            set
            {
                if (InputControl != null)
                    (InputControl as IInputOperand).Field = value;
            }
        }

        private void btnSelectInput_Click(object sender, EventArgs e)
        {
            contextMenuStripInput.Show(btnSelectInput, 0, btnSelectInput.Height);
        }

        private void constToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputValue();
            Type = DependentType;
            Field = DependentField;
        }

        private void fieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputField() { FilterData = FilterData };
            Type = DependentType;
            Field = DependentField;
        }

        private void subqueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputSubquery() { FilterData = FilterData };
            Type = DependentType;
            Field = DependentField;
        }
        
        public IFilterOperand Operand
        {
            get => (InputControl as IInputOperand)?.Operand;
            set
            {
                // Если не задан операнд
                if (value == null)
                    return;

                switch (value.Type)
                {
                    case OperandType.FIELD:
                        fieldToolStripMenuItem_Click(null, null);
                        break;
                    case OperandType.VALUE:
                        constToolStripMenuItem_Click(null, null);
                        break;
                    default:
                        return;
                }

                if (InputControl != null) (InputControl as IInputOperand).Operand = value;
            }
        }
    }
}
