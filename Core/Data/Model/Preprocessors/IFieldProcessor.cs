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
            var value = Value;

            if (ModelField.OldValue != value)
            {
                ModelField.State = ModelValueState.CHANGED;
                ModelField.Value = value;
            }
        }

        public FieldData Field { get; set; }

        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }
    }
}
