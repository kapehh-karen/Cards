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

namespace Core.Filter.Controls
{
    public partial class InputOperand : UserControl, IInputOperand
    {
        public event EventHandler OperandTypeChanged = (s, e) => { };

        public InputOperand()
        {
            InitializeComponent();
        }

        public FilterData FilterData { get; set; }

        private FieldType dependentType = FieldType.UNKNOWN;
        /// <summary>
        /// Тип от которого зависит операнд. Если не задан, то это ведущий операнд.
        /// </summary>
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

        private Control inputControl;
        public Control InputControl
        {
            get => inputControl;
            set
            {
                if (inputControl != null)
                {
                    (inputControl as IInputOperand).OperandTypeChanged -= OperandTypeChanged;
                    inputControl.Dispose();
                    inputControl = null;
                }

                if (value == null)
                    return;

                inputControl = value;
                panel1.Controls.Add(inputControl);
                inputControl.Dock = DockStyle.Fill;
                (inputControl as IInputOperand).OperandTypeChanged += OperandTypeChanged;
            }
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

        private void btnSelectInput_Click(object sender, EventArgs e)
        {
            contextMenuStripInput.Show(btnSelectInput, 0, btnSelectInput.Height);
        }

        private void constToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputValue();
            Type = DependentType;
        }

        private void fieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputControl = new InputField() { FilterData = FilterData };
            Type = DependentType;
        }

        private void subqueryToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
