using Core.Filter.Data.Operand.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator.Impl
{
    public class NotLikeOperator : IFilterOperator
    {
        public override OperatorType Type => OperatorType.NOT_LIKE;

        // Встатвляем символ % в начало и конец значения поиска
        public override IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            if (Condition?.LeftOperand != null)
                foreach (var param in Condition.LeftOperand.GetParameters())
                    yield return param;

            if (Condition?.RightOperand != null)
            {
                if (Condition.RightOperand is ValueOperand valueOp)
                {
                    yield return new KeyValuePair<string, object>(valueOp.VarName, $"%{valueOp.Value}%");
                }
                else
                {
                    foreach (var param in Condition.RightOperand.GetParameters())
                        yield return param;
                }
            }
        }

        public override string SQLExpression => $"{Condition.LeftOperand?.SQLExpression} NOT LIKE {Condition.RightOperand?.SQLExpression}";
    }
}
