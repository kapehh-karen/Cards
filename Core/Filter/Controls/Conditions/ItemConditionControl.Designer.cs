﻿using Core.Filter.Controls.Conditions.Operator;

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
            this.inputOperandRight  = new Core.Filter.Controls.InputOperand();
            this.btnActionDelete = new System.Windows.Forms.Button();
            this.cmbConcatenate = new Core.Filter.Controls.Conditions.Operator.InputOperatorConcatenate();
            this.SuspendLayout();
            // 
            // inputOperandLeft
            // 
            this.inputOperandLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputOperandLeft.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.FilterData = null;
            this.inputOperandLeft.InputControl = null;
            this.inputOperandLeft.Location = new System.Drawing.Point(68, 3);
            this.inputOperandLeft.Name = "inputOperandLeft";
            this.inputOperandLeft.Size = new System.Drawing.Size(172, 33);
            this.inputOperandLeft.TabIndex = 0;
            this.inputOperandLeft.Type = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandLeft.OperandTypeChanged += new System.EventHandler(this.inputOperandLeft_OperandTypeChanged);
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
            this.inputOperandRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputOperandRight.DependentType = Core.Data.Field.FieldType.UNKNOWN;
            this.inputOperandRight.FilterData = null;
            this.inputOperandRight.InputControl = null;
            this.inputOperandRight.Location = new System.Drawing.Point(373, 3);
            this.inputOperandRight.Name = "inputOperandRight";
            this.inputOperandRight.Size = new System.Drawing.Size(172, 33);
            this.inputOperandRight.TabIndex = 2;
            this.inputOperandRight.Type = Core.Data.Field.FieldType.UNKNOWN;
            // 
            // btnActionDelete
            // 
            this.btnActionDelete.Image = global::Core.Properties.Resources.delete_s;
            this.btnActionDelete.Location = new System.Drawing.Point(548, 3);
            this.btnActionDelete.Name = "btnActionDelete";
            this.btnActionDelete.Size = new System.Drawing.Size(33, 32);
            this.btnActionDelete.TabIndex = 3;
            this.btnActionDelete.UseVisualStyleBackColor = true;
            this.btnActionDelete.Click += new System.EventHandler(this.btnActionDelete_Click);
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
            this.Controls.Add(this.inputOperandRight);
            this.Controls.Add(this.inputOperator);
            this.Controls.Add(this.inputOperandLeft);
            this.Name = "ItemConditionControl";
            this.Size = new System.Drawing.Size(584, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private InputOperand inputOperandLeft;
        private InputOperator inputOperator;
        private InputOperand inputOperandRight;
        private System.Windows.Forms.Button btnActionDelete;
        private InputOperatorConcatenate cmbConcatenate;
    }
}