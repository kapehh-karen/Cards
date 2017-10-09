using Core.Helper;
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
                listTabPages.Items.Add(new ListBoxItem() { Text = tp.TempString ?? tp.Text, Page = tp }));
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

                // clear saved text
                cardTabPages.ForEach(tp => tp.TempString = null);

                ResetList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // save entered text
            cardTabPages.Where(tp => !string.IsNullOrEmpty(tp.TempString)).ForEach(tp => tp.Text = tp.TempString);

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
                var index = CardTabPages.IndexOf(item.Page);

                if (index > 0)
                {
                    CardTabPages.Remove(item.Page);
                    CardTabPages.Insert(index - 1, item.Page);
                    ResetList();
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {
                var index = CardTabPages.IndexOf(item.Page);

                if (index < CardTabPages.Count - 1)
                {
                    CardTabPages.Remove(item.Page);
                    CardTabPages.Insert(index + 1, item.Page);
                    ResetList();
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (listTabPages.SelectedItem is ListBoxItem item && item != null)
            {
                var dialog = new FormCreateTabPage() { EnteredText = item.Page.Text };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item.Page.TempString = dialog.EnteredText;
                    ResetList();
                }
            }
        }
    }
}
