namespace Core.Filter.Controls.Conditions.Operands
{
    partial class InputSubquery
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
            this.btnSelectSubquery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectSubquery
            // 
            this.btnSelectSubquery.AutoEllipsis = true;
            this.btnSelectSubquery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectSubquery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectSubquery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectSubquery.ForeColor = System.Drawing.Color.Red;
            this.btnSelectSubquery.Location = new System.Drawing.Point(0, 0);
            this.btnSelectSubquery.Name = "btnSelectSubquery";
            this.btnSelectSubquery.Size = new System.Drawing.Size(191, 33);
            this.btnSelectSubquery.TabIndex = 0;
            this.btnSelectSubquery.Text = "Выбрать выборку";
            this.btnSelectSubquery.UseVisualStyleBackColor = true;
            this.btnSelectSubquery.Click += new System.EventHandler(this.btnSelectSubquery_Click);
            // 
            // InputSubquery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectSubquery);
            this.Name = "InputSubquery";
            this.Size = new System.Drawing.Size(191, 33);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectSubquery;
    }
}
