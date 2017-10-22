using Core.Data.Field;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class CardModel
    {
        public ModelFieldValue ID { get; set; } = new ModelFieldValue();

        public ModelValueState State =>
            FieldValues.Any(field => field.State == ModelValueState.CHANGED) ? ModelValueState.CHANGED
            : LinkedValues.Any(link => link.State == ModelValueState.CHANGED) ? ModelValueState.CHANGED
            : ModelValueState.UNCHANGED;

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

            LinkedValues.ForEach(linkedValue => linkedValue.Items.ForEach(item => item.ResetStates()));
        }
    }
}
