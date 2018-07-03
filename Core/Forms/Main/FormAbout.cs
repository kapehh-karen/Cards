using Core.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            foreach (var plugin in PluginManager.Instance.Plugins)
            {
                var item = new ListViewItem(plugin.Version);
                item.SubItems.Add(plugin.Name);
                item.SubItems.Add(plugin.Author);
                listViewPlugins.Items.Add(item);
            }
        }
    }
}
