using Core.Data.Design.Controls;
using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public abstract class IFieldProcessor
    {
        public void Save()
        {
            ModelField.Value = Value;
            ModelField.State = ModelField.OldValue != ModelField.Value ? ModelValueState.CHANGED : ModelValueState.UNCHANGED;
        }

        public FieldData Field { get; set; }

        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }
    }
}
