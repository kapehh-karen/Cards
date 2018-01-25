using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Filter.Data.Condition.Impl;

namespace Core.Filter.Data.Operator.Impl
{
    public class EqualOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.EQUAL;

        public override string BuildSQLExpression(ItemCondition condition)
            => $"{condition.LeftOperand.SQLExpression} = {condition.RightOperands[0].SQLExpression}";
    }
}
