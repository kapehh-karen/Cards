using Core.Data.Field;
using Core.Filter.Data.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filter.Controls
{
    public interface IInputOperand
    {
        event EventHandler OperandTypeChanged;

        FieldType Type { get; set; }

        event EventHandler OperandFieldChanged;

        FieldData Field { get; set; }

        IFilterOperand Operand { get; set; }
    }
}
