namespace Core.Forms.Design
{
    partial class FormEmpty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPages = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabPages
            // 
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 0);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(551, 475);
            this.tabPages.TabIndex = 0;
            this.tabPages.TabStop = false;
            this.tabPages.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabPages_Selected);
            // 
            // FormEmpty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 475);
            this.Controls.Add(this.tabPages);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEmpty";
            this.Text = "Форма";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEmpty_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormEmpty_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPages;
    }
}