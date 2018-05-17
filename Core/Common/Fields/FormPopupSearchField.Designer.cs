namespace Core.Common.Fields
{
    partial class FormPopupSearchField
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
            this.txtSearchField = new System.Windows.Forms.TextBox();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtSearchField
            // 
            this.txtSearchField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchField.Location = new System.Drawing.Point(3, 3);
            this.txtSearchField.Name = "txtSearchField";
            this.txtSearchField.Size = new System.Drawing.Size(259, 20);
            this.txtSearchField.TabIndex = 0;
            this.txtSearchField.TextChanged += new System.EventHandler(this.txtSearchField_TextChanged);
            this.txtSearchField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchField_KeyUp);
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(3, 25);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(259, 264);
            this.lstFields.Sorted = true;
            this.lstFields.TabIndex = 1;
            this.lstFields.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFields_KeyPress);
            this.lstFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFields_MouseDoubleClick);
            // 
            // FormPopupSearchField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(265, 292);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.txtSearchField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPopupSearchField";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выбор поля";
            this.Deactivate += new System.EventHandler(this.FormPopupSearchField_Deactivate);
            this.Load += new System.EventHandler(this.FormPopupSearchField_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchField;
        private System.Windows.Forms.ListBox lstFields;
    }
}