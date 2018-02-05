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
    public partial class ContainerConditionControl : UserControl, IConditionControl
    {
        public ContainerConditionControl()
        {
            InitializeComponent();
        }
        
        public FilterData FilterData { get; set; }

        private bool isRoot = true;
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

        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Add(new ItemConditionControl() { FilterData = FilterData });
        }

        private void btnAddContainer_Click(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Add(new ContainerConditionControl()
            {
                FilterData = FilterData,
                IsRoot = false
            });
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
    }
}
