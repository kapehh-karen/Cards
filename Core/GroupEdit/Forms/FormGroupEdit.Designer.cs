namespace Core.GroupEdit.Forms
{
    partial class FormGroupEdit
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
            this.lstFields = new System.Windows.Forms.ListBox();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstFields
            // 
            this.lstFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(12, 12);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(273, 381);
            this.lstFields.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainer.AutoScroll = true;
            this.panelContainer.Location = new System.Drawing.Point(334, 12);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(466, 348);
            this.panelContainer.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(291, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(37, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Image = global::Core.Properties.Resources.checkmark;
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRun.Location = new System.Drawing.Point(677, 366);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(123, 40);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Изменить";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // FormGroupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 415);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.lstFields);
            this.Name = "FormGroupEdit";
            this.Text = "Групповая корректировка";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRun;
    }
}