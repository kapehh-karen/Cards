using Core.Data.Field;
using Core.Filter.Data;
using Core.Filter.Data.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Controls.Conditions.Operands
{
    public interface IInputOperand
    {
        event EventHandler OperandTypeChanged;

        event EventHandler OperandFieldChanged;

        FieldType Type { get; set; }
        
        FieldData Field { get; set; }

        FilterData FilterData { get; set; }

        IFilterOperand Operand { get; set; }
    }
}
