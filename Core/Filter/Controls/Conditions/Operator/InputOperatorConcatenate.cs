using Core.Filter.Data.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Filter.Controls.Conditions.Operator
{
    public class InputOperatorConcatenate : ComboBox
    {
        private class ConditionConcatenate
        {
            public ConditionOperator Operator { get; set; }

            public override string ToString()
            {
                switch (Operator)
                {
                    case ConditionOperator.NONE:
                        return string.Empty;
                    case ConditionOperator.AND:
                        return "И";
                    case ConditionOperator.OR:
                        return "ИЛИ";
                }
                return base.ToString();
            }
        }

        private readonly Dictionary<ConditionOperator, ConditionConcatenate> operators =
            new Dictionary<ConditionOperator, ConditionConcatenate>()
            {
                {
                    ConditionOperator.NONE,
                    new ConditionConcatenate() { Operator = ConditionOperator.NONE }
                },
                {
                    ConditionOperator.AND,
                    new ConditionConcatenate() { Operator = ConditionOperator.AND }
                },
                {
                    ConditionOperator.OR,
                    new ConditionConcatenate() { Operator = ConditionOperator.OR }
                }
            };

        public InputOperatorConcatenate()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            UpdateConditions();
        }

        public ConditionOperator SelectedConditionOperator
        {
            get => (SelectedItem as ConditionConcatenate)?.Operator ?? ConditionOperator.NONE;
            set => SelectedItem = operators.ContainsKey(value) ? operators[value] : null;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            UpdateConditions();
            base.OnEnabledChanged(e);
        }

        private void UpdateConditions()
        {
            if (!Enabled)
            {
                Items.Clear();
                Items.Add(operators[ConditionOperator.NONE]);
                SelectedItem = operators[ConditionOperator.NONE];
            }
            else
            {
                Items.Clear();
                Items.Add(operators[ConditionOperator.AND]);
                Items.Add(operators[ConditionOperator.OR]);
                SelectedItem = operators[ConditionOperator.AND];
            }
        }
    }
}
