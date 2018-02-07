namespace Core.Filter.Controls
{
    partial class InputOperand
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStripInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.constToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subqueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectInput = new System.Windows.Forms.Button();
            this.contextMenuStripInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripInput
            // 
            this.contextMenuStripInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constToolStripMenuItem,
            this.fieldToolStripMenuItem,
            this.subqueryToolStripMenuItem});
            this.contextMenuStripInput.Name = "contextMenuStripInput";
            this.contextMenuStripInput.Size = new System.Drawing.Size(128, 70);
            // 
            // constToolStripMenuItem
            // 
            this.constToolStripMenuItem.Enabled = false;
            this.constToolStripMenuItem.Name = "constToolStripMenuItem";
            this.constToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.constToolStripMenuItem.Text = "Значение";
            this.constToolStripMenuItem.Click += new System.EventHandler(this.constToolStripMenuItem_Click);
            // 
            // fieldToolStripMenuItem
            // 
            this.fieldToolStripMenuItem.Name = "fieldToolStripMenuItem";
            this.fieldToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.fieldToolStripMenuItem.Text = "Поле";
            this.fieldToolStripMenuItem.Click += new System.EventHandler(this.fieldToolStripMenuItem_Click);
            // 
            // subqueryToolStripMenuItem
            // 
            this.subqueryToolStripMenuItem.Name = "subqueryToolStripMenuItem";
            this.subqueryToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.subqueryToolStripMenuItem.Text = "Выборка";
            this.subqueryToolStripMenuItem.Click += new System.EventHandler(this.subqueryToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(32, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 28);
            this.panel1.TabIndex = 3;
            // 
            // btnSelectInput
            // 
            this.btnSelectInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSelectInput.Image = global::Core.Properties.Resources.choose;
            this.btnSelectInput.Location = new System.Drawing.Point(0, 0);
            this.btnSelectInput.Name = "btnSelectInput";
            this.btnSelectInput.Size = new System.Drawing.Size(32, 28);
            this.btnSelectInput.TabIndex = 0;
            this.btnSelectInput.UseVisualStyleBackColor = true;
            this.btnSelectInput.Click += new System.EventHandler(this.btnSelectInput_Click);
            // 
            // InputOperand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSelectInput);
            this.Name = "InputOperand";
            this.Size = new System.Drawing.Size(172, 28);
            this.contextMenuStripInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectInput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInput;
        private System.Windows.Forms.ToolStripMenuItem constToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subqueryToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}
