namespace Core.Forms.Main
{
    partial class FormSelectTable
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbConfigs = new System.Windows.Forms.ComboBox();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.btnSelectTable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Конфигурация базы";
            // 
            // cmbConfigs
            // 
            this.cmbConfigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConfigs.FormattingEnabled = true;
            this.cmbConfigs.Location = new System.Drawing.Point(20, 30);
            this.cmbConfigs.Name = "cmbConfigs";
            this.cmbConfigs.Size = new System.Drawing.Size(303, 21);
            this.cmbConfigs.TabIndex = 1;
            this.cmbConfigs.SelectedValueChanged += new System.EventHandler(this.cmbConfigs_SelectedValueChanged);
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.Location = new System.Drawing.Point(20, 81);
            this.lbTables.Name = "lbTables";
            this.lbTables.Size = new System.Drawing.Size(303, 199);
            this.lbTables.TabIndex = 2;
            // 
            // btnSelectTable
            // 
            this.btnSelectTable.Location = new System.Drawing.Point(209, 286);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new System.Drawing.Size(114, 23);
            this.btnSelectTable.TabIndex = 3;
            this.btnSelectTable.Text = "Соединиться";
            this.btnSelectTable.UseVisualStyleBackColor = true;
            this.btnSelectTable.Click += new System.EventHandler(this.btnSelectTable_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Таблицы";
            // 
            // FormSelectTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectTable);
            this.Controls.Add(this.lbTables);
            this.Controls.Add(this.cmbConfigs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CARDS ::.";
            this.Load += new System.EventHandler(this.FormSelectTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbConfigs;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.Button btnSelectTable;
        private System.Windows.Forms.Label label2;
    }
}