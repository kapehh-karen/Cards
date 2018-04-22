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
using Core.Filter.Data.Operator.Impl;
using Core.Filter.Data.Operand.Impl;

namespace Core.Filter.Controls
{
    public partial class ContainerConditionControl : UserControl, IConditionControl
    {
        public ContainerConditionControl()
        {
            InitializeComponent();
            IsRoot = false;
            IsFirst = false;
        }
        
        public FilterData FilterData { get; set; }

        private bool isRoot;
        public bool IsRoot
        {
            get => isRoot;
            set
            {
                isRoot = value;
                AutoSize = !isRoot;
                flowLayoutPanel.AutoSize = !isRoot;
                flowLayoutPanel.AutoScroll = isRoot;
                cmbConcatenate.Enabled = !isRoot;
                btnActionDelete.Enabled = !isRoot;
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
            return new ContainerCondition()
            {
                ConditionOperator = cmbConcatenate.SelectedConditionOperator,
                Conditions = flowLayoutPanel.Controls.Cast<IConditionControl>().Select(c => c.BuildCondition()).ToList()
            };
        }

        public void LoadCondition(ICondition condition)
        {
            if (condition.Type != ConditionType.CONTAINER)
                return;

            var group = condition as ContainerCondition;
            cmbConcatenate.SelectedConditionOperator = group.ConditionOperator;
            flowLayoutPanel.Controls.Clear();
            group.Conditions?.ForEach(cond =>
            {
                var control = CreateConditionControl(cond.Type);
                control.LoadCondition(cond);
                flowLayoutPanel.Controls.Add(control as Control);
            });
        }

        private IConditionControl CreateConditionControl(ConditionType typeCondition)
        {
            switch (typeCondition)
            {
                case ConditionType.ITEM:
                    return new ItemConditionControl()
                    {
                        FilterData = FilterData
                    };
                case ConditionType.CONTAINER:
                    return new ContainerConditionControl()
                    {
                        FilterData = FilterData
                    };
                default:
                    return null;
            }
        }

        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            var itemCond = CreateConditionControl(ConditionType.ITEM);
            // По-умолчанию для добавляемых условий делаю оператор равенства и правый операнд значения
            itemCond.LoadCondition(new ItemCondition()
            {
                Operator = new EqualOperator(),
                RightOperand = new ValueOperand()
            });
            flowLayoutPanel.Controls.Add(itemCond as Control);
        }

        private void btnAddContainer_Click(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Add(CreateConditionControl(ConditionType.CONTAINER) as Control);
        }

        private void UpdateUI()
        {
            if (flowLayoutPanel.Controls.Count > 0)
                (flowLayoutPanel.Controls[0] as IConditionControl).IsFirst = true;
        }

        private void flowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            UpdateUI();
        }

        private void flowLayoutPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            UpdateUI();
        }

        private void btnActionDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
