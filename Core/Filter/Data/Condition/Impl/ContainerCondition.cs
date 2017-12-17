﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Condition.Impl
{
    public class ContainerCondition : ICondition
    {
        // TODO: Обрамлять скобками () только когда кондишионов более одного

        /// <summary>
        /// Отрицание всего выражения
        /// </summary>
        public bool IsNot { get; set; } = false;

        public List<ICondition> Conditions { get; private set; } = new List<ICondition>();
    }
}
