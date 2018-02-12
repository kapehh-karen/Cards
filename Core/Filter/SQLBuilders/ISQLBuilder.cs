using Core.Filter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.SQLBuilders
{
    public interface ISQLBuilder
    {
        FilterData Filter { get; set; }

        string SQLExpression { get; }
    }
}
