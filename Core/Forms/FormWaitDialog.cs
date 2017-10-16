﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms
{
    public partial class FormWaitDialog : Form
    {
        public FormWaitDialog()
        {
            InitializeComponent();
        }

        public string Message {
            get => lblMessage.Text;
            set
            {
                lblMessage.Text = value;

                var g = this.CreateGraphics();
                var rect = g.MeasureString(value, this.Font);
                Width = (int)rect.Width + 50;
            }
        }
    }
}
