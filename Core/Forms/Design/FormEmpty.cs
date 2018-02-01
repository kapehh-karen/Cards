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
        private HighlightFocusedControl highlight;

        public FormEmpty(FormData formData = null)
        {
            InitializeComponent();

            highlight = new HighlightFocusedControl(this);
            if (formData == null)
            {
                tabPages.TabPages.Add(SelectedTabPage = new CardTabPage() { Text = "Основная информация", Form = this });
            }
            else
            {
                SuspendLayout();
                LoadFromData(formData);
                ResumeLayout();
            }
            
            SetEventListeners();
        }

        private void LoadFromData(FormData formData)
        {
            var pages = formData.Pages.Select(page =>
            {
                var cardTabPage = new CardTabPage() { Text = page.Title, Form = this };
                cardTabPage.DesignControls = page.Controls.Select(cdata => MapDataToDesignControl(cdata, cardTabPage, cardTabPage)).ToList();
                return cardTabPage;
            }).ToArray();

            if (pages.Length > 0)
                SelectedTabPage = pages[0];

            tabPages.TabPages.AddRange(pages);
            Size = formData.Size;
        }

        private IDesignControl MapDataToDesignControl(ControlData control, Control parent, CardTabPage cardTabPage)
        {
            var type = Type.GetType(control.FullClassName);
            var element = Activator.CreateInstance(type) as IDesignControl;

            element.ParentControl = parent as IDesignControl;
            element.Properties.ForEach(property =>
            {
                var p = control.Properties.FirstOrDefault(pdata => pdata.Name == property.Name);
                
                if (p != null)
                    property.Value = p.Value;
            });
            element.DesignControls = control.Chields.Select(cdata => MapDataToDesignControl(cdata, element as Control, cardTabPage)).ToList();
            element.InDesigner = true;

            parent.Controls.Add(element as Control);
            cardTabPage.AddDesignControl(element);
            return element;
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
                Text = $"Форма - Выбранный режим .:: {value.Name} ::.";
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

                    // Для отлова KeyDown и выделения его рамкой
                    (value as Control).Focus();
                }
                else if (control != null)
                {
                    // Для потери фокуса с элементов
                    // NOTE: Вся эта пиздопляска для отрисовки Highlighter-а
                    this.ActiveControl = null;
                    this.Refresh();
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
            var c = sender is IDesignControl && !(sender is CardTabPage) ? sender as Control : null;
            FormBrush?.MouseDown(SelectedTabPage, c, e.Location);
        }

        public void FormEmpty_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = sender is IDesignControl && !(sender is CardTabPage) ? sender as Control : null;
                FormBrush?.MouseMove(SelectedTabPage, c, e.Location);
            }
        }

        public void FormEmpty_MouseUp(object sender, MouseEventArgs e)
        {
            var c = sender is IDesignControl && !(sender is CardTabPage) ? sender as Control : null;
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

        public void FormEmpty_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedControl != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        DeleteControl();
                        break;

                    default:
                        var c = sender is IDesignControl && !(sender is CardTabPage) ? sender as Control : null;
                        FormBrush?.KeyPress(SelectedTabPage, c, e);
                        break;
                }
            }
        }

        public void DeleteControl()
        {
            if (MessageBox.Show("Вы уверены что хотите удалить элемент?", Consts.ProgramTitle,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                SelectedControl.ParentControl?.DesignControls.Remove(SelectedControl);
                (SelectedControl as Control)?.Dispose();
                SelectedControl = null;
            }
        }

        private void FormEmpty_Load(object sender, EventArgs e)
        {
            highlight.Install();
        }
    }
}
