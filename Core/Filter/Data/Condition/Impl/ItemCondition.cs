using Core.Filter.Data.Operand;
using Core.Filter.Data.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition.Impl
{
    public class ItemCondition : ICondition
    {
        public override ConditionType Type => ConditionType.ITEM;

        /// <summary>
        /// Левый операнд
        /// </summary>
        public IFilterOperand LeftOperand { get; set; }
        
        /// <summary>
        /// Оператор. Зависит полностью от типа левого операнда.
        /// </summary>
        public IFilterOperator Operator { get; set; }

        /// <summary>
        /// Правые операнды. Типы операндов и их количество управляются оператором.
        /// </summary>
        public List<IFilterOperand> RightOperands { get; set; } = new List<IFilterOperand>(2);

        public override string SQLExpression => Operator.SQLExpression;
    }
}
