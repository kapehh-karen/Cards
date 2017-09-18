namespace CardsServer
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMachineName = new System.Windows.Forms.Label();
            this.lblIPList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMachineName
            // 
            this.lblMachineName.AutoSize = true;
            this.lblMachineName.Location = new System.Drawing.Point(12, 9);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(76, 13);
            this.lblMachineName.TabIndex = 0;
            this.lblMachineName.Text = "MachineName";
            // 
            // lblIPList
            // 
            this.lblIPList.AutoSize = true;
            this.lblIPList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIPList.ForeColor = System.Drawing.Color.Red;
            this.lblIPList.Location = new System.Drawing.Point(12, 37);
            this.lblIPList.Name = "lblIPList";
            this.lblIPList.Size = new System.Drawing.Size(58, 13);
            this.lblIPList.TabIndex = 1;
            this.lblIPList.Text = "List of IP";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 295);
            this.Controls.Add(this.lblIPList);
            this.Controls.Add(this.lblMachineName);
            this.Name = "MainForm";
            this.Text = "Cards - Server";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label lblIPList;
    }
}

