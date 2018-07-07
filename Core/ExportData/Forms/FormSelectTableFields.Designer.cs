using Core.Common.TreeViewEx;

namespace Core.ExportData.Forms
{
    partial class FormSelectTableFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectTableFields));
            this.treeViewFields = new Core.Common.TreeViewEx.MixedCheckBoxesTreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonForAllTable = new System.Windows.Forms.RadioButton();
            this.radioButtonInTable = new System.Windows.Forms.RadioButton();
            this.radioButtonSelected = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewFields
            // 
            this.treeViewFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewFields.CheckBoxes = true;
            this.treeViewFields.Location = new System.Drawing.Point(12, 115);
            this.treeViewFields.Name = "treeViewFields";
            this.treeViewFields.Size = new System.Drawing.Size(653, 314);
            this.treeViewFields.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 435);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(575, 123);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(593, 535);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonForAllTable);
            this.groupBox1.Controls.Add(this.radioButtonInTable);
            this.groupBox1.Controls.Add(this.radioButtonSelected);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Для каких записей";
            // 
            // radioButtonForAllTable
            // 
            this.radioButtonForAllTable.AutoSize = true;
            this.radioButtonForAllTable.Checked = true;
            this.radioButtonForAllTable.Location = new System.Drawing.Point(17, 70);
            this.radioButtonForAllTable.Name = "radioButtonForAllTable";
            this.radioButtonForAllTable.Size = new System.Drawing.Size(178, 17);
            this.radioButtonForAllTable.TabIndex = 0;
            this.radioButtonForAllTable.TabStop = true;
            this.radioButtonForAllTable.Text = "Для всех записей из таблицы";
            this.radioButtonForAllTable.UseVisualStyleBackColor = true;
            // 
            // radioButtonInTable
            // 
            this.radioButtonInTable.AutoSize = true;
            this.radioButtonInTable.Location = new System.Drawing.Point(17, 47);
            this.radioButtonInTable.Name = "radioButtonInTable";
            this.radioButtonInTable.Size = new System.Drawing.Size(189, 17);
            this.radioButtonInTable.TabIndex = 0;
            this.radioButtonInTable.Text = "Для текущих записей в таблице";
            this.radioButtonInTable.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelected
            // 
            this.radioButtonSelected.AutoSize = true;
            this.radioButtonSelected.Location = new System.Drawing.Point(17, 24);
            this.radioButtonSelected.Name = "radioButtonSelected";
            this.radioButtonSelected.Size = new System.Drawing.Size(106, 17);
            this.radioButtonSelected.TabIndex = 0;
            this.radioButtonSelected.Text = "Для выбранных";
            this.radioButtonSelected.UseVisualStyleBackColor = true;
            // 
            // FormSelectTableFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 570);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.treeViewFields);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormSelectTableFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор полей для экспорта";
            this.Load += new System.EventHandler(this.FormSelectTableFields_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MixedCheckBoxesTreeView treeViewFields;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonForAllTable;
        private System.Windows.Forms.RadioButton radioButtonInTable;
        private System.Windows.Forms.RadioButton radioButtonSelected;
    }
}