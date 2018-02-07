using Core.Filter.Controls.Conditions.Operator;

namespace Core.Filter.Controls
{
    partial class ContainerConditionControl
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddContainer = new System.Windows.Forms.Button();
            this.btnAddCondition = new System.Windows.Forms.Button();
            this.cmbConcatenate = new Core.Filter.Controls.Conditions.Operator.InputOperatorConcatenate();
            this.btnActionDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 32);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(459, 5);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.WrapContents = false;
            this.flowLayoutPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel_ControlAdded);
            this.flowLayoutPanel.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel_ControlRemoved);
            // 
            // btnAddContainer
            // 
            this.btnAddContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddContainer.Image = global::Core.Properties.Resources.add_s;
            this.btnAddContainer.Location = new System.Drawing.Point(342, 3);
            this.btnAddContainer.Name = "btnAddContainer";
            this.btnAddContainer.Size = new System.Drawing.Size(75, 23);
            this.btnAddContainer.TabIndex = 1;
            this.btnAddContainer.Text = "Группа";
            this.btnAddContainer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddContainer.UseVisualStyleBackColor = true;
            this.btnAddContainer.Click += new System.EventHandler(this.btnAddContainer_Click);
            // 
            // btnAddCondition
            // 
            this.btnAddCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCondition.Image = global::Core.Properties.Resources.add_s;
            this.btnAddCondition.Location = new System.Drawing.Point(261, 3);
            this.btnAddCondition.Name = "btnAddCondition";
            this.btnAddCondition.Size = new System.Drawing.Size(75, 23);
            this.btnAddCondition.TabIndex = 2;
            this.btnAddCondition.Text = "Условие";
            this.btnAddCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddCondition.UseVisualStyleBackColor = true;
            this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
            // 
            // cmbConcatenate
            // 
            this.cmbConcatenate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcatenate.FormattingEnabled = true;
            this.cmbConcatenate.Location = new System.Drawing.Point(3, 5);
            this.cmbConcatenate.Name = "cmbConcatenate";
            this.cmbConcatenate.Size = new System.Drawing.Size(59, 21);
            this.cmbConcatenate.TabIndex = 5;
            // 
            // btnActionDelete
            // 
            this.btnActionDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActionDelete.Image = global::Core.Properties.Resources.delete_s;
            this.btnActionDelete.Location = new System.Drawing.Point(431, 3);
            this.btnActionDelete.Name = "btnActionDelete";
            this.btnActionDelete.Size = new System.Drawing.Size(26, 23);
            this.btnActionDelete.TabIndex = 6;
            this.btnActionDelete.UseVisualStyleBackColor = true;
            this.btnActionDelete.Click += new System.EventHandler(this.btnActionDelete_Click);
            // 
            // ContainerConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnActionDelete);
            this.Controls.Add(this.cmbConcatenate);
            this.Controls.Add(this.btnAddCondition);
            this.Controls.Add(this.btnAddContainer);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "ContainerConditionControl";
            this.Size = new System.Drawing.Size(465, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btnAddContainer;
        private System.Windows.Forms.Button btnAddCondition;
        private InputOperatorConcatenate cmbConcatenate;
        private System.Windows.Forms.Button btnActionDelete;
    }
}
