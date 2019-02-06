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
            this.SuspendLayout();
            // 
            // layoutContainer
            // 
            this.layoutContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutContainer.AutoScroll = true;
            this.layoutContainer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.layoutContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.layoutContainer.Location = new System.Drawing.Point(12, 12);
            this.layoutContainer.Name = "layoutContainer";
            this.layoutContainer.Size = new System.Drawing.Size(625, 335);
            this.layoutContainer.TabIndex = 0;
            this.layoutContainer.WrapContents = false;
            // 
            // SimpleFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 359);
            this.Controls.Add(this.layoutContainer);
            this.Name = "SimpleFilterForm";
            this.Text = "SimpleFilterForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel layoutContainer;
    }
}