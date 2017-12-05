using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Data.Operator
{
    public abstract class IFilterOperator
    {
        public abstract OperatorType Type { get; }
    }
}
