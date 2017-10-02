using Core.Data.Design.Controls;
using Core.Data.Design.FormBrushes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Design
{
    public partial class FormEmpty : Form
    {
        public delegate void EventDesignControl(IDesignControl control);
        public event EventDesignControl ControlSelected;
        public event EventDesignControl ControlRelease;

        private IDesignControl control;

        public FormEmpty()
        {
            InitializeComponent();
        }

        public IFormBrush FormBrush { get; set; }

        public IDesignControl SelectedControl
        {
            get
            {
                return control;
            }
            set
            {
                if (value == null)
                    ControlRelease?.Invoke(control);
                else
                    ControlSelected?.Invoke(value);

                control = value;
            }
        }

        public List<IDesignControl> DesignControls { get; } = new List<IDesignControl>();

        public void AddDesignControl(IDesignControl control)
        {
            if (control is Control c)
            {
                c.MouseDown += FormEmpty_MouseDown;
                c.MouseMove += FormEmpty_MouseMove;
                c.MouseUp += FormEmpty_MouseUp;
            }
        }

        private void FormEmpty_MouseDown(object sender, MouseEventArgs e)
        {
            var p = this.PointToClient(Cursor.Position);
            FormBrush?.MouseDown(this, this.GetChildAtPoint(p), p);
        }

        private void FormEmpty_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var p = this.PointToClient(Cursor.Position);
                FormBrush?.MouseMove(this, this.GetChildAtPoint(p), p);
            }
        }

        private void FormEmpty_MouseUp(object sender, MouseEventArgs e)
        {
            var p = this.PointToClient(Cursor.Position);
            FormBrush?.MouseUp(this, this.GetChildAtPoint(p), p);
        }
    }
}
