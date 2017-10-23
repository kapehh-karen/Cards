using Core.Data.Field;
using Core.Data.Table;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class CardModel
    {
        public static CardModel CreateFromTable(TableData table)
        {
            var model = new CardModel() { ID = new ModelFieldValue() { Field = table.IdentifierField } };
            model.FieldValues.AddRange(table.Fields.Where(f => !f.IsIdentifier).Select(f => new ModelFieldValue() { Field = f }));
            return model;
        }

        public ModelFieldValue ID { get; set; } = null;

        public ModelValueState State =>
            FieldValues.Any(field => field.State != ModelValueState.UNCHANGED) ? ModelValueState.CHANGED
            : LinkedValues.Any(link => link.State != ModelValueState.UNCHANGED) ? ModelValueState.CHANGED
            : LinkedState != ModelLinkedItemState.UNCHANGED ? ModelValueState.CHANGED : ModelValueState.UNCHANGED;

        /// <summary>
        /// Используется когда CardModel является частью ModelLinkedValue, а не root-ом
        /// </summary>
        public ModelLinkedItemState LinkedState { get; set; } = ModelLinkedItemState.UNCHANGED;

        public List<ModelFieldValue> FieldValues { get; set; } = new List<ModelFieldValue>();

        public List<ModelLinkedValue> LinkedValues { get; set; } = new List<ModelLinkedValue>();
        
        /// <summary>
        /// При удачном сохранении модели, вызывать это.
        /// Сбрасывает состояние полей в UNCHANGED и устанавливает OldValue в текущее значение
        /// Нужно для того, чтобы иметь возможность не закрывая окна переключаться между записями
        /// </summary>
        public void ResetStates()
        {
            FieldValues.ForEach(fieldValue =>
            {
                fieldValue.State = ModelValueState.UNCHANGED;
                fieldValue.OldValue = fieldValue.Value;
            });

            LinkedState = ModelLinkedItemState.UNCHANGED;

            LinkedValues.ForEach(linkedValue => linkedValue.Items.ForEach(item => item.ResetStates()));
        }
    }
}
