﻿namespace Core.Forms.Design
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
            this.SuspendLayout();
            // 
            // FormEmpty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 299);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEmpty";
            this.Text = "Новая форма";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormEmpty_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormEmpty_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}