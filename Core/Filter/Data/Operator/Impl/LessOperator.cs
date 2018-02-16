using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class LessOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.LESS;

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} < {Condition.RightOperand?.SQLExpression}";
    }
}
