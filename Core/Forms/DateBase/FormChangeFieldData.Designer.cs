namespace Core.Forms.DateBase
{
    partial class FormChangeFieldData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangeFieldData));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.gbBindSettings = new System.Windows.Forms.GroupBox();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.chkRequire = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.gbBindSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип";
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(15, 25);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(227, 21);
            this.cmbFieldType.TabIndex = 1;
            this.cmbFieldType.SelectedValueChanged += new System.EventHandler(this.cmbFieldType_SelectedValueChanged);
            // 
            // gbBindSettings
            // 
            this.gbBindSettings.Controls.Add(this.cmbField);
            this.gbBindSettings.Controls.Add(this.label3);
            this.gbBindSettings.Controls.Add(this.cmbTable);
            this.gbBindSettings.Controls.Add(this.label2);
            this.gbBindSettings.Enabled = false;
            this.gbBindSettings.Location = new System.Drawing.Point(15, 52);
            this.gbBindSettings.Name = "gbBindSettings";
            this.gbBindSettings.Size = new System.Drawing.Size(227, 115);
            this.gbBindSettings.TabIndex = 0;
            this.gbBindSettings.TabStop = false;
            this.gbBindSettings.Text = "Связано с";
            // 
            // cmbField
            // 
            this.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(13, 83);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(203, 21);
            this.cmbField.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Отображаемое поле";
            // 
            // cmbTable
            // 
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(13, 40);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(203, 21);
            this.cmbTable.TabIndex = 4;
            this.cmbTable.SelectedValueChanged += new System.EventHandler(this.cmbTable_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Таблица";
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVisible.Location = new System.Drawing.Point(311, 179);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(98, 17);
            this.chkVisible.TabIndex = 2;
            this.chkVisible.Text = "Видимое поле";
            this.chkVisible.UseVisualStyleBackColor = true;
            // 
            // chkRequire
            // 
            this.chkRequire.AutoSize = true;
            this.chkRequire.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRequire.Location = new System.Drawing.Point(283, 202);
            this.chkRequire.Name = "chkRequire";
            this.chkRequire.Size = new System.Drawing.Size(126, 17);
            this.chkRequire.TabIndex = 3;
            this.chkRequire.Text = "Обязательное поле";
            this.chkRequire.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(321, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Применить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(321, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Отображаемое название";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(15, 198);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(227, 20);
            this.txtDisplayName.TabIndex = 9;
            // 
            // FormChangeFieldData
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 231);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkRequire);
            this.Controls.Add(this.chkVisible);
            this.Controls.Add(this.gbBindSettings);
            this.Controls.Add(this.cmbFieldType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChangeFieldData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поле";
            this.Load += new System.EventHandler(this.frmChangeFieldData_Load);
            this.gbBindSettings.ResumeLayout(false);
            this.gbBindSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFieldType;
        private System.Windows.Forms.GroupBox gbBindSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.CheckBox chkRequire;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisplayName;
    }
}