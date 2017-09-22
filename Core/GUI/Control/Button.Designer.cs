namespace Core.GUI.Control
{
    partial class Button
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
            this.SuspendLayout();
            // 
            // Button
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Button";
            this.Size = new System.Drawing.Size(75, 23);
            this.Load += new System.EventHandler(this.Button_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Button_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Button_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Button_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Button_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            this.Resize += new System.EventHandler(this.Button_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
