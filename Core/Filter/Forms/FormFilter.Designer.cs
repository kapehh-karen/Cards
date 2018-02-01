namespace Core.Filter.Forms
{
    partial class FormFilter
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
            this.treeSubFilter = new System.Windows.Forms.TreeView();
            this.btnApply = new System.Windows.Forms.Button();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLinkedTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputOperand1 = new Core.Filter.Controls.InputOperand();
            this.contextMenuTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeSubFilter
            // 
            this.treeSubFilter.Location = new System.Drawing.Point(12, 12);
            this.treeSubFilter.Name = "treeSubFilter";
            this.treeSubFilter.Size = new System.Drawing.Size(307, 414);
            this.treeSubFilter.TabIndex = 0;
            this.treeSubFilter.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeSubFilter_NodeMouseClick);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(776, 403);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "button1";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLinkedTableToolStripMenuItem,
            this.removeNodeToolStripMenuItem});
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.Size = new System.Drawing.Size(298, 48);
            // 
            // addLinkedTableToolStripMenuItem
            // 
            this.addLinkedTableToolStripMenuItem.Name = "addLinkedTableToolStripMenuItem";
            this.addLinkedTableToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.addLinkedTableToolStripMenuItem.Text = "Добавить выборку по внешним данным";
            // 
            // removeNodeToolStripMenuItem
            // 
            this.removeNodeToolStripMenuItem.Name = "removeNodeToolStripMenuItem";
            this.removeNodeToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.removeNodeToolStripMenuItem.Text = "Удалить выбранный элемент";
            this.removeNodeToolStripMenuItem.Click += new System.EventHandler(this.removeNodeToolStripMenuItem_Click);
            // 
            // inputOperand1
            // 
            this.inputOperand1.FilterData = null;
            this.inputOperand1.InputControl = null;
            this.inputOperand1.Location = new System.Drawing.Point(419, 169);
            this.inputOperand1.Name = "inputOperand1";
            this.inputOperand1.Size = new System.Drawing.Size(282, 25);
            this.inputOperand1.TabIndex = 2;
            // 
            // FormFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 438);
            this.Controls.Add(this.inputOperand1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.treeSubFilter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFilter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильтрация";
            this.Load += new System.EventHandler(this.FormFilter_Load);
            this.contextMenuTreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeSubFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem addLinkedTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeNodeToolStripMenuItem;
        private Controls.InputOperand inputOperand1;
    }
}