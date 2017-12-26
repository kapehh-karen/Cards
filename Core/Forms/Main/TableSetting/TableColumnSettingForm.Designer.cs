namespace Core.Forms.Main.TableSetting
{
    partial class TableColumnSettingForm
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
            this.lvSelectedColumns = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnUpColumn = new System.Windows.Forms.Button();
            this.btnDownColumn = new System.Windows.Forms.Button();
            this.lvColumns = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvSelectedColumns
            // 
            this.lvSelectedColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvSelectedColumns.Location = new System.Drawing.Point(230, 28);
            this.lvSelectedColumns.Name = "lvSelectedColumns";
            this.lvSelectedColumns.Size = new System.Drawing.Size(263, 338);
            this.lvSelectedColumns.TabIndex = 0;
            this.lvSelectedColumns.UseCompatibleStateImageBehavior = false;
            this.lvSelectedColumns.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Столбец";
            this.columnHeader1.Width = 118;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ширина";
            this.columnHeader2.Width = 132;
            // 
            // btnUpColumn
            // 
            this.btnUpColumn.Location = new System.Drawing.Point(499, 72);
            this.btnUpColumn.Name = "btnUpColumn";
            this.btnUpColumn.Size = new System.Drawing.Size(75, 23);
            this.btnUpColumn.TabIndex = 1;
            this.btnUpColumn.Text = "Вверх";
            this.btnUpColumn.UseVisualStyleBackColor = true;
            // 
            // btnDownColumn
            // 
            this.btnDownColumn.Location = new System.Drawing.Point(499, 101);
            this.btnDownColumn.Name = "btnDownColumn";
            this.btnDownColumn.Size = new System.Drawing.Size(75, 23);
            this.btnDownColumn.TabIndex = 2;
            this.btnDownColumn.Text = "Вниз";
            this.btnDownColumn.UseVisualStyleBackColor = true;
            // 
            // lvColumns
            // 
            this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvColumns.Location = new System.Drawing.Point(12, 28);
            this.lvColumns.Name = "lvColumns";
            this.lvColumns.Size = new System.Drawing.Size(157, 338);
            this.lvColumns.TabIndex = 3;
            this.lvColumns.UseCompatibleStateImageBehavior = false;
            this.lvColumns.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Столбец";
            this.columnHeader3.Width = 140;
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(175, 72);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(49, 23);
            this.btnAddColumn.TabIndex = 4;
            this.btnAddColumn.Text = ">>";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Location = new System.Drawing.Point(175, 101);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(49, 23);
            this.btnRemoveColumn.TabIndex = 4;
            this.btnRemoveColumn.Text = "<<";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Столбцы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Выбранные столбцы";
            // 
            // TableColumnSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 378);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveColumn);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.lvColumns);
            this.Controls.Add(this.btnDownColumn);
            this.Controls.Add(this.btnUpColumn);
            this.Controls.Add(this.lvSelectedColumns);
            this.Name = "TableColumnSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки столбцов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvSelectedColumns;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnUpColumn;
        private System.Windows.Forms.Button btnDownColumn;
        private System.Windows.Forms.ListView lvColumns;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnRemoveColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}