﻿using Core.Filter.Data.Operand;
using Core.Filter.Data.Operator;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition.Impl
{
    public class ItemCondition : ICondition
    {
        public override ConditionType Type => ConditionType.ITEM;

        /// <summary>
        /// Левый операнд
        /// </summary>
        public IFilterOperand LeftOperand { get; set; }
        
        /// <summary>
        /// Оператор. Зависит полностью от типа левого операнда.
        /// </summary>
        public IFilterOperator Operator { get; set; }

        /// <summary>
        /// Правый операнд
        /// </summary>
        public IFilterOperand RightOperand { get; set; }

        public override bool Completed => Operator != null && Operator.Completed;

        public override IEnumerable<KeyValuePair<string, object>> GetParameters()
        {
            if (LeftOperand != null)
                foreach (var param in LeftOperand.GetParameters())
                    yield return param;

            if (RightOperand != null)
            {
                // NOTE: Грязный хак, чтобы вставить символ % в операторы LIKE и NOT LIKE
                var likeOperators = Operator?.Type == OperatorType.LIKE || Operator?.Type == OperatorType.NOT_LIKE;
                var valueOperand = RightOperand?.Type == OperandType.VALUE;

                if (likeOperators && valueOperand)
                {
                    var param = RightOperand.GetParameters().ElementAt(0);
                    yield return new KeyValuePair<string, object>(param.Key, $"%{param.Value}%");
                }
                else
                {
                    foreach (var param in RightOperand.GetParameters())
                        yield return param;
                }
            }
        }

        public override string SQLExpression => Operator?.SQLExpression;
    }
}
