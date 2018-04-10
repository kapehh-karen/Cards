namespace Core.GroupEdit.Controls
{
    partial class ItemFieldValue
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.controlFieldValue = new Core.GroupEdit.Controls.ControlFieldValue();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поле для замены:";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFieldName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblFieldName.Location = new System.Drawing.Point(113, 9);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(11, 13);
            this.lblFieldName.TabIndex = 1;
            this.lblFieldName.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Заменить на значение:";
            // 
            // controlFieldValue
            // 
            this.controlFieldValue.Field = null;
            this.controlFieldValue.InputControl = null;
            this.controlFieldValue.Location = new System.Drawing.Point(138, 29);
            this.controlFieldValue.Name = "controlFieldValue";
            this.controlFieldValue.Processor = null;
            this.controlFieldValue.Size = new System.Drawing.Size(247, 23);
            this.controlFieldValue.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Core.Properties.Resources.delete_s;
            this.btnDelete.Location = new System.Drawing.Point(393, 27);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(37, 27);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ItemFieldValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.controlFieldValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.label1);
            this.Name = "ItemFieldValue";
            this.Size = new System.Drawing.Size(439, 59);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label label2;
        private ControlFieldValue controlFieldValue;
        private System.Windows.Forms.Button btnDelete;
    }
}
