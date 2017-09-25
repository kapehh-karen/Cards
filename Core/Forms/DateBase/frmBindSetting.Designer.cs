namespace Core.Forms.DateBase
{
    partial class frmBindSetting
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
            this.cmbBasesList = new System.Windows.Forms.ComboBox();
            this.gbDateBase = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gbDateTable = new System.Windows.Forms.GroupBox();
            this.btnForm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIDField = new System.Windows.Forms.ComboBox();
            this.lvFields = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvDataList = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkClassif = new System.Windows.Forms.CheckBox();
            this.gbDateBase.SuspendLayout();
            this.gbDateTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "База";
            // 
            // cmbBasesList
            // 
            this.cmbBasesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasesList.FormattingEnabled = true;
            this.cmbBasesList.Location = new System.Drawing.Point(15, 25);
            this.cmbBasesList.Name = "cmbBasesList";
            this.cmbBasesList.Size = new System.Drawing.Size(185, 21);
            this.cmbBasesList.TabIndex = 1;
            // 
            // gbDateBase
            // 
            this.gbDateBase.Controls.Add(this.gbDateTable);
            this.gbDateBase.Controls.Add(this.comboBox1);
            this.gbDateBase.Controls.Add(this.label2);
            this.gbDateBase.Location = new System.Drawing.Point(15, 52);
            this.gbDateBase.Name = "gbDateBase";
            this.gbDateBase.Size = new System.Drawing.Size(519, 495);
            this.gbDateBase.TabIndex = 2;
            this.gbDateBase.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Таблица";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // gbDateTable
            // 
            this.gbDateTable.Controls.Add(this.checkClassif);
            this.gbDateTable.Controls.Add(this.lvDataList);
            this.gbDateTable.Controls.Add(this.label5);
            this.gbDateTable.Controls.Add(this.label4);
            this.gbDateTable.Controls.Add(this.lvFields);
            this.gbDateTable.Controls.Add(this.cmbIDField);
            this.gbDateTable.Controls.Add(this.label3);
            this.gbDateTable.Controls.Add(this.btnForm);
            this.gbDateTable.Location = new System.Drawing.Point(15, 59);
            this.gbDateTable.Name = "gbDateTable";
            this.gbDateTable.Size = new System.Drawing.Size(481, 417);
            this.gbDateTable.TabIndex = 2;
            this.gbDateTable.TabStop = false;
            // 
            // btnForm
            // 
            this.btnForm.Location = new System.Drawing.Point(391, 373);
            this.btnForm.Name = "btnForm";
            this.btnForm.Size = new System.Drawing.Size(75, 23);
            this.btnForm.TabIndex = 0;
            this.btnForm.Text = "Форма";
            this.btnForm.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(9, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "Поле идентификатора\r\n(Primary Key)";
            // 
            // cmbIDField
            // 
            this.cmbIDField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIDField.FormattingEnabled = true;
            this.cmbIDField.Location = new System.Drawing.Point(135, 21);
            this.cmbIDField.Name = "cmbIDField";
            this.cmbIDField.Size = new System.Drawing.Size(102, 21);
            this.cmbIDField.TabIndex = 2;
            // 
            // lvFields
            // 
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvFields.Location = new System.Drawing.Point(12, 84);
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(454, 132);
            this.lvFields.TabIndex = 3;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PK";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Имя поля";
            this.columnHeader2.Width = 130;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Тип поля";
            this.columnHeader3.Width = 181;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Видимость";
            this.columnHeader4.Width = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Поля (INS - добавить, DEL - удалить, ENTER - изменить)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(9, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(363, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Связанные списки (INS - добавить, DEL - удалить, ENTER - изменить)";
            // 
            // lvDataList
            // 
            this.lvDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvDataList.Location = new System.Drawing.Point(12, 247);
            this.lvDataList.Name = "lvDataList";
            this.lvDataList.Size = new System.Drawing.Size(454, 109);
            this.lvDataList.TabIndex = 6;
            this.lvDataList.UseCompatibleStateImageBehavior = false;
            this.lvDataList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Имя таблицы";
            this.columnHeader6.Width = 161;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "По полю (Foreign Key)";
            this.columnHeader7.Width = 212;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Обязательное";
            this.columnHeader5.Width = 47;
            // 
            // checkClassif
            // 
            this.checkClassif.AutoSize = true;
            this.checkClassif.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkClassif.Location = new System.Drawing.Point(315, 23);
            this.checkClassif.Name = "checkClassif";
            this.checkClassif.Size = new System.Drawing.Size(151, 17);
            this.checkClassif.TabIndex = 8;
            this.checkClassif.Text = "Таблица классификатор";
            this.checkClassif.UseVisualStyleBackColor = true;
            // 
            // frmBindSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 559);
            this.Controls.Add(this.gbDateBase);
            this.Controls.Add(this.cmbBasesList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBindSetting";
            this.Text = "БД: Поля и связи";
            this.Load += new System.EventHandler(this.frmBindSetting_Load);
            this.gbDateBase.ResumeLayout(false);
            this.gbDateBase.PerformLayout();
            this.gbDateTable.ResumeLayout(false);
            this.gbDateTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBasesList;
        private System.Windows.Forms.GroupBox gbDateBase;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbDateTable;
        private System.Windows.Forms.Button btnForm;
        private System.Windows.Forms.ComboBox cmbIDField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvFields;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvDataList;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox checkClassif;
    }
}