using System;
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

                // Изменяем размер формы под ширину текста. Чем больше текста тем шире форма
                var g = this.CreateGraphics();
                var rect = g.MeasureString(value, this.Font);
                this.Width = (int)rect.Width + 50;

                // По центру
                Rectangle area = Screen.FromControl(this).WorkingArea; 
                this.Top = (area.Height - this.Height) / 2;
                this.Left = (area.Width - this.Width) / 2;
            }
        }
    }
}
