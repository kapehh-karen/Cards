using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition.Impl
{
    public class ContainerCondition : ICondition
    {
        public List<ICondition> Conditions { get; private set; } = new List<ICondition>();

        public override string SQLExpression
        {
            get
            {
                // Обрамлять скобками () только когда кондишионов более одного
                var sql = string.Join(" ", Conditions.Select(condition =>
                    condition.ConditionOperator == ConditionOperator.NONE ?
                        condition.SQLExpression :
                        $"{condition.ConditionOperator} {condition.SQLExpression}").ToArray());
                return Conditions.Count > 1 ? $"({sql})" : sql;
            }
        }
    }
}
