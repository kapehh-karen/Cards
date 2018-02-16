using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class GreaterEqualOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.GREATER_EQUAL;

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} >= {Condition.RightOperand?.SQLExpression}";
    }
}
