﻿namespace Core.Forms.Main
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTableView));
            this.tableDataGridView1 = new Core.Forms.Main.TableDataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSelectedAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripHeader = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSimpleFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFilterReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGroupEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentsExploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedExportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCommandBar = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize) (this.tableDataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStripHeader.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableDataGridView1
            // 
            this.tableDataGridView1.AllowCache = false;
            this.tableDataGridView1.AllowMultiSelect = true;
            this.tableDataGridView1.AllowUserToAddRows = false;
            this.tableDataGridView1.AllowUserToDeleteRows = false;
            this.tableDataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (245)))), ((int) (((byte) (245)))), ((int) (((byte) (245)))));
            this.tableDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tableDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tableDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView1.CurrentDataTable = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.tableDataGridView1.DesignControls = null;
            this.tableDataGridView1.Filter = null;
            this.tableDataGridView1.InDesigner = false;
            this.tableDataGridView1.KeepSelectedColumn = null;
            this.tableDataGridView1.Location = new System.Drawing.Point(5, 106);
            this.tableDataGridView1.Name = "tableDataGridView1";
            this.tableDataGridView1.ParentControl = null;
            this.tableDataGridView1.ParentField = null;
            this.tableDataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.tableDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.tableDataGridView1.SelectedID = null;
            this.tableDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tableDataGridView1.Size = new System.Drawing.Size(804, 400);
            this.tableDataGridView1.StandardTab = true;
            this.tableDataGridView1.TabIndex = 0;
            this.tableDataGridView1.Table = null;
            this.tableDataGridView1.TableStorageInformation = null;
            this.tableDataGridView1.TableStorageType = Core.Storage.Tables.TableStorageType.Table;
            this.tableDataGridView1.RedSelectingChanged += new System.EventHandler(this.tableDataGridView1_RedSelectingChanged);
            this.tableDataGridView1.PressedKey += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedKey);
            this.tableDataGridView1.PressedClick += new System.Windows.Forms.KeyEventHandler(this.tableDataGridView1_PressedKey);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabelAmount, this.toolStripStatusLabelSelectedAmount });
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(814, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelAmount
            // 
            this.toolStripStatusLabelAmount.Name = "toolStripStatusLabelAmount";
            this.toolStripStatusLabelAmount.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabelAmount.Text = "Всего записей: 0";
            // 
            // toolStripStatusLabelSelectedAmount
            // 
            this.toolStripStatusLabelSelectedAmount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelSelectedAmount.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabelSelectedAmount.Name = "toolStripStatusLabelSelectedAmount";
            this.toolStripStatusLabelSelectedAmount.Size = new System.Drawing.Size(702, 17);
            this.toolStripStatusLabelSelectedAmount.Spring = true;
            this.toolStripStatusLabelSelectedAmount.Text = "Выбрано записей: 0";
            this.toolStripStatusLabelSelectedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripHeader
            // 
            this.toolStripHeader.AutoSize = false;
            this.toolStripHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripButtonRefresh, this.toolStripSeparator1, this.toolStripButtonCreate, this.toolStripButtonChange, this.toolStripSeparator2, this.toolStripSimpleFilter, this.toolStripButtonFilter, this.toolStripButtonFilterReset, this.toolStripSeparator3, this.toolStripButtonGroupEdit, this.toolStripButtonDelete });
            this.toolStripHeader.Location = new System.Drawing.Point(0, 27);
            this.toolStripHeader.Name = "toolStripHeader";
            this.toolStripHeader.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripHeader.Size = new System.Drawing.Size(814, 79);
            this.toolStripHeader.TabIndex = 6;
            this.toolStripHeader.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.AutoSize = false;
            this.toolStripButtonRefresh.Image = global::Core.Properties.Resources.refresh;
            this.toolStripButtonRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(100, 76);
            this.toolStripButtonRefresh.Text = "Обновить";
            this.toolStripButtonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 79);
            // 
            // toolStripButtonCreate
            // 
            this.toolStripButtonCreate.AutoSize = false;
            this.toolStripButtonCreate.Image = global::Core.Properties.Resources.users;
            this.toolStripButtonCreate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonCreate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreate.Name = "toolStripButtonCreate";
            this.toolStripButtonCreate.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonCreate.Text = "Создать";
            this.toolStripButtonCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonCreate.Click += new System.EventHandler(this.toolStripButtonCreate_Click);
            // 
            // toolStripButtonChange
            // 
            this.toolStripButtonChange.AutoSize = false;
            this.toolStripButtonChange.Image = global::Core.Properties.Resources.edit;
            this.toolStripButtonChange.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonChange.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChange.Name = "toolStripButtonChange";
            this.toolStripButtonChange.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonChange.Text = "Изменить";
            this.toolStripButtonChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonChange.Click += new System.EventHandler(this.toolStripButtonChange_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 79);
            // 
            // toolStripSimpleFilter
            // 
            this.toolStripSimpleFilter.AutoSize = false;
            this.toolStripSimpleFilter.Image = global::Core.Properties.Resources.search;
            this.toolStripSimpleFilter.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripSimpleFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSimpleFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSimpleFilter.Name = "toolStripSimpleFilter";
            this.toolStripSimpleFilter.Size = new System.Drawing.Size(80, 76);
            this.toolStripSimpleFilter.Text = "Поиск";
            this.toolStripSimpleFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSimpleFilter.Click += new System.EventHandler(this.toolStripSimpleFilter_Click);
            // 
            // toolStripButtonFilter
            // 
            this.toolStripButtonFilter.AutoSize = false;
            this.toolStripButtonFilter.Image = global::Core.Properties.Resources.funnel;
            this.toolStripButtonFilter.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFilter.Name = "toolStripButtonFilter";
            this.toolStripButtonFilter.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonFilter.Text = "Фильтр";
            this.toolStripButtonFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonFilter.Click += new System.EventHandler(this.toolStripButtonFilter_Click);
            // 
            // toolStripButtonFilterReset
            // 
            this.toolStripButtonFilterReset.AutoSize = false;
            this.toolStripButtonFilterReset.Image = global::Core.Properties.Resources.funnel_off;
            this.toolStripButtonFilterReset.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonFilterReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonFilterReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFilterReset.Name = "toolStripButtonFilterReset";
            this.toolStripButtonFilterReset.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonFilterReset.Text = "Сбросить";
            this.toolStripButtonFilterReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonFilterReset.Click += new System.EventHandler(this.toolStripButtonFilterReset_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 79);
            // 
            // toolStripButtonGroupEdit
            // 
            this.toolStripButtonGroupEdit.AutoSize = false;
            this.toolStripButtonGroupEdit.Image = global::Core.Properties.Resources.layers;
            this.toolStripButtonGroupEdit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonGroupEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonGroupEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGroupEdit.Name = "toolStripButtonGroupEdit";
            this.toolStripButtonGroupEdit.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonGroupEdit.Text = "Гр. коррект.";
            this.toolStripButtonGroupEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonGroupEdit.ToolTipText = "Групповая корректировка";
            this.toolStripButtonGroupEdit.Click += new System.EventHandler(this.toolStripButtonGroupEdit_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonDelete.AutoSize = false;
            this.toolStripButtonDelete.Image = global::Core.Properties.Resources.x_button;
            this.toolStripButtonDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(80, 76);
            this.toolStripButtonDelete.Text = "Удалить";
            this.toolStripButtonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.programToolStripMenuItem, this.toolStripCommandBar });
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(814, 27);
            this.mainMenuStrip.TabIndex = 7;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.documentsExploreToolStripMenuItem, this.toolStripMenuItem3, this.saveToExcelToolStripMenuItem, this.extendedExportToExcelToolStripMenuItem, this.toolStripMenuItem2, this.exitProgramToolStripMenuItem });
            this.programToolStripMenuItem.Image = global::Core.Properties.Resources.terminal;
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.programToolStripMenuItem.Text = "Меню";
            // 
            // documentsExploreToolStripMenuItem
            // 
            this.documentsExploreToolStripMenuItem.Image = global::Core.Properties.Resources.folder1;
            this.documentsExploreToolStripMenuItem.Name = "documentsExploreToolStripMenuItem";
            this.documentsExploreToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.documentsExploreToolStripMenuItem.Text = "Открыть папку с документами";
            this.documentsExploreToolStripMenuItem.Click += new System.EventHandler(this.documentsExploreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(281, 6);
            // 
            // saveToExcelToolStripMenuItem
            // 
            this.saveToExcelToolStripMenuItem.Image = global::Core.Properties.Resources.excel;
            this.saveToExcelToolStripMenuItem.Name = "saveToExcelToolStripMenuItem";
            this.saveToExcelToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.saveToExcelToolStripMenuItem.Text = "Сохранить данные в Excel";
            this.saveToExcelToolStripMenuItem.Click += new System.EventHandler(this.saveToExcelToolStripMenuItem_Click);
            // 
            // extendedExportToExcelToolStripMenuItem
            // 
            this.extendedExportToExcelToolStripMenuItem.Image = global::Core.Properties.Resources.excel;
            this.extendedExportToExcelToolStripMenuItem.Name = "extendedExportToExcelToolStripMenuItem";
            this.extendedExportToExcelToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.extendedExportToExcelToolStripMenuItem.Text = "Расширенный экспорт данных в Excel";
            this.extendedExportToExcelToolStripMenuItem.Click += new System.EventHandler(this.extendedExportToExcelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(281, 6);
            // 
            // exitProgramToolStripMenuItem
            // 
            this.exitProgramToolStripMenuItem.Image = global::Core.Properties.Resources.back;
            this.exitProgramToolStripMenuItem.Name = "exitProgramToolStripMenuItem";
            this.exitProgramToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.exitProgramToolStripMenuItem.Text = "Открыть другую таблицу";
            this.exitProgramToolStripMenuItem.Click += new System.EventHandler(this.exitProgramToolStripMenuItem_Click);
            // 
            // toolStripCommandBar
            // 
            this.toolStripCommandBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripCommandBar.Name = "toolStripCommandBar";
            this.toolStripCommandBar.Size = new System.Drawing.Size(200, 23);
            this.toolStripCommandBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripCommandBar_KeyDown);
            // 
            // FormTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 533);
            this.Controls.Add(this.toolStripHeader);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.tableDataGridView1);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "FormTableView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Таблица";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTableView_FormClosing);
            ((System.ComponentModel.ISupportInitialize) (this.tableDataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripHeader.ResumeLayout(false);
            this.toolStripHeader.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripTextBox toolStripCommandBar;
        #endregion

        private TableDataGridView tableDataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAmount;
        private System.Windows.Forms.ToolStrip toolStripHeader;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripButton toolStripButtonChange;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonFilterReset;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentsExploreToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelectedAmount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroupEdit;
        private System.Windows.Forms.ToolStripMenuItem saveToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem extendedExportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripSimpleFilter;
    }
}