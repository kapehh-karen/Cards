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
        /// Правый операнд
        /// </summary>
        public IFilterOperand RightOperand { get; set; }

        public override string SQLExpression => Operator.SQLExpression;
    }
}
