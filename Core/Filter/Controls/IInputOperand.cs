using Core.Data.Field;
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
    }
}
