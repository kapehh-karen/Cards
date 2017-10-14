using Core.Config;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.Standard;
using Core.Data.Design.FormBrushes;
using Core.Data.Design.InternalData;
using Core.Data.Design.Properties;
using Core.Data.Table;
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
    public partial class FormDesigner : Form
    {
        private FormEmpty frmEmpty;
        private CursorBrush cursorBrush;
        private IDesignControl selectedControl;

        private bool closing;

        public FormDesigner()
        {
            InitializeComponent();
        }

        private void InitializeEmptyForm()
        {
            if (frmEmpty != null)
            {
                frmEmpty.Close();
                frmEmpty.Dispose();
            }

            frmEmpty = new FormEmpty(this.FormData)
            {
                MdiParent = this,
                Location = new Point(0, 0),
                FormBrush = cursorBrush
            };
            frmEmpty.Show();

            frmEmpty.ControlSelected += FrmEmpty_ControlSelected;
            frmEmpty.ControlRelease += FrmEmpty_ControlRelease;
            frmEmpty.SelectedControl = null;
        }

        private void FormDesigner_Load(object sender, EventArgs e)
        {
            FillListViewControls();
            InitializeEmptyForm();

            this.Text = $"Дизайнер формы для таблицы - {TableData.Name}";
        }

        private void FillListViewControls()
        {
            listViewControls.Items.Add(new ListViewItem() { Text = "- Указатель -", Tag = cursorBrush = new CursorBrush() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Надпись", Tag = new CreateLabelControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Группировка", Tag = new CreateGroupBoxControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Текстовое значение", Tag = new CreateTextControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Логическое значение", Tag = new CreateBooleanControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Связанное значение", Tag = new CreateBindControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Таблица внешних данных", Tag = new CreateLinkedTableControl() });
        }

        private void FrmEmpty_ControlRelease(IDesignControl control)
        {
            selectedControl = null;
            listViewProperties.Items.Clear();
            btnDelete.Enabled = false;
        }

        private void FrmEmpty_ControlSelected(IDesignControl control)
        {
            selectedControl = control;
            listViewProperties.Items.Clear();
            btnDelete.Enabled = true;

            foreach (var proper in control.Properties)
            {
                var lvi = new ListViewItem() { Text = proper.Name, Tag = proper };
                lvi.SubItems.Add(proper.Value?.ToString());
                listViewProperties.Items.Add(lvi);
            }

            frmEmpty.FormBrush = cursorBrush;
            listViewControls.SelectedItems.Clear();
        }

        private void listViewControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                frmEmpty.FormBrush = listView.SelectedItems[0].Tag as IFormBrush;
            }
        }

        private void listViewProperties_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                var proper = listView.SelectedItems[0].Tag as IControlProperties;

                // refresh properties
                if (proper.ChangeValue(TableData))
                {
                    FrmEmpty_ControlSelected(selectedControl);
                }
            }
        }

        private void tabPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FormTabPages() { CardTabPages = frmEmpty.CardTabPages };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                frmEmpty.CardTabPages = dialog.CardTabPages;
            }
        }

        public FormData FormData { get; set; }

        public TableData TableData { get; set; }

        private FormData GetFormData()
        {
            return new FormData()
            {
                Size = frmEmpty.Size,
                Pages = frmEmpty.CardTabPages.Select(tp => new PageData()
                {
                    Title = tp.Text,
                    Controls = tp.DesignControls.Select(MapDesignControlToData).ToList()
                }).ToList()
            };
        }

        private ControlData MapDesignControlToData(IDesignControl control)
        {
            return new ControlData()
            {
                FullClassName = control.GetType().FullName,
                Chields = control.DesignControls.Select(MapDesignControlToData).ToList(),
                Properties = control.Properties.Select(p => new PropertyData()
                {
                    Name = p.Name,
                    Value = p.Value
                }).ToList()
            };
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormData = GetFormData();
            closing = true;
            DialogResult = DialogResult.OK;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closing = true;
            DialogResult = DialogResult.Cancel;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmEmpty?.DeleteControl();
        }

        private void FormDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closing)
            {
                closing = false;
                return;
            }

            if (MessageBox.Show("Сохранить все изменения?", "Редактор форм",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.FormData = GetFormData();
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
