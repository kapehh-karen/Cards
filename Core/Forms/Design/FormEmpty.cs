using Core.Data.Design.Controls;
using Core.Data.Design.FormBrushes;
using Core.Utils;
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

            ControlSelected += FormEmpty_ControlSelected;
            ControlRelease += FormEmpty_ControlRelease;
        }

        private void FormEmpty_ControlRelease(IDesignControl control)
        {

        }

        private void FormEmpty_ControlSelected(IDesignControl control)
        {

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
                this.Invalidate();
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
            var p = e.Location; // this.PointToClient(Cursor.Position);
            var c = sender is Control ? sender as Control : null;
            FormBrush?.MouseDown(this, c, p);
        }

        private void FormEmpty_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var p = e.Location;
                var c = sender is Control ? sender as Control : null;
                FormBrush?.MouseMove(this, c, p);
            }
        }

        private void FormEmpty_MouseUp(object sender, MouseEventArgs e)
        {
            var p = e.Location;
            var c = sender is Control ? sender as Control : null;
            FormBrush?.MouseUp(this, c, p);
        }

        private void FormEmpty_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedControl != null && SelectedControl is Control c)
            {
                var rect = c.Bounds;
                rect.Offset(-1, -1);
                rect.Inflate(1, 1);

                e.Graphics.DrawRectangle(Pens.Red, rect);
            }
        }
    }
}
