using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class LikeOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.LIKE;

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} LIKE {Condition.RightOperand?.SQLExpression}";
    }
}
