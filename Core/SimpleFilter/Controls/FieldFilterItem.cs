using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Field;

namespace Core.SimpleFilter.Controls
{
    public partial class FieldFilterItem : UserControl
    {
        private FieldData _field;

        public FieldFilterItem()
        {
            InitializeComponent();
        }

        public FieldData Field
        {
            get => _field;
            set
            {
                _field = value ?? throw new ArgumentNullException(nameof(value));
                lblFieldName.Text = _field.DisplayName;
            }
        }

        public string Value => txtValue.Text;
    }
}
