using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class CardModel : ICloneable
    {
        public static CardModel CreateFromTable(TableData table)
        {
            var model = new CardModel();
            model.FieldValues.AddRange(table.Fields.Select(f => new ModelFieldValue() { Field = f }));
            model.LinkedValues.AddRange(table.LinkedTables.Select(l => new ModelLinkedValue() { LinkedTable = l }));
            return model;
        }

        // -------------------- START BODY --------------------

        private ModelLinkedItemState linkedState = ModelLinkedItemState.UNKNOWN;

        /// <summary>
        /// Используется для записи-загрушки, записи без внешних данных
        /// </summary>
        public bool IsEmpty { get; set; } = true;

        public ModelFieldValue ID => FieldValues.FirstOrDefault(fv => fv.Field.IsIdentifier);

        public ModelValueState State =>
            FieldValues.Any(field => field.State != ModelValueState.UNCHANGED) ||
            LinkedValues.Any(link => link.State != ModelValueState.UNCHANGED)
            ? ModelValueState.CHANGED : ModelValueState.UNCHANGED;
        
        public bool IsNew => ID?.Value == null;
        
        /// <summary>
        /// Используется когда CardModel является частью ModelLinkedValue, а не root-ом
        /// </summary>
        public ModelLinkedItemState LinkedState
        {
            get => linkedState == ModelLinkedItemState.UNKNOWN
                    ? State != ModelValueState.UNCHANGED ? ModelLinkedItemState.CHANGED : ModelLinkedItemState.UNCHANGED
                    : linkedState;
            set => linkedState = value;
        }

        public List<ModelFieldValue> FieldValues { get; set; } = new List<ModelFieldValue>();

        public List<ModelLinkedValue> LinkedValues { get; set; } = new List<ModelLinkedValue>();

        public ModelFieldValue GetModelField(FieldData field) => FieldValues.FirstOrDefault(mfv => mfv.Field.Equals(field));

        public object this[string field]
        {
            get => FieldValues.FirstOrDefault(mfv => mfv.Field.Name.Equals(field))?.Value;
            set
            {
                var fieldValue = FieldValues.FirstOrDefault(mfv => mfv.Field.Name.Equals(field));
                if (fieldValue != null)
                    fieldValue.Value = value;
            }
        }

        public object this[FieldData field]
        {
            get => FieldValues.FirstOrDefault(mfv => mfv.Field.Equals(field))?.Value;
            set
            {
                var fieldValue = FieldValues.FirstOrDefault(mfv => mfv.Field.Equals(field));
                if (fieldValue != null)
                    fieldValue.Value = value;
            }
        }

        /// <summary>
        /// При удачном сохранении модели, вызывать это.
        /// Сбрасывает состояние полей в UNCHANGED и устанавливает OldValue в текущее значение
        /// Нужно для того, чтобы иметь возможность не закрывая окна переключаться между записями
        /// </summary>
        public void ResetStates()
        {
            FieldValues.ForEach(fieldValue => {
                fieldValue.OldValue = fieldValue.Value;

                // Если не существует связанного значения, то нету и идентификатора
                if ((fieldValue.Field.Type == FieldType.BIND) && (fieldValue.BindData == null))
                {
                    // Текущее значение - NULL, а OldValue предыдущее значение Value
                    fieldValue.Value = null;
                }
            });

            LinkedValues.ForEach(linkedValue => {
                // Удаляем удаленные записи
                linkedValue.Items = linkedValue.Items.Where(item => item.LinkedState != ModelLinkedItemState.DELETED).ToList();

                // Ресетаем все внешние элементы
                linkedValue.Items.ForEach(item => item.ResetStates());
            });

            LinkedState = ModelLinkedItemState.UNKNOWN;
        }

        /// <summary>
        /// Рекурсивно помечает запись (и её внешние данные, если имеются) как DELETED
        /// </summary>
        public void CheckDeleteFull()
        {
            LinkedState = ModelLinkedItemState.DELETED;

            // Помечаем как удаленную не только эту запись, но и все связанные с нею записи
            LinkedValues.ForEach(linkedValue => linkedValue.Items.ForEach(item => item.CheckDeleteFull()));
        }

        public object Clone()
        {
            var model = new CardModel()
            {
                // private
                linkedState = linkedState,
                // public
                IsEmpty = IsEmpty
            };

            FieldValues.Select(fv => fv.Clone() as ModelFieldValue).ForEach(model.FieldValues.Add);
            LinkedValues.Select(lv => lv.Clone() as ModelLinkedValue).ForEach(model.LinkedValues.Add);

            return model;
        }

        // -------------------- START EQUALS --------------------
        
        public static bool operator ==(CardModel a, CardModel b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.ID?.Value == b.ID?.Value;
        }

        public static bool operator !=(CardModel a, CardModel b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is CardModel cardObj)
                return this == cardObj;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
