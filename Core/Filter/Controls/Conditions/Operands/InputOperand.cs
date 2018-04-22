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
using Core.Filter.Data.Operator;

namespace Core.Filter.Controls.Conditions.Operands
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
        
        private void UpdateEnabledInput()
        {
            // Только к типу NUMERIC можно применять выборку
            var isNumeric = DependentType == FieldType.NUMBER;

            // Оператор LIKE можно применять только к Значению
            var isLike = DependentOperator == OperatorType.LIKE || DependentOperator == OperatorType.NOT_LIKE;

            if (!isNumeric && (InputControl as IInputOperand)?.OperandType == OperandType.SUBQUERY)
            {
                RecreateOperandControl(OperandType.VALUE);
            }
            else if (isLike && (InputControl as IInputOperand)?.OperandType != OperandType.VALUE)
            {
                RecreateOperandControl(OperandType.VALUE);
            }
            
            fieldToolStripMenuItem.Enabled = !isLike;
            subqueryToolStripMenuItem.Enabled = isNumeric && !isLike;
        }

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

                    UpdateEnabledInput();
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

        private OperatorType dependentOperator = OperatorType.UNKNOWN;
        public OperatorType DependentOperator
        {
            get => dependentOperator;
            set
            {
                if (dependentOperator != value)
                {
                    dependentOperator = value;

                    UpdateEnabledInput();
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
            constToolStripMenuItem.ForeColor = InputControl is InputValue ? Color.DarkViolet : Color.Black;
            fieldToolStripMenuItem.ForeColor = InputControl is InputField ? Color.DarkViolet : Color.Black;
            subqueryToolStripMenuItem.ForeColor = InputControl is InputSubquery ? Color.DarkViolet : Color.Black;
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

        public OperandType OperandType => (InputControl as IInputOperand).OperandType;

        private void btnSelectInput_Click(object sender, EventArgs e)
        {
            contextMenuStripInput.Show(btnSelectInput, 0, btnSelectInput.Height);
        }

        private void constToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputValue() { FilterData = FilterData };
            Field = DependentField;
            Type = DependentType;
        }

        private void fieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputField() { FilterData = FilterData };
            Field = DependentField;
            Type = DependentType;
        }

        private void subqueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputSubquery() { FilterData = FilterData };
            Field = DependentField;
            Type = DependentType;
        }
        
        public IFilterOperand Operand
        {
            get => (InputControl as IInputOperand)?.Operand;
            set
            {
                // Если не задан операнд
                if (value == null)
                    return;

                RecreateOperandControl(value.Type);

                if (InputControl != null) (InputControl as IInputOperand).Operand = value;
            }
        }

        private void RecreateOperandControl(OperandType operandType)
        {
            if (InputControl != null && (InputControl as IInputOperand).OperandType == operandType)
                return;

            switch (operandType)
            {
                case OperandType.FIELD:
                    fieldToolStripMenuItem_Click(null, null);
                    break;
                case OperandType.VALUE:
                    constToolStripMenuItem_Click(null, null);
                    break;
                case OperandType.SUBQUERY:
                    subqueryToolStripMenuItem_Click(null, null);
                    break;
                default:
                    return;
            }
        }
    }
}
