using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelFieldValue
    {
        public ModelValueState State { get; set; } = ModelValueState.UNCHANGED;

        public FieldData Field { get; set; } = null;

        public object Value { get; set; } = null;

        public object OldValue { get; set; } = null;
    }
}
