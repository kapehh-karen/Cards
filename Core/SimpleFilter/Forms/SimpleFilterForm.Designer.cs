namespace Core.SimpleFilter.Forms
{
    partial class SimpleFilterForm
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
            this.layoutContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // layoutContainer
            // 
            this.layoutContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutContainer.AutoScroll = true;
            this.layoutContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.layoutContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.layoutContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.layoutContainer.Location = new System.Drawing.Point(12, 12);
            this.layoutContainer.Name = "layoutContainer";
            this.layoutContainer.Size = new System.Drawing.Size(540, 269);
            this.layoutContainer.TabIndex = 0;
            this.layoutContainer.WrapContents = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(432, 287);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Применить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SimpleFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 323);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.layoutContainer);
            this.Name = "SimpleFilterForm";
            this.Text = "Быстрый фильтр";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel layoutContainer;
        private System.Windows.Forms.Button btnOk;
    }
}