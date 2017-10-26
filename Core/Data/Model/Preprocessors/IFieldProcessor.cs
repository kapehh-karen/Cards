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
            if (ModelField != null)
                ModelField.Value = Value;
        }

        public void Load()
        {
            if (ModelField != null)
                Value = ModelField.Value;
        }
        
        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach()
        {
            Load();
        }

        /// <summary>
        /// Detach events
        /// </summary>
        public virtual void Detach() { }

        public FieldData Field { get; set; }

        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }
    }
}
