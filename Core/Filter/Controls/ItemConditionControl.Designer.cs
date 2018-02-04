namespace Core.Filter.Controls
{
    partial class ItemConditionControl
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
            this.inputOperandLeft = new Core.Filter.Controls.InputOperand();
            this.inputOperator = new Core.Filter.Controls.InputOperator();
            this.inputOperandRight1 = new Core.Filter.Controls.InputOperand();
            this.SuspendLayout();
            // 
            // inputOperandLeft
            // 
            this.inputOperandLeft.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.FilterData = null;
            this.inputOperandLeft.InputControl = null;
            this.inputOperandLeft.Location = new System.Drawing.Point(0, 0);
            this.inputOperandLeft.Name = "inputOperandLeft";
            this.inputOperandLeft.Size = new System.Drawing.Size(172, 25);
            this.inputOperandLeft.TabIndex = 0;
            this.inputOperandLeft.Type = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.OperandTypeChanged += new System.EventHandler(this.inputOperand1_OperandTypeChanged);
            // 
            // inputOperator
            // 
            this.inputOperator.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperator.FormattingEnabled = true;
            this.inputOperator.Location = new System.Drawing.Point(178, 3);
            this.inputOperator.Name = "inputOperator";
            this.inputOperator.Size = new System.Drawing.Size(121, 21);
            this.inputOperator.TabIndex = 1;
            // 
            // inputOperandRight1
            // 
            this.inputOperandRight1.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandRight1.FilterData = null;
            this.inputOperandRight1.InputControl = null;
            this.inputOperandRight1.Location = new System.Drawing.Point(308, 0);
            this.inputOperandRight1.Name = "inputOperandRight1";
            this.inputOperandRight1.Size = new System.Drawing.Size(172, 25);
            this.inputOperandRight1.TabIndex = 2;
            this.inputOperandRight1.Type = Core.Data.Field.FieldType.UNKNOWN;
            // 
            // ItemConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.inputOperandRight1);
            this.Controls.Add(this.inputOperator);
            this.Controls.Add(this.inputOperandLeft);
            this.Name = "ItemConditionControl";
            this.Size = new System.Drawing.Size(552, 79);
            this.ResumeLayout(false);

        }

        #endregion

        private InputOperand inputOperandLeft;
        private InputOperator inputOperator;
        private InputOperand inputOperandRight1;
    }
}
