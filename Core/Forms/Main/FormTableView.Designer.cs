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
            this.btnChange = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableDataGridView1
            // 
            this.tableDataGridView1.AllowCache = false;
            this.tableDataGridView1.AllowUserToAddRows = false;
            this.tableDataGridView1.AllowUserToDeleteRows = false;
            this.tableDataGridView1.AllowUserToOrderColumns = true;
            this.tableDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGridView1.Base = null;
            this.tableDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView1.CurrentDataTable = null;
            this.tableDataGridView1.Location = new System.Drawing.Point(12, 56);
            this.tableDataGridView1.MultiSelect = false;
            this.tableDataGridView1.Name = "tableDataGridView1";
            this.tableDataGridView1.ReadOnly = true;
            this.tableDataGridView1.SelectedID = null;
            this.tableDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tableDataGridView1.Size = new System.Drawing.Size(751, 465);
            this.tableDataGridView1.TabIndex = 0;
            this.tableDataGridView1.Table = null;
            this.tableDataGridView1.PressedKey += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedKey);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenForm
            // 
            this.btnOpenForm.Location = new System.Drawing.Point(93, 10);
            this.btnOpenForm.Name = "btnOpenForm";
            this.btnOpenForm.Size = new System.Drawing.Size(75, 40);
            this.btnOpenForm.TabIndex = 2;
            this.btnOpenForm.Text = "Создать";
            this.btnOpenForm.UseVisualStyleBackColor = true;
            this.btnOpenForm.Click += new System.EventHandler(this.btnOpenForm_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(174, 10);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(76, 40);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(256, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 40);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FormTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 533);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnChange);
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
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDelete;
    }
}