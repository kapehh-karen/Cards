namespace Core.Forms.Main
{
    partial class FormSelectTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectTable));
            this.imageListTables = new System.Windows.Forms.ImageList(this.components);
            this.listViewTables = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // imageListTables
            // 
            this.imageListTables.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTables.ImageStream")));
            this.imageListTables.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTables.Images.SetKeyName(0, "classificator.png");
            this.imageListTables.Images.SetKeyName(1, "table.png");
            // 
            // listViewTables
            // 
            this.listViewTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTables.LabelWrap = false;
            this.listViewTables.LargeImageList = this.imageListTables;
            this.listViewTables.Location = new System.Drawing.Point(0, 0);
            this.listViewTables.MultiSelect = false;
            this.listViewTables.Name = "listViewTables";
            this.listViewTables.Size = new System.Drawing.Size(470, 383);
            this.listViewTables.TabIndex = 0;
            this.listViewTables.TileSize = new System.Drawing.Size(216, 70);
            this.listViewTables.UseCompatibleStateImageBehavior = false;
            this.listViewTables.View = System.Windows.Forms.View.Tile;
            this.listViewTables.ItemActivate += new System.EventHandler(this.listViewTables_ItemActivate);
            // 
            // FormSelectTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 383);
            this.Controls.Add(this.listViewTables);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CARDS ::.";
            this.Load += new System.EventHandler(this.FormSelectTable_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListTables;
        private System.Windows.Forms.ListView listViewTables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}