using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class NotLikeOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.NOT_LIKE;

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} NOT LIKE {Condition.RightOperand?.SQLExpression}";
    }
}
