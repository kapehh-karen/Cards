﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition
{
    public abstract class ICondition
    {
        /// <summary>
        /// Оператор AND, OR, etc. перед самим условием
        /// </summary>
        public ConditionOperator ConditionOperator { get; set; } = ConditionOperator.NONE;

        public abstract ConditionType Type { get; }

        public virtual bool Completed => true;

        public virtual IEnumerable<KeyValuePair<string, object>> GetParameters() { yield break; }

        public virtual string SQLExpression => string.Empty;
    }
}
