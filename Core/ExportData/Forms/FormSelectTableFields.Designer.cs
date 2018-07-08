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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonForAllTable = new System.Windows.Forms.RadioButton();
            this.radioButtonInTable = new System.Windows.Forms.RadioButton();
            this.radioButtonSelected = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
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
            this.treeViewFields.Size = new System.Drawing.Size(658, 304);
            this.treeViewFields.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonForAllTable);
            this.groupBox1.Controls.Add(this.radioButtonInTable);
            this.groupBox1.Controls.Add(this.radioButtonSelected);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Для каких записей";
            // 
            // radioButtonForAllTable
            // 
            this.radioButtonForAllTable.AutoSize = true;
            this.radioButtonForAllTable.Checked = true;
            this.radioButtonForAllTable.ForeColor = System.Drawing.Color.DarkRed;
            this.radioButtonForAllTable.Location = new System.Drawing.Point(17, 70);
            this.radioButtonForAllTable.Name = "radioButtonForAllTable";
            this.radioButtonForAllTable.Size = new System.Drawing.Size(205, 17);
            this.radioButtonForAllTable.TabIndex = 0;
            this.radioButtonForAllTable.TabStop = true;
            this.radioButtonForAllTable.Text = "Для всех записей из таблицы";
            this.radioButtonForAllTable.UseVisualStyleBackColor = true;
            // 
            // radioButtonInTable
            // 
            this.radioButtonInTable.AutoSize = true;
            this.radioButtonInTable.ForeColor = System.Drawing.Color.DarkRed;
            this.radioButtonInTable.Location = new System.Drawing.Point(17, 47);
            this.radioButtonInTable.Name = "radioButtonInTable";
            this.radioButtonInTable.Size = new System.Drawing.Size(218, 17);
            this.radioButtonInTable.TabIndex = 0;
            this.radioButtonInTable.Text = "Для текущих записей в таблице";
            this.radioButtonInTable.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelected
            // 
            this.radioButtonSelected.AutoSize = true;
            this.radioButtonSelected.ForeColor = System.Drawing.Color.DarkRed;
            this.radioButtonSelected.Location = new System.Drawing.Point(17, 24);
            this.radioButtonSelected.Name = "radioButtonSelected";
            this.radioButtonSelected.Size = new System.Drawing.Size(119, 17);
            this.radioButtonSelected.TabIndex = 0;
            this.radioButtonSelected.Text = "Для выбранных";
            this.radioButtonSelected.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrint.Image = global::Core.Properties.Resources.excel;
            this.btnPrint.Location = new System.Drawing.Point(536, 427);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(134, 43);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Экспорт";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FormSelectTableFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 480);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPrint);
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

        }

        #endregion

        private MixedCheckBoxesTreeView treeViewFields;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonForAllTable;
        private System.Windows.Forms.RadioButton radioButtonInTable;
        private System.Windows.Forms.RadioButton radioButtonSelected;
    }
}