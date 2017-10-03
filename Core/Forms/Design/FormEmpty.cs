using Core.Data.Design.Controls;
using Core.Data.Design.FormBrushes;
using Core.Data.Design.InternalData;
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
        private IFormBrush brush;

        public FormEmpty(FormData formData = null)
        {
            InitializeComponent();
            
            if (formData == null)
            {
                tabPages.TabPages.Add(SelectedTabPage = new TabPage("Страница"));
            }
            else
            {
                // TODO: Loading form data
            }

            ControlSelected += FormEmpty_ControlSelected;
            ControlRelease += FormEmpty_ControlRelease;
            SetEventListeners();
        }

        private void FormEmpty_ControlRelease(IDesignControl control)
        {

        }

        private void FormEmpty_ControlSelected(IDesignControl control)
        {

        }

        private TabPage SelectedTabPage { get; set; }

        public IFormBrush FormBrush
        {
            get
            {
                return brush;
            }
            set
            {
                brush?.DeactivateBrush(SelectedTabPage);
                brush = value;
                brush.ActivateBrush(SelectedTabPage);
            }
        }

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

        // TODO: Перенести в TabPage, т.к. теперь по табам раскидано
        public void AddDesignControl(IDesignControl control)
        {
            if (control is Control c)
            {
                c.MouseDown += FormEmpty_MouseDown;
                c.MouseMove += FormEmpty_MouseMove;
                c.MouseUp += FormEmpty_MouseUp;
            }
        }

        private void SetEventListeners()
        {
            foreach (TabPage page in tabPages.TabPages)
            {
                page.MouseDown -= FormEmpty_MouseDown;
                page.MouseDown += FormEmpty_MouseDown;

                page.MouseMove -= FormEmpty_MouseMove;
                page.MouseMove += FormEmpty_MouseMove;

                page.MouseUp -= FormEmpty_MouseUp;
                page.MouseUp += FormEmpty_MouseUp;
            }
        }

        private void FormEmpty_MouseDown(object sender, MouseEventArgs e)
        {
            var p = e.Location; // this.PointToClient(Cursor.Position);
            var c = sender is IDesignControl ? sender as Control : null;
            FormBrush?.MouseDown(SelectedTabPage, c, p);
        }

        private void FormEmpty_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var p = e.Location;
                var c = sender is IDesignControl ? sender as Control : null;
                FormBrush?.MouseMove(SelectedTabPage, c, p);
            }
        }

        private void FormEmpty_MouseUp(object sender, MouseEventArgs e)
        {
            var p = e.Location;
            var c = sender is IDesignControl ? sender as Control : null;
            FormBrush?.MouseUp(SelectedTabPage, c, p);
        }

        private void FormEmpty_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.UserClosing;
        }

        private void tabPages_Selected(object sender, TabControlEventArgs e)
        {
            SelectedTabPage = e.TabPage;
        }
    }
}
