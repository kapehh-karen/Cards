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
            this.containerConditionControl1 = new Core.Filter.Controls.ContainerConditionControl();
            this.contextMenuTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeSubFilter
            // 
            this.treeSubFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeSubFilter.Location = new System.Drawing.Point(12, 12);
            this.treeSubFilter.Name = "treeSubFilter";
            this.treeSubFilter.Size = new System.Drawing.Size(235, 462);
            this.treeSubFilter.TabIndex = 0;
            this.treeSubFilter.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeSubFilter_NodeMouseClick);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(898, 451);
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
            // containerConditionControl1
            // 
            this.containerConditionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerConditionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.containerConditionControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.containerConditionControl1.FilterData = null;
            this.containerConditionControl1.IsFirst = false;
            this.containerConditionControl1.IsRoot = true;
            this.containerConditionControl1.Location = new System.Drawing.Point(253, 12);
            this.containerConditionControl1.Name = "containerConditionControl1";
            this.containerConditionControl1.Size = new System.Drawing.Size(639, 462);
            this.containerConditionControl1.TabIndex = 2;
            // 
            // FormFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 486);
            this.Controls.Add(this.containerConditionControl1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.treeSubFilter);
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
        private Controls.ContainerConditionControl containerConditionControl1;
    }
}