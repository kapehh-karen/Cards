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
        public event Action<object, IFieldProcessor> ValueChanged = (v, p) => { };

        public virtual void Save()
        {
            if (ModelField != null)
            {
                var value = Value;
                ModelField.Value = value;
                OnValueChanged(value, this);
            }
        }

        protected void OnValueChanged(object value, IFieldProcessor processor)
        {
            // Вызываем событие что значение изменилось
            ValueChanged(value, processor);
        }

        public void SetValueAndLoad(object value)
        {
            ModelField.Value = value;
            // Вызываем для того, чтобы сначала выполнить Detach а потом Attach,
            // иначе будут лишний раз дергаться обработчики событий
            Load();
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

        public virtual FieldData Field { get; set; }
        
        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }

        public CardModel ParentModel { get; set; }
    }
}
