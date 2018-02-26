using Core.Filter.Data.Condition.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator
{
    public abstract class IFilterOperator
    {
        public abstract OperatorType Type { get; }

        public virtual ItemCondition Condition { get; set; }

        public virtual bool Completed =>
            Condition != null && Condition.LeftOperand != null && Condition.RightOperand != null &&
            Condition.LeftOperand.Completed && Condition.RightOperand.Completed;

        public virtual IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            if (Condition?.LeftOperand != null)
                foreach (var param in Condition.LeftOperand.GetParameters())
                    yield return param;

            if (Condition?.RightOperand != null)
                foreach (var param in Condition.RightOperand.GetParameters())
                    yield return param;
        }

        public virtual string SQLExpression => string.Empty;
    }
}
