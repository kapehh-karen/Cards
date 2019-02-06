using Core.Data.Field;
using Core.SimpleFilter.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.SimpleFilter.Forms
{
    public partial class SimpleFilterForm : Form
    {
        public SimpleFilterForm()
        {
            InitializeComponent();
        }

        public void InitializeWithFields(IList<FieldData> fieldDatas)
        {
            foreach (var field in fieldDatas)
            {
                layoutContainer.Controls.Add(new FieldFilterItem
                {
                    Field = field
                });
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
