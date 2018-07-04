using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;

namespace Core.Data.Design.Controls.FieldControl
{
    public partial class BooleanControl : UserControl, IDesignControl
    {
        public event EventHandler CheckedChanged = (s, e) => { };

        public BooleanControl()
        {
            InitializeComponent();

            Properties.Add(new NameProperty(this));
            Properties.Add(new FontProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new FieldProperty(this) { AccessTypes = new FieldType[] { FieldType.BOOLEAN } });
            Properties.Add(new TabIndexProperty(this));
            Properties.Add(new SizeProperty(this)); // Сначала размер!
            Properties.Add(new TextProperty(this)); // А только потом текст!!! Потому-что текст меняет размер ширины
        }

        public override string Text
        {
            get => lblText.Text;
            set
            {
                lblText.Text = value;
                cmbFlag.Left = lblText.Left + lblText.Width + 5;
                Width = cmbFlag.Left + cmbFlag.Width + 5;
            }
        }

        public bool? Checked
        {
            get
            {
                switch (cmbFlag.SelectedIndex)
                {
                    case 1:
                        return false;
                    case 2:
                        return true;
                    default:
                        return null;
                }
            }
            set
            {
                switch (value)
                {
                    case false:
                        cmbFlag.SelectedIndex = 1;
                        break;
                    case true:
                        cmbFlag.SelectedIndex = 2;
                        break;
                    default:
                        cmbFlag.SelectedIndex = 0;
                        break;
                }
            }
        }

        private void cmbFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedChanged(this, EventArgs.Empty);
        }

        private void BooleanControl_Resize(object sender, EventArgs e)
        {
            lblText.Top = (Height - lblText.Height) / 2;
            cmbFlag.Top = (Height - cmbFlag.Height) / 2;
        }

        public DesignControlType ControlType => DesignControlType.FIELD;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        private bool inDesigner;
        public bool InDesigner
        {
            get => inDesigner;
            set
            {
                inDesigner = value;
                cmbFlag.Enabled = !value;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
