using Core.Filter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Filter.Forms
{
    public partial class FormFilter : Form
    {
        public FormFilter()
        {
            InitializeComponent();
        }

        private void FormFilter_Load(object sender, EventArgs e)
        {

        }

        private FilterData filterData;
        public FilterData FilterData
        {
            get => filterData;
            set
            {
                value = filterData;

                if (filterData != null)
                {

                }
            }
        }
    }
}
