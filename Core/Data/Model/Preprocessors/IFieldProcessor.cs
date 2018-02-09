using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public abstract class IFieldProcessor
    {
        public virtual void Save()
        {
            if (ModelField != null)
                ModelField.Value = Value;
        }

        public virtual void Load()
        {
            Detach();
            if (ModelField != null)
                Value = ModelField.Value;
            Attach();
        }
        
        public bool CheckRequired()
        {
            // Если поле идентификатор, или обязательное, тогда оно должно быть обязательно заполнено
            return (!Field.IsIdentifier && !Field.Required) || Value != null;
        }

        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach() { }

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
