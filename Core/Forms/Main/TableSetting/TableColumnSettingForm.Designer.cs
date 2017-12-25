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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnUpColumn = new System.Windows.Forms.Button();
            this.btnDownColumn = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(230, 26);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(263, 338);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
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
            this.btnUpColumn.Location = new System.Drawing.Point(499, 26);
            this.btnUpColumn.Name = "btnUpColumn";
            this.btnUpColumn.Size = new System.Drawing.Size(75, 23);
            this.btnUpColumn.TabIndex = 1;
            this.btnUpColumn.Text = "Вверх";
            this.btnUpColumn.UseVisualStyleBackColor = true;
            // 
            // btnDownColumn
            // 
            this.btnDownColumn.Location = new System.Drawing.Point(499, 55);
            this.btnDownColumn.Name = "btnDownColumn";
            this.btnDownColumn.Size = new System.Drawing.Size(75, 23);
            this.btnDownColumn.TabIndex = 2;
            this.btnDownColumn.Text = "Вниз";
            this.btnDownColumn.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listView2.Location = new System.Drawing.Point(12, 26);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(157, 338);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Столбец";
            this.columnHeader3.Width = 140;
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(175, 64);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(49, 23);
            this.btnAddColumn.TabIndex = 4;
            this.btnAddColumn.Text = ">>";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Location = new System.Drawing.Point(175, 93);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(49, 23);
            this.btnRemoveColumn.TabIndex = 4;
            this.btnRemoveColumn.Text = "<<";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            // 
            // TableColumnSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 375);
            this.Controls.Add(this.btnRemoveColumn);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.btnDownColumn);
            this.Controls.Add(this.btnUpColumn);
            this.Controls.Add(this.listView1);
            this.Name = "TableColumnSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки столбцов";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnUpColumn;
        private System.Windows.Forms.Button btnDownColumn;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnRemoveColumn;
    }
}