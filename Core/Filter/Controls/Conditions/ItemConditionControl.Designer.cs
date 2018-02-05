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
            this.btnActionDelete = new System.Windows.Forms.Button();
            this.cmbConcatenate = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // inputOperandLeft
            // 
            this.inputOperandLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputOperandLeft.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.FilterData = null;
            this.inputOperandLeft.InputControl = null;
            this.inputOperandLeft.Location = new System.Drawing.Point(68, 3);
            this.inputOperandLeft.Name = "inputOperandLeft";
            this.inputOperandLeft.Size = new System.Drawing.Size(172, 33);
            this.inputOperandLeft.TabIndex = 0;
            this.inputOperandLeft.Type = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.OperandTypeChanged += new System.EventHandler(this.inputOperand1_OperandTypeChanged);
            // 
            // inputOperator
            // 
            this.inputOperator.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputOperator.FormattingEnabled = true;
            this.inputOperator.Location = new System.Drawing.Point(246, 8);
            this.inputOperator.Name = "inputOperator";
            this.inputOperator.Size = new System.Drawing.Size(121, 21);
            this.inputOperator.TabIndex = 1;
            // 
            // inputOperandRight1
            // 
            this.inputOperandRight1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputOperandRight1.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandRight1.FilterData = null;
            this.inputOperandRight1.InputControl = null;
            this.inputOperandRight1.Location = new System.Drawing.Point(373, 3);
            this.inputOperandRight1.Name = "inputOperandRight1";
            this.inputOperandRight1.Size = new System.Drawing.Size(172, 33);
            this.inputOperandRight1.TabIndex = 2;
            this.inputOperandRight1.Type = Core.Data.Field.FieldType.UNKNOWN;
            // 
            // btnActionDelete
            // 
            this.btnActionDelete.Location = new System.Drawing.Point(551, 8);
            this.btnActionDelete.Name = "btnActionDelete";
            this.btnActionDelete.Size = new System.Drawing.Size(24, 21);
            this.btnActionDelete.TabIndex = 3;
            this.btnActionDelete.Text = "X";
            this.btnActionDelete.UseVisualStyleBackColor = true;
            // 
            // cmbConcatenate
            // 
            this.cmbConcatenate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcatenate.FormattingEnabled = true;
            this.cmbConcatenate.Location = new System.Drawing.Point(3, 8);
            this.cmbConcatenate.Name = "cmbConcatenate";
            this.cmbConcatenate.Size = new System.Drawing.Size(59, 21);
            this.cmbConcatenate.TabIndex = 4;
            // 
            // ItemConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbConcatenate);
            this.Controls.Add(this.btnActionDelete);
            this.Controls.Add(this.inputOperandRight1);
            this.Controls.Add(this.inputOperator);
            this.Controls.Add(this.inputOperandLeft);
            this.Name = "ItemConditionControl";
            this.Size = new System.Drawing.Size(584, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private InputOperand inputOperandLeft;
        private InputOperator inputOperator;
        private InputOperand inputOperandRight1;
        private System.Windows.Forms.Button btnActionDelete;
        private System.Windows.Forms.ComboBox cmbConcatenate;
    }
}
