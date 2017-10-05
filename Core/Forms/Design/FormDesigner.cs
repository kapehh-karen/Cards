using Core.Config;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.Standard;
using Core.Data.Design.FormBrushes;
using Core.Data.Design.InternalData;
using Core.Data.Design.Properties;
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

        public FormDesigner()
        {
            InitializeComponent();
        }

        private void InitializeEmptyForm(FormData formData = null)
        {
            if (frmEmpty != null)
            {
                frmEmpty.Close();
                frmEmpty.Dispose();
            }

            frmEmpty = new FormEmpty(formData);
            frmEmpty.MdiParent = this;
            frmEmpty.Location = new Point(0, 0);
            frmEmpty.FormBrush = cursorBrush;
            frmEmpty.Show();

            frmEmpty.ControlSelected += FrmEmpty_ControlSelected;
            frmEmpty.ControlRelease += FrmEmpty_ControlRelease;
            frmEmpty.SelectedControl = null;
        }

        private void FormDesigner_Load(object sender, EventArgs e)
        {
            FillListViewControls();
            InitializeEmptyForm();
        }

        private void FillListViewControls()
        {
            listViewControls.Items.Add(new ListViewItem() { Text = "- Указатель -", Tag = cursorBrush = new CursorBrush() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Надпись", Tag = new CreateLabelControl() });
            listViewControls.Items.Add(new ListViewItem() { Text = "Группировка", Tag = new CreateGroupBoxControl() });
        }

        private void FrmEmpty_ControlRelease(IDesignControl control)
        {
            listViewProperties.Items.Clear();
        }

        private void FrmEmpty_ControlSelected(IDesignControl control)
        {
            listViewProperties.Items.Clear();

            foreach (var proper in control.Properties)
            {
                listViewProperties.Items.Add(new ListViewItem() { Text = proper.Name, Tag = proper });
            }
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
                proper.ChangeValue();
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
        
        private FormData GetFormData()
        {
            return new FormData()
            {
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
            var configForm = new Configuration<FormData>();
            configForm.WriteToFile(GetFormData(), "form.xml");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configForm = new Configuration<FormData>();
            var formData = configForm.ReadFromFile("form.xml");
            InitializeEmptyForm(formData);
        }
    }
}
