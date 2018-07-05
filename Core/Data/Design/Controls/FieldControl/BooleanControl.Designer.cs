using Core.Common;

namespace Core.Data.Design.Controls.FieldControl
{
    partial class BooleanControl
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
            this.cmbFlag = new System.Windows.Forms.ComboBox();
            this.lblText = new Core.Common.NotSelectableLabel();
            this.SuspendLayout();
            // 
            // cmbFlag
            // 
            this.cmbFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlag.FormattingEnabled = true;
            this.cmbFlag.Items.AddRange(new object[] {
            "",
            "Нет",
            "Да"});
            this.cmbFlag.Location = new System.Drawing.Point(12, 3);
            this.cmbFlag.Name = "cmbFlag";
            this.cmbFlag.Size = new System.Drawing.Size(44, 21);
            this.cmbFlag.TabIndex = 0;
            this.cmbFlag.SelectedIndexChanged += new System.EventHandler(this.cmbFlag_SelectedIndexChanged);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(6, 6);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(0, 13);
            this.lblText.TabIndex = 1;
            // 
            // BooleanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.cmbFlag);
            this.MinimumSize = new System.Drawing.Size(27, 27);
            this.Name = "BooleanControl";
            this.Size = new System.Drawing.Size(60, 27);
            this.Resize += new System.EventHandler(this.BooleanControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFlag;
        private NotSelectableLabel lblText;
    }
}
