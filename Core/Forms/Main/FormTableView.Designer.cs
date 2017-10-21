namespace Core.Forms.Main
{
    partial class FormTableView
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
            this.tableDataGridView1 = new Core.Forms.Main.TableDataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableDataGridView1
            // 
            this.tableDataGridView1.AllowUserToAddRows = false;
            this.tableDataGridView1.AllowUserToDeleteRows = false;
            this.tableDataGridView1.AllowUserToOrderColumns = true;
            this.tableDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGridView1.Base = null;
            this.tableDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView1.CurrentDataTable = null;
            this.tableDataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableDataGridView1.Location = new System.Drawing.Point(0, 39);
            this.tableDataGridView1.Name = "tableDataGridView1";
            this.tableDataGridView1.ReadOnly = true;
            this.tableDataGridView1.Size = new System.Drawing.Size(752, 489);
            this.tableDataGridView1.TabIndex = 0;
            this.tableDataGridView1.Table = null;
            this.tableDataGridView1.SelectionChanged += new System.EventHandler(this.tableDataGridView1_SelectionChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenForm
            // 
            this.btnOpenForm.Location = new System.Drawing.Point(93, 10);
            this.btnOpenForm.Name = "btnOpenForm";
            this.btnOpenForm.Size = new System.Drawing.Size(75, 23);
            this.btnOpenForm.TabIndex = 2;
            this.btnOpenForm.Text = "test";
            this.btnOpenForm.UseVisualStyleBackColor = true;
            this.btnOpenForm.Click += new System.EventHandler(this.btnOpenForm_Click);
            // 
            // FormTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 528);
            this.Controls.Add(this.btnOpenForm);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tableDataGridView1);
            this.Name = "FormTableView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Таблица";
            this.Load += new System.EventHandler(this.FormTableView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableDataGridView tableDataGridView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOpenForm;
    }
}