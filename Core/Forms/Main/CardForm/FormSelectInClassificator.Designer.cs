namespace Core.Forms.Main.CardForm
{
    partial class FormSelectInClassificator
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
            this.lblSelectedCell = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAmount = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableDataGridView1
            // 
            this.tableDataGridView1.AllowCache = true;
            this.tableDataGridView1.AllowUserToAddRows = false;
            this.tableDataGridView1.AllowUserToDeleteRows = false;
            this.tableDataGridView1.AllowUserToOrderColumns = true;
            this.tableDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.tableDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGridView1.Base = null;
            this.tableDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView1.CurrentDataTable = null;
            this.tableDataGridView1.KeepSelectedColumn = null;
            this.tableDataGridView1.Location = new System.Drawing.Point(5, 38);
            this.tableDataGridView1.MultiSelect = false;
            this.tableDataGridView1.Name = "tableDataGridView1";
            this.tableDataGridView1.ReadOnly = true;
            this.tableDataGridView1.SelectedID = null;
            this.tableDataGridView1.Size = new System.Drawing.Size(630, 485);
            this.tableDataGridView1.TabIndex = 0;
            this.tableDataGridView1.Table = null;
            this.tableDataGridView1.PressedEnter += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedEnter);
            this.tableDataGridView1.CurrentCellChanged += new System.EventHandler(this.tableDataGridView1_CurrentCellChanged);
            this.tableDataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tableDataGridView1_KeyPress);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(548, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(81, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblSelectedCell
            // 
            this.lblSelectedCell.AutoSize = true;
            this.lblSelectedCell.Location = new System.Drawing.Point(289, 14);
            this.lblSelectedCell.Name = "lblSelectedCell";
            this.lblSelectedCell.Size = new System.Drawing.Size(10, 13);
            this.lblSelectedCell.TabIndex = 2;
            this.lblSelectedCell.Text = "-";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Поиск";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelAmount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(641, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelAmount
            // 
            this.toolStripStatusLabelAmount.Name = "toolStripStatusLabelAmount";
            this.toolStripStatusLabelAmount.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabelAmount.Text = "Всего записей: -";
            // 
            // FormSelectInClassificator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 551);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblSelectedCell);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tableDataGridView1);
            this.KeyPreview = true;
            this.Name = "FormSelectInClassificator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор из классификатора";
            this.Load += new System.EventHandler(this.FormSelectInClassificator_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormSelectInClassificator_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableDataGridView tableDataGridView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSelectedCell;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAmount;
    }
}