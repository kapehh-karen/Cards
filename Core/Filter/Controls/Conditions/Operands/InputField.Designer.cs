namespace Core.Filter.Controls
{
    partial class InputField
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
            this.btnSelectField = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectField
            // 
            this.btnSelectField.AutoEllipsis = true;
            this.btnSelectField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectField.ForeColor = System.Drawing.Color.Red;
            this.btnSelectField.Location = new System.Drawing.Point(0, 0);
            this.btnSelectField.Name = "btnSelectField";
            this.btnSelectField.Size = new System.Drawing.Size(187, 40);
            this.btnSelectField.TabIndex = 0;
            this.btnSelectField.Text = "Выбрать поле";
            this.btnSelectField.UseVisualStyleBackColor = true;
            this.btnSelectField.Click += new System.EventHandler(this.btnSelectField_Click);
            // 
            // InputField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectField);
            this.Name = "InputField";
            this.Size = new System.Drawing.Size(187, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectField;
    }
}
