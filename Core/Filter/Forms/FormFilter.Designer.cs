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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilter));
            this.treeSubFilter = new System.Windows.Forms.TreeView();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLinkedTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.containerConditionControl1 = new Core.Filter.Controls.ContainerConditionControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnViewSQL = new System.Windows.Forms.Button();
            this.contextMenuTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeSubFilter
            // 
            this.treeSubFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeSubFilter.Location = new System.Drawing.Point(12, 34);
            this.treeSubFilter.Name = "treeSubFilter";
            this.treeSubFilter.Size = new System.Drawing.Size(235, 409);
            this.treeSubFilter.TabIndex = 0;
            this.treeSubFilter.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSubFilter_AfterSelect);
            this.treeSubFilter.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeSubFilter_NodeMouseClick);
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
            this.containerConditionControl1.Location = new System.Drawing.Point(253, 34);
            this.containerConditionControl1.Name = "containerConditionControl1";
            this.containerConditionControl1.Size = new System.Drawing.Size(658, 409);
            this.containerConditionControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Вложенность";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCondition.Location = new System.Drawing.Point(250, 12);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(103, 13);
            this.lblCondition.TabIndex = 4;
            this.lblCondition.Text = "Условие отбора";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::Core.Properties.Resources.back;
            this.btnCancel.Location = new System.Drawing.Point(655, 449);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 36);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "ОТМЕНА";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAccept.Image = global::Core.Properties.Resources.checkmark;
            this.btnAccept.Location = new System.Drawing.Point(766, 449);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(145, 36);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "ПРИМЕНИТЬ";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnViewSQL
            // 
            this.btnViewSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewSQL.Image = global::Core.Properties.Resources.sql_open_file_format;
            this.btnViewSQL.Location = new System.Drawing.Point(12, 449);
            this.btnViewSQL.Name = "btnViewSQL";
            this.btnViewSQL.Size = new System.Drawing.Size(139, 36);
            this.btnViewSQL.TabIndex = 3;
            this.btnViewSQL.Text = "Просмотр SQL";
            this.btnViewSQL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewSQL.UseVisualStyleBackColor = true;
            this.btnViewSQL.Click += new System.EventHandler(this.btnViewSQL_Click);
            // 
            // FormFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 492);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnViewSQL);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.containerConditionControl1);
            this.Controls.Add(this.treeSubFilter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormFilter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильтрация";
            this.Load += new System.EventHandler(this.FormFilter_Load);
            this.contextMenuTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeSubFilter;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem addLinkedTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeNodeToolStripMenuItem;
        private Controls.ContainerConditionControl containerConditionControl1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnViewSQL;
    }
}