using Core.Data.Design.InternalData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.CardForm
{
    public partial class FormCardView : Form
    {
        private FormData form;

        public FormData Form
        {
            get => form;
            set
            {
                form = value;

                if (form != null)
                {
                    this.Size = new Size(form.Size.Width, form.Size.Height + 80);
                    modelCardView1.Form = form;
                }
            }
        }

        public FormCardView()
        {
            InitializeComponent();
        }

        private void FormCardView_Load(object sender, EventArgs e)
        {

        }
    }
}
