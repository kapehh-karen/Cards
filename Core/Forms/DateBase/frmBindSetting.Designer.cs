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
            this.gbDateTable = new System.Windows.Forms.GroupBox();
            this.checkClassif = new System.Windows.Forms.CheckBox();
            this.lvDataList = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvFields = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbIDField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnForm = new System.Windows.Forms.Button();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.cmbBasesList.SelectedValueChanged += new System.EventHandler(this.cmbBasesList_SelectedValueChanged);
            // 
            // gbDateBase
            // 
            this.gbDateBase.Controls.Add(this.gbDateTable);
            this.gbDateBase.Controls.Add(this.cmbTables);
            this.gbDateBase.Controls.Add(this.label2);
            this.gbDateBase.Enabled = false;
            this.gbDateBase.Location = new System.Drawing.Point(15, 52);
            this.gbDateBase.Name = "gbDateBase";
            this.gbDateBase.Size = new System.Drawing.Size(519, 479);
            this.gbDateBase.TabIndex = 2;
            this.gbDateBase.TabStop = false;
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
            this.gbDateTable.Enabled = false;
            this.gbDateTable.Location = new System.Drawing.Point(15, 59);
            this.gbDateTable.Name = "gbDateTable";
            this.gbDateTable.Size = new System.Drawing.Size(481, 407);
            this.gbDateTable.TabIndex = 2;
            this.gbDateTable.TabStop = false;
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
            this.checkClassif.CheckedChanged += new System.EventHandler(this.checkClassif_CheckedChanged);
            // 
            // lvDataList
            // 
            this.lvDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvDataList.FullRowSelect = true;
            this.lvDataList.GridLines = true;
            this.lvDataList.Location = new System.Drawing.Point(12, 247);
            this.lvDataList.MultiSelect = false;
            this.lvDataList.Name = "lvDataList";
            this.lvDataList.Size = new System.Drawing.Size(454, 109);
            this.lvDataList.TabIndex = 6;
            this.lvDataList.UseCompatibleStateImageBehavior = false;
            this.lvDataList.View = System.Windows.Forms.View.Details;
            this.lvDataList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvDataList_KeyUp);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(9, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(363, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Связанные списки [INS - добавить, DEL - удалить, ENTER - изменить]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Поля [ENTER - изменить]";
            // 
            // lvFields
            // 
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvFields.FullRowSelect = true;
            this.lvFields.GridLines = true;
            this.lvFields.Location = new System.Drawing.Point(12, 84);
            this.lvFields.MultiSelect = false;
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(454, 132);
            this.lvFields.TabIndex = 3;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            this.lvFields.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvFields_KeyUp);
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
            // columnHeader5
            // 
            this.columnHeader5.Text = "Обязательное";
            this.columnHeader5.Width = 47;
            // 
            // cmbIDField
            // 
            this.cmbIDField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIDField.FormattingEnabled = true;
            this.cmbIDField.Location = new System.Drawing.Point(135, 21);
            this.cmbIDField.Name = "cmbIDField";
            this.cmbIDField.Size = new System.Drawing.Size(102, 21);
            this.cmbIDField.TabIndex = 2;
            this.cmbIDField.SelectedValueChanged += new System.EventHandler(this.cmbIDField_SelectedValueChanged);
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
            // btnForm
            // 
            this.btnForm.Location = new System.Drawing.Point(391, 373);
            this.btnForm.Name = "btnForm";
            this.btnForm.Size = new System.Drawing.Size(75, 23);
            this.btnForm.TabIndex = 0;
            this.btnForm.Text = "Форма";
            this.btnForm.UseVisualStyleBackColor = true;
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(15, 32);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(170, 21);
            this.cmbTables.TabIndex = 1;
            this.cmbTables.SelectedValueChanged += new System.EventHandler(this.cmbTables_SelectedValueChanged);
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
            // btnSaveApply
            // 
            this.btnSaveApply.Location = new System.Drawing.Point(15, 537);
            this.btnSaveApply.Name = "btnSaveApply";
            this.btnSaveApply.Size = new System.Drawing.Size(163, 23);
            this.btnSaveApply.TabIndex = 3;
            this.btnSaveApply.Text = "Сохранить и применить";
            this.btnSaveApply.UseVisualStyleBackColor = true;
            this.btnSaveApply.Click += new System.EventHandler(this.btnSaveApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(184, 537);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmBindSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 567);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveApply);
            this.Controls.Add(this.gbDateBase);
            this.Controls.Add(this.cmbBasesList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBindSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "БД: Поля и связи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBindSetting_FormClosing);
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
        private System.Windows.Forms.ComboBox cmbTables;
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
        private System.Windows.Forms.Button btnSaveApply;
        private System.Windows.Forms.Button btnCancel;
    }
}