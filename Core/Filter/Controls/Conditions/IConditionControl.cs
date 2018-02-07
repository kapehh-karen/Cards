using Core.Filter.Data.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Controls.Conditions
{
    public interface IConditionControl
    {
        bool IsFirst { get; set; }

        ICondition Condition { get; set; }
    }
}
