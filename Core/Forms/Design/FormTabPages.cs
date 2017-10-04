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
    public partial class FormTabPages : Form
    {
        private class ListBoxItem
        {
            public string Text { get; set; }

            public CardTabPage Page { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private List<CardTabPage> cardTabPages;

        public FormTabPages()
        {
            InitializeComponent();
        }

        private void ResetList()
        {
            listTabPages.Items.Clear();
            (listTabPages.Tag as List<CardTabPage>).ForEach(tp => 
                listTabPages.Items.Add(new ListBoxItem() { Text = tp.Text, Page = tp }));
        }

        public List<CardTabPage> CardTabPages
        {
            get
            {
                return cardTabPages;
            }
            set
            {
                listTabPages.Tag = value;
                cardTabPages = value;
                ResetList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialog = new FormCreateTabPage();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CardTabPages.Add(new CardTabPage() { Text = dialog.EnteredText });
                ResetList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {
                CardTabPages.Remove(item.Page);
                ResetList();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {

            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {

            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {

            }
        }
    }
}
