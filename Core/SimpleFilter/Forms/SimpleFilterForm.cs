using Core.Data.Field;
using Core.Helper;
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
        private List<FieldFilterItem> items = new List<FieldFilterItem>();

        public SimpleFilterForm()
        {
            InitializeComponent();
        }

        public void InitializeWithFields(IList<FieldData> fieldDatas)
        {
            foreach (var field in fieldDatas)
            {
                var item = new FieldFilterItem
                {
                    Field = field
                };

                items.Add(item);
                layoutContainer.Controls.Add(item);
            }
        }

        public string ResultFilter { get; private set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (items.Any(x => !string.IsNullOrEmpty(x.Value)))
            {
                var values = items
                    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                    .Select(x => DataGridViewHelper.BuildRowFilter(x.Field, x.Value));
                ResultFilter = string.Join(" AND ", values);
            }
            else
            {
                ResultFilter = string.Empty;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
