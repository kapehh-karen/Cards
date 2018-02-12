using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition.Impl
{
    public class ContainerCondition : ICondition
    {
        public override ConditionType Type => ConditionType.CONTAINER;

        public List<ICondition> Conditions { get; set; } = new List<ICondition>();

        public override bool Completed => Conditions.Any(cond => cond.Completed);

        public override IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            foreach (var cond in Conditions.Where(cond => cond.Completed))
                foreach (var param in cond.GetParameters())
                    yield return param;
        }

        public override string SQLExpression
        {
            get
            {
                var completedConditions = Conditions.Where(cond => cond.Completed).ToList();
                // У первого элемента убираем оператор конкатенации
                if (completedConditions.Count > 0)
                    completedConditions[0].ConditionOperator = ConditionOperator.NONE;

                var sql = string.Join(" ", completedConditions.Select(condition =>
                    condition.ConditionOperator == ConditionOperator.NONE ?
                        condition?.SQLExpression :
                        $"{condition.ConditionOperator} {condition?.SQLExpression}").ToArray());

                return completedConditions.Count > 1 ? $"({sql})" : sql;
            }
        }
    }
}
