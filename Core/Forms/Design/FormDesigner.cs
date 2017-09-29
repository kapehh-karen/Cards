﻿using Core.Data.Design.Controls;
using Core.Data.Design.Controls.Standard;
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
        private class ControlItem
        {
            public Func<IDesignControl> CreateControll { get; set; }
        }

        private FormEmpty frmEmpty = new FormEmpty();

        public FormDesigner()
        {
            InitializeComponent();
        }

        private void FillListViewControls()
        {
            listViewControls.Items.Add(new ListViewItem() { Text = "Label", Tag = new ControlItem() { CreateControll = () => new LabelControl() } });
        }

        private void FillListViewProperties(IDesignControl control)
        {
            listViewProperties.Items.Clear();
            
            foreach (var proper in control.Properties)
            {
                listViewProperties.Items.Add(new ListViewItem() { Text = proper.Name, Tag = proper });
            }
        }

        private void FormDesigner_Load(object sender, EventArgs e)
        {
            frmEmpty.MdiParent = this;
            frmEmpty.Location = new Point(0, 0);
            frmEmpty.Show();

            FillListViewControls();
        }

        private void listViewControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                var cc = listView.SelectedItems[0].Tag as ControlItem;
                var control = cc.CreateControll();
                
                frmEmpty.Controls.Add(control.FormControl);

                FillListViewProperties(control);
            }
        }

        private void listViewProperties_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                var proper = listView.SelectedItems[0].Tag as IControlProperties;

                if (proper.ChangeValue())
                {
                    MessageBox.Show("UPDATED");
                }
            }
        }
    }
}
