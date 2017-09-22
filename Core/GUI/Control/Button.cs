using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.GUI.Control
{
    public partial class Button : UserControl
    {
        public event EventHandler PressedButton;

        private enum State
        {
            NORMAL = 0,
            HOVERED = 1,
            PRESSED = 2,
        }

        private Pen penControlNormalBorder = new Pen(SystemColors.ControlDark, 1);
        private Pen penControlHoveredBorder = new Pen(Color.DodgerBlue, 1);
        private Pen penControlFocusedBorder = new Pen(Color.DodgerBlue, 5);

        private string _text;
        private Point positionText = new Point();
        private State state = State.NORMAL;
        private bool hovered = false;
        private Keys keypressed = 0;

        public Button()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.ContainerControl, false);

            this.GotFocus += Button_GotFocus;
            this.LostFocus += Button_LostFocus;

            this.Text = this.Name;
            this.Cursor = Cursors.Hand;
        }

        private void Button_LostFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Button_GotFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void RecalculateSizeText()
        {
            Graphics graphics = Graphics.FromHwnd(this.Handle);
            var sizeText = graphics.MeasureString(_text, this.Font);
            positionText.X = (int)((this.Width - sizeText.Width) / 2) + 1;
            positionText.Y = (int)((this.Height - sizeText.Height) / 2) + 1;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RecalculateSizeText();
                this.Invalidate();
            }
        }

        private void ChangeStatus(State newState)
        {
            this.state = newState;
            this.Invalidate();
        }
        
        private void Button_Load(object sender, EventArgs e)
        {

        }

        private void Button_Paint(object sender, PaintEventArgs e)
        {
            switch (this.state)
            {
                case State.NORMAL:
                    e.Graphics.Clear(SystemColors.Control);
                    if (this.Focused)
                    {
                        e.Graphics.DrawRectangle(penControlFocusedBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(penControlNormalBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, positionText);
                    break;

                case State.HOVERED:
                    e.Graphics.Clear(Color.LightCyan);
                    e.Graphics.DrawRectangle(penControlHoveredBorder, 0, 0, this.Width - 1, this.Height - 1);
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, positionText);
                    break;

                case State.PRESSED:
                    e.Graphics.Clear(Color.PowderBlue);
                    e.Graphics.DrawRectangle(penControlHoveredBorder, 0, 0, this.Width - 1, this.Height - 1);
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, positionText);
                    break;
            }
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            RecalculateSizeText();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            this.hovered = true;
            ChangeStatus(State.HOVERED);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            this.hovered = false;
            ChangeStatus(State.NORMAL);
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            ChangeStatus(State.PRESSED);
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeStatus(State.NORMAL);
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (state == State.NORMAL)
            {
                ChangeStatus(State.HOVERED);
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            this.PressedButton?.Invoke(this, new EventArgs());
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            keypressed = e.KeyCode;

            ChangeStatus(State.PRESSED);
        }

        private void Button_KeyUp(object sender, KeyEventArgs e)
        {
            if (keypressed != e.KeyCode)
                return;
            keypressed = 0;

            ChangeStatus(this.hovered ? State.HOVERED : State.NORMAL);

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                this.PressedButton?.Invoke(this, new EventArgs());
        }
    }
}
