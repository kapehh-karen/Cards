using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Filter.Data;

namespace Core.Filter.Controls
{
    public partial class ItemConditionControl : UserControl
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

        private void inputOperand1_OperandTypeChanged(object sender, EventArgs e)
        {
            inputOperator.DependentType = inputOperandLeft.Type;
            inputOperandRight1.DependentType = inputOperandLeft.Type;
        }
    }
}
