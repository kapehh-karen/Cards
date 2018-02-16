using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class GreaterOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.GREATER;

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} > {Condition.RightOperand?.SQLExpression}";
    }
}
