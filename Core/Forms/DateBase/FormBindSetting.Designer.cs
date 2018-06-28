using Core.Common;

namespace Core.Forms.DateBase
{
    partial class FormBindSetting
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBindSetting));
            this.gbDateBase = new System.Windows.Forms.GroupBox();
            this.gbDateTable = new System.Windows.Forms.GroupBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.lvFields = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvDataList = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbFastJumpField = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFormRemove = new System.Windows.Forms.Button();
            this.checkVisible = new System.Windows.Forms.CheckBox();
            this.txtTableDisplayName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkClassif = new System.Windows.Forms.CheckBox();
            this.cmbIDField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnForm = new Core.Common.MenuButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editCurrentFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFormFromTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditDB = new System.Windows.Forms.Button();
            this.btnCopyFromTable = new System.Windows.Forms.Button();
            this.gbDateBase.SuspendLayout();
            this.gbDateTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDateBase
            // 
            this.gbDateBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDateBase.Controls.Add(this.gbDateTable);
            this.gbDateBase.Controls.Add(this.cmbTables);
            this.gbDateBase.Controls.Add(this.label2);
            this.gbDateBase.Enabled = false;
            this.gbDateBase.Location = new System.Drawing.Point(12, 12);
            this.gbDateBase.Name = "gbDateBase";
            this.gbDateBase.Size = new System.Drawing.Size(758, 530);
            this.gbDateBase.TabIndex = 2;
            this.gbDateBase.TabStop = false;
            // 
            // gbDateTable
            // 
            this.gbDateTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDateTable.Controls.Add(this.splitContainerMain);
            this.gbDateTable.Controls.Add(this.cmbFastJumpField);
            this.gbDateTable.Controls.Add(this.label1);
            this.gbDateTable.Controls.Add(this.btnFormRemove);
            this.gbDateTable.Controls.Add(this.checkVisible);
            this.gbDateTable.Controls.Add(this.txtTableDisplayName);
            this.gbDateTable.Controls.Add(this.label6);
            this.gbDateTable.Controls.Add(this.checkClassif);
            this.gbDateTable.Controls.Add(this.cmbIDField);
            this.gbDateTable.Controls.Add(this.label3);
            this.gbDateTable.Controls.Add(this.btnForm);
            this.gbDateTable.Enabled = false;
            this.gbDateTable.Location = new System.Drawing.Point(15, 59);
            this.gbDateTable.Name = "gbDateTable";
            this.gbDateTable.Size = new System.Drawing.Size(728, 455);
            this.gbDateTable.TabIndex = 2;
            this.gbDateTable.TabStop = false;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerMain.Location = new System.Drawing.Point(12, 58);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.btnCopyFromTable);
            this.splitContainerMain.Panel1.Controls.Add(this.lvFields);
            this.splitContainerMain.Panel1.Controls.Add(this.label4);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.label5);
            this.splitContainerMain.Panel2.Controls.Add(this.lvDataList);
            this.splitContainerMain.Size = new System.Drawing.Size(701, 307);
            this.splitContainerMain.SplitterDistance = 175;
            this.splitContainerMain.SplitterWidth = 7;
            this.splitContainerMain.TabIndex = 15;
            // 
            // lvFields
            // 
            this.lvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader8});
            this.lvFields.FullRowSelect = true;
            this.lvFields.GridLines = true;
            this.lvFields.Location = new System.Drawing.Point(3, 27);
            this.lvFields.MultiSelect = false;
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(691, 141);
            this.lvFields.TabIndex = 3;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            this.lvFields.ItemActivate += new System.EventHandler(this.lvFields_ItemActivate);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Имя поля";
            this.columnHeader2.Width = 194;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Тип поля";
            this.columnHeader3.Width = 190;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Видимость";
            this.columnHeader4.Width = 63;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Обязательное";
            this.columnHeader5.Width = 91;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Отображаемое имя поля";
            this.columnHeader8.Width = 204;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Поля [ENTER - изменить]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(353, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Внешние данные [INS - добавить, DEL - удалить, ENTER - изменить]";
            // 
            // lvDataList
            // 
            this.lvDataList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader9});
            this.lvDataList.FullRowSelect = true;
            this.lvDataList.GridLines = true;
            this.lvDataList.Location = new System.Drawing.Point(6, 20);
            this.lvDataList.MultiSelect = false;
            this.lvDataList.Name = "lvDataList";
            this.lvDataList.Size = new System.Drawing.Size(688, 98);
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
            this.columnHeader7.Text = "Внешнее поле (Foreign Key)";
            this.columnHeader7.Width = 153;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Обязательное";
            this.columnHeader9.Width = 115;
            // 
            // cmbFastJumpField
            // 
            this.cmbFastJumpField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFastJumpField.FormattingEnabled = true;
            this.cmbFastJumpField.Location = new System.Drawing.Point(423, 21);
            this.cmbFastJumpField.Name = "cmbFastJumpField";
            this.cmbFastJumpField.Size = new System.Drawing.Size(121, 21);
            this.cmbFastJumpField.TabIndex = 14;
            this.cmbFastJumpField.SelectedValueChanged += new System.EventHandler(this.cmbFastJumpField_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Поле быстрого перехода:";
            // 
            // btnFormRemove
            // 
            this.btnFormRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFormRemove.Image = global::Core.Properties.Resources.delete_s;
            this.btnFormRemove.Location = new System.Drawing.Point(688, 19);
            this.btnFormRemove.Name = "btnFormRemove";
            this.btnFormRemove.Size = new System.Drawing.Size(25, 23);
            this.btnFormRemove.TabIndex = 12;
            this.btnFormRemove.UseVisualStyleBackColor = true;
            this.btnFormRemove.Click += new System.EventHandler(this.btnFormRemove_Click);
            // 
            // checkVisible
            // 
            this.checkVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkVisible.AutoSize = true;
            this.checkVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkVisible.Location = new System.Drawing.Point(631, 420);
            this.checkVisible.Name = "checkVisible";
            this.checkVisible.Size = new System.Drawing.Size(82, 17);
            this.checkVisible.TabIndex = 11;
            this.checkVisible.Text = "Видимость";
            this.checkVisible.UseVisualStyleBackColor = true;
            this.checkVisible.CheckedChanged += new System.EventHandler(this.checkVisible_CheckedChanged);
            // 
            // txtTableDisplayName
            // 
            this.txtTableDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTableDisplayName.Location = new System.Drawing.Point(196, 371);
            this.txtTableDisplayName.Name = "txtTableDisplayName";
            this.txtTableDisplayName.Size = new System.Drawing.Size(517, 20);
            this.txtTableDisplayName.TabIndex = 10;
            this.txtTableDisplayName.TextChanged += new System.EventHandler(this.txtTableDisplayName_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Отображаемое название таблицы:";
            // 
            // checkClassif
            // 
            this.checkClassif.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkClassif.AutoSize = true;
            this.checkClassif.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkClassif.Location = new System.Drawing.Point(562, 397);
            this.checkClassif.Name = "checkClassif";
            this.checkClassif.Size = new System.Drawing.Size(151, 17);
            this.checkClassif.TabIndex = 8;
            this.checkClassif.Text = "Таблица классификатор";
            this.checkClassif.UseVisualStyleBackColor = true;
            this.checkClassif.CheckedChanged += new System.EventHandler(this.checkClassif_CheckedChanged);
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
            this.label3.Size = new System.Drawing.Size(123, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "Поле идентификатора:\r\n(Primary Key)";
            // 
            // btnForm
            // 
            this.btnForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForm.Location = new System.Drawing.Point(590, 19);
            this.btnForm.Menu = this.contextMenuStrip1;
            this.btnForm.Name = "btnForm";
            this.btnForm.Size = new System.Drawing.Size(98, 23);
            this.btnForm.TabIndex = 0;
            this.btnForm.Text = "Форма";
            this.btnForm.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCurrentFormToolStripMenuItem,
            this.copyFormFromTableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(295, 48);
            // 
            // editCurrentFormToolStripMenuItem
            // 
            this.editCurrentFormToolStripMenuItem.Name = "editCurrentFormToolStripMenuItem";
            this.editCurrentFormToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.editCurrentFormToolStripMenuItem.Text = "Редактировать текущую форму";
            this.editCurrentFormToolStripMenuItem.Click += new System.EventHandler(this.editCurrentFormToolStripMenuItem_Click);
            // 
            // copyFormFromTableToolStripMenuItem
            // 
            this.copyFormFromTableToolStripMenuItem.Name = "copyFormFromTableToolStripMenuItem";
            this.copyFormFromTableToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.copyFormFromTableToolStripMenuItem.Text = "Скопировать форму из другой таблицы";
            this.copyFormFromTableToolStripMenuItem.Click += new System.EventHandler(this.copyFormFromTableToolStripMenuItem_Click);
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
            this.btnSaveApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveApply.Location = new System.Drawing.Point(607, 548);
            this.btnSaveApply.Name = "btnSaveApply";
            this.btnSaveApply.Size = new System.Drawing.Size(163, 23);
            this.btnSaveApply.TabIndex = 3;
            this.btnSaveApply.Text = "Сохранить";
            this.btnSaveApply.UseVisualStyleBackColor = true;
            this.btnSaveApply.Click += new System.EventHandler(this.btnSaveApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(526, 548);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditDB
            // 
            this.btnEditDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditDB.Location = new System.Drawing.Point(12, 550);
            this.btnEditDB.Name = "btnEditDB";
            this.btnEditDB.Size = new System.Drawing.Size(189, 21);
            this.btnEditDB.TabIndex = 5;
            this.btnEditDB.Text = "Изменить настройки соединения";
            this.btnEditDB.UseVisualStyleBackColor = true;
            this.btnEditDB.Click += new System.EventHandler(this.btnEditDB_Click);
            // 
            // btnCopyFromTable
            // 
            this.btnCopyFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFromTable.Location = new System.Drawing.Point(485, 1);
            this.btnCopyFromTable.Name = "btnCopyFromTable";
            this.btnCopyFromTable.Size = new System.Drawing.Size(210, 23);
            this.btnCopyFromTable.TabIndex = 5;
            this.btnCopyFromTable.Text = "Автозаполнение из таблицы";
            this.btnCopyFromTable.UseVisualStyleBackColor = true;
            this.btnCopyFromTable.Click += new System.EventHandler(this.btnCopyFromTable_Click);
            // 
            // FormBindSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 578);
            this.Controls.Add(this.btnEditDB);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveApply);
            this.Controls.Add(this.gbDateBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(797, 617);
            this.Name = "FormBindSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки БД";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBindSetting_FormClosing);
            this.Load += new System.EventHandler(this.frmBindSetting_Load);
            this.gbDateBase.ResumeLayout(false);
            this.gbDateBase.PerformLayout();
            this.gbDateTable.ResumeLayout(false);
            this.gbDateTable.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbDateBase;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbDateTable;
        private MenuButton btnForm;
        private System.Windows.Forms.ComboBox cmbIDField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvFields;
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
        private System.Windows.Forms.Button btnEditDB;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TextBox txtTableDisplayName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkVisible;
        private System.Windows.Forms.Button btnFormRemove;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ComboBox cmbFastJumpField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editCurrentFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFormFromTableToolStripMenuItem;
        private System.Windows.Forms.Button btnCopyFromTable;
    }
}