﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using Core.Data.Field;
using Core.Data.Table;
using Core.Config;

namespace TestApp
{
    public partial class frmSerializer : Form
    {
        public frmSerializer()
        {
            InitializeComponent();
        }

        [DataContract]
        public class Testik
        {
            [DataMember]
            public FieldData Field { get; set; }

            [DataMember]
            public TableData Table { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
