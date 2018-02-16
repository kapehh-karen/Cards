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
using Core.Data.Field;
using Core.Filter.Data.Operator;

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
        
        public ICondition BuildCondition()
        {
            var item = new ItemCondition()
            {
                ConditionOperator = cmbConcatenate.SelectedConditionOperator,
                Operator = inputOperator.Operator,
                LeftOperand = inputOperandLeft.Operand,
                RightOperand = inputOperandRight.Operand
            };
            if (item.Operator != null) item.Operator.Condition = item;
            return item;
        }

        public void LoadCondition(ICondition condition)
        {
            // Можем загрузить только айтем
            if (condition.Type != ConditionType.ITEM)
                return;

            var item = condition as ItemCondition;
            inputOperandLeft.Operand = item.LeftOperand; // Left
            inputOperandRight.DependentType = item.LeftOperand?.ValueType ?? FieldType.UNKNOWN; // Right
            inputOperandRight.Operand = item.RightOperand;
            inputOperandRight.Field = inputOperandLeft.Field;
            inputOperator.DependentType = item.LeftOperand?.ValueType ?? FieldType.UNKNOWN; // Operator
            inputOperator.Operator = item.Operator;
            cmbConcatenate.SelectedConditionOperator = item.ConditionOperator; // Condition prefix
        }

        private void inputOperandLeft_OperandTypeChanged(object sender, EventArgs e)
        {
            inputOperator.DependentType = inputOperandLeft.Type;
            inputOperandRight.DependentType = inputOperandLeft.Type;
        }

        private void inputOperandLeft_OperandFieldChanged(object sender, EventArgs e)
        {
            inputOperandRight.DependentField = inputOperandLeft.Field;
        }

        private void inputOperator_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedType = inputOperator.Type;
            var onlyLeft = selectedType == OperatorType.IS_NULL || selectedType == OperatorType.IS_NOT_NULL;
            inputOperandRight.Visible = !onlyLeft;
        }

        private void btnActionDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
