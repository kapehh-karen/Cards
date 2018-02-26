using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class NotNullOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.IS_NOT_NULL;

        public override bool Completed => Condition?.LeftOperand?.Completed ?? false;

        // Правый операнд вообще не учитываем, он мусор
        public override IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            if (Condition?.LeftOperand != null)
                foreach (var param in Condition.LeftOperand.GetParameters())
                    yield return param;
        }

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} IS NOT NULL";
    }
}
