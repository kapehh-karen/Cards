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

namespace Core.Filter.Controls
{
    public partial class ItemConditionControl : UserControl, IConditionControl
    {
        public ItemConditionControl()
        {
            InitializeComponent();
        }

        private FilterData filterData;
        public FilterData FilterData
        {
            get => filterData;
            set
            {
                filterData = value;

                inputOperandLeft.FilterData = value;
                inputOperandRight1.FilterData = value;
            }
        }

        private bool isFirst = false;
        public bool IsFirst
        {
            get => isFirst;
            set
            {
                isFirst = value;
                cmbConcatenate.Enabled = !isFirst;
            }
        }

        private void inputOperand1_OperandTypeChanged(object sender, EventArgs e)
        {
            inputOperator.DependentType = inputOperandLeft.Type;
            inputOperandRight1.DependentType = inputOperandLeft.Type;
        }

        private void btnActionDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
