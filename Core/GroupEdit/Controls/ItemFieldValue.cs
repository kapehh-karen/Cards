using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Field;

namespace Core.GroupEdit.Controls
{
    public partial class ItemFieldValue : UserControl
    {
        public event EventHandler ItemDelete = (s, e) => { };

        public ItemFieldValue()
        {
            InitializeComponent();
        }

        private FieldData field = null;
        public FieldData Field
        {
            get => field;
            set
            {
                field = value;
                if (field != null)
                {
                    lblFieldName.Text = field.DisplayName;
                    controlFieldValue.Field = field;
                }
            }
        }

        public object Value => controlFieldValue?.Value;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ItemDelete(this, EventArgs.Empty);
            this.Dispose();
        }
    }
}
