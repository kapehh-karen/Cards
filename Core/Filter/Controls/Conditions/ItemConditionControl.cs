using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Filter.Data;
using Core.Filter.Controls.Conditions;
using Core.Filter.Data.Condition;
using Core.Filter.Data.Condition.Impl;

namespace Core.Filter.Controls
{
    public partial class ItemConditionControl : UserControl, IConditionControl
    {
        public ItemConditionControl()
        {
            InitializeComponent();
            IsFirst = false;
        }

        private FilterData filterData;
        public FilterData FilterData
        {
            get => filterData;
            set
            {
                filterData = value;

                inputOperandLeft.FilterData = value;
                inputOperandRight.FilterData = value;
            }
        }

        private bool isFirst;
        public bool IsFirst
        {
            get => isFirst;
            set
            {
                isFirst = value;
                cmbConcatenate.Enabled = !isFirst;
            }
        }

        public ICondition Condition
        {
            get
            {
                var item = new ItemCondition()
                {
                    ConditionOperator = cmbConcatenate.SelectedConditionOperator,
                    Operator = inputOperator.Operator,
                    LeftOperand = inputOperandLeft.Operand,
                    RightOperand = inputOperandRight.Operand
                };
                item.Operator.Condition = item;
                return item;
            }
            set
            {

            }
        }

        private void inputOperandLeft_OperandTypeChanged(object sender, EventArgs e)
        {
            inputOperator.DependentType = inputOperandLeft.Type;
            inputOperandRight.DependentType = inputOperandLeft.Type;
        }

        private void btnActionDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
