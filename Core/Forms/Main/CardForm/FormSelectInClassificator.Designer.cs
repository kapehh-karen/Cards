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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectInClassificator));
            this.tableDataGridView1 = new Core.Forms.Main.TableDataGridView();
            this.lblSelectedCell = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.useNullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableDataGridView1
            // 
            this.tableDataGridView1.AllowCache = true;
            this.tableDataGridView1.AllowUserToAddRows = false;
            this.tableDataGridView1.AllowUserToDeleteRows = false;
            this.tableDataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tableDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tableDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGridView1.Base = null;
            this.tableDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView1.CurrentDataTable = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.tableDataGridView1.DesignControls = null;
            this.tableDataGridView1.Filter = null;
            this.tableDataGridView1.InDesigner = false;
            this.tableDataGridView1.KeepSelectedColumn = null;
            this.tableDataGridView1.Location = new System.Drawing.Point(5, 56);
            this.tableDataGridView1.MultiSelect = false;
            this.tableDataGridView1.Name = "tableDataGridView1";
            this.tableDataGridView1.ParentControl = null;
            this.tableDataGridView1.ParentField = null;
            this.tableDataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.tableDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.tableDataGridView1.SelectedID = null;
            this.tableDataGridView1.Size = new System.Drawing.Size(630, 467);
            this.tableDataGridView1.StandardTab = true;
            this.tableDataGridView1.TabIndex = 0;
            this.tableDataGridView1.Table = null;
            this.tableDataGridView1.TableStorageInformation = null;
            this.tableDataGridView1.TableStorageType = Core.Storage.Tables.TableStorageType.Classificator;
            this.tableDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.tableDataGridView1.PressedEnter += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedEnter);
            this.tableDataGridView1.PressedClick += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedEnter);
            this.tableDataGridView1.CurrentCellChanged += new System.EventHandler(this.tableDataGridView1_CurrentCellChanged);
            this.tableDataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tableDataGridView1_KeyPress);
            // 
            // lblSelectedCell
            // 
            this.lblSelectedCell.AutoSize = true;
            this.lblSelectedCell.Location = new System.Drawing.Point(286, 32);
            this.lblSelectedCell.Name = "lblSelectedCell";
            this.lblSelectedCell.Size = new System.Drawing.Size(10, 13);
            this.lblSelectedCell.TabIndex = 2;
            this.lblSelectedCell.Text = "-";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useNullToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(641, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // useNullToolStripMenuItem
            // 
            this.useNullToolStripMenuItem.Image = global::Core.Properties.Resources.empty_set;
            this.useNullToolStripMenuItem.Name = "useNullToolStripMenuItem";
            this.useNullToolStripMenuItem.Size = new System.Drawing.Size(206, 20);
            this.useNullToolStripMenuItem.Text = "Использовать пустое значение";
            this.useNullToolStripMenuItem.Click += new System.EventHandler(this.useNullToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::Core.Properties.Resources.refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.refreshToolStripMenuItem.Text = "Обновить";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // FormSelectInClassificator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 551);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblSelectedCell);
            this.Controls.Add(this.tableDataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormSelectInClassificator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор из классификатора";
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableDataGridView tableDataGridView1;
        private System.Windows.Forms.Label lblSelectedCell;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAmount;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem useNullToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}