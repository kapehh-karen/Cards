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
            this.btnUpColumn = new System.Windows.Forms.Button();
            this.btnDownColumn = new System.Windows.Forms.Button();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstColumns = new System.Windows.Forms.ListBox();
            this.lstSelectedColumns = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpColumn
            // 
            this.btnUpColumn.Location = new System.Drawing.Point(499, 28);
            this.btnUpColumn.Name = "btnUpColumn";
            this.btnUpColumn.Size = new System.Drawing.Size(75, 23);
            this.btnUpColumn.TabIndex = 1;
            this.btnUpColumn.Text = "Вверх";
            this.btnUpColumn.UseVisualStyleBackColor = true;
            // 
            // btnDownColumn
            // 
            this.btnDownColumn.Location = new System.Drawing.Point(499, 57);
            this.btnDownColumn.Name = "btnDownColumn";
            this.btnDownColumn.Size = new System.Drawing.Size(75, 23);
            this.btnDownColumn.TabIndex = 2;
            this.btnDownColumn.Text = "Вниз";
            this.btnDownColumn.UseVisualStyleBackColor = true;
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(175, 28);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(49, 23);
            this.btnAddColumn.TabIndex = 4;
            this.btnAddColumn.Text = ">>";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Location = new System.Drawing.Point(175, 57);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(49, 23);
            this.btnRemoveColumn.TabIndex = 4;
            this.btnRemoveColumn.Text = "<<";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Столбцы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(230, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Выбранные столбцы";
            // 
            // lstColumns
            // 
            this.lstColumns.FormattingEnabled = true;
            this.lstColumns.Location = new System.Drawing.Point(15, 28);
            this.lstColumns.Name = "lstColumns";
            this.lstColumns.Size = new System.Drawing.Size(154, 342);
            this.lstColumns.TabIndex = 7;
            // 
            // lstSelectedColumns
            // 
            this.lstSelectedColumns.FormattingEnabled = true;
            this.lstSelectedColumns.Location = new System.Drawing.Point(230, 28);
            this.lstSelectedColumns.Name = "lstSelectedColumns";
            this.lstSelectedColumns.Size = new System.Drawing.Size(263, 342);
            this.lstSelectedColumns.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(499, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 58);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TableColumnSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 384);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstSelectedColumns);
            this.Controls.Add(this.lstColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveColumn);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.btnDownColumn);
            this.Controls.Add(this.btnUpColumn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableColumnSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки столбцов";
            this.Load += new System.EventHandler(this.TableColumnSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUpColumn;
        private System.Windows.Forms.Button btnDownColumn;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnRemoveColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstColumns;
        private System.Windows.Forms.ListBox lstSelectedColumns;
        private System.Windows.Forms.Button button1;
    }
}