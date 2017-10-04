﻿using Core.Data.Design.Controls;
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
                tabPages.TabPages.Add(SelectedTabPage = new CardTabPage() { Text = "Страница 1", Form = this });
            }
            else
            {
                // TODO: Loading form data
            }

            ControlSelected += FormEmpty_ControlSelected;
            ControlRelease += FormEmpty_ControlRelease;
            SetEventListeners();
        }

        public List<CardTabPage> CardTabPages
        {
            get
            {
                var list = new List<CardTabPage>();
                foreach (var item in tabPages.TabPages)
                    list.Add(item as CardTabPage);
                return list;
            }
            set
            {
                SelectedControl = null;
                tabPages.TabPages.Clear();
                foreach (var page in value)
                {
                    page.Form = this;
                    tabPages.TabPages.Add(page);
                }
                SelectTabPage(value[0]);
                SetEventListeners();
            }
        }

        private void FormEmpty_ControlRelease(IDesignControl control)
        {
            if (control is Control c && c != null)
                c.BackColor = SystemColors.Control;
        }

        private void FormEmpty_ControlSelected(IDesignControl control)
        {
            if (control is Control c && c != null)
                c.BackColor = Color.Red;
        }

        private CardTabPage SelectedTabPage { get; set; }

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
                ControlRelease?.Invoke(control);

                if (value != null)
                {
                    ControlSelected?.Invoke(value);
                }

                control = value;
            }
        }

        private void SetEventListeners()
        {
            foreach (CardTabPage page in tabPages.TabPages)
            {
                page.MouseDown -= FormEmpty_MouseDown;
                page.MouseDown += FormEmpty_MouseDown;

                page.MouseMove -= FormEmpty_MouseMove;
                page.MouseMove += FormEmpty_MouseMove;

                page.MouseUp -= FormEmpty_MouseUp;
                page.MouseUp += FormEmpty_MouseUp;
            }
        }

        public void FormEmpty_MouseDown(object sender, MouseEventArgs e)
        {
            var c = sender is IDesignControl ? sender as Control : null;
            FormBrush?.MouseDown(SelectedTabPage, c, e.Location);
        }

        public void FormEmpty_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = sender is IDesignControl ? sender as Control : null;
                FormBrush?.MouseMove(SelectedTabPage, c, e.Location);
            }
        }

        public void FormEmpty_MouseUp(object sender, MouseEventArgs e)
        {
            var c = sender is IDesignControl ? sender as Control : null;
            FormBrush?.MouseUp(SelectedTabPage, c, e.Location);
        }

        private void FormEmpty_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.UserClosing;
        }

        private void SelectTabPage(TabPage tabPage)
        {
            if (SelectedTabPage != null)
                FormBrush?.DeactivateBrush(SelectedTabPage);

            if (tabPage == null)
            {
                SelectedTabPage = null;
            }
            else
            {
                SelectedTabPage = tabPage as CardTabPage;
                FormBrush?.ActivateBrush(SelectedTabPage);
            }
        }

        private void tabPages_Selected(object sender, TabControlEventArgs e)
        {
            SelectTabPage(e.TabPage);
        }
    }
}
