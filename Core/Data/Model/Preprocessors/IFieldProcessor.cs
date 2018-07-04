using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors
{
    public abstract class IFieldProcessor
    {
        public event Action<object, IFieldProcessor> ValueChanged = (v, p) => { };
        private ToolTip toolTip = new ToolTip();

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
            UpdateState();

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

            UpdateState();

            Attach();
        }

        public void UpdateState()
        {
            if (!AllowVisualStates)
                return;

            // Обновляем фоновый цвет
            UpdateColorState();

            // Обновить тултип элементу
            UpdateToolTipText();
        }

        public void UpdateColorState()
        {
            // При изменении значения, если оно поменялось, подсвечиваем элемент
            if (Control is Control ctrl && ctrl != null)
            {
                switch (ModelField?.State)
                {
                    case ModelValueState.CHANGED:
                        ctrl.BackColor = Color.LightGreen;
                        break;
                    case ModelValueState.UNCHANGED:
                        ctrl.BackColor = (Color)DefaultBackColor;
                        break;
                }
            }
        }

        /// <summary>
        /// Обновляет подсказку у элемента
        /// </summary>
        public void UpdateToolTipText()
        {
            if (Control is Control ctrl && ctrl != null)
            {
                toolTip.SetToolTip(ctrl,
                    $"Текущее значение: {ModelField?.DisplayValue ?? "Пусто"}\nПредыдущее значение: {ModelField?.DisplayOldValue ?? "Пусто"}");
            }
        }
        
        public bool CheckRequired()
        {
            // Если поле идентификатор, или обязательное, тогда оно должно быть обязательно заполнено
            return (!Field.IsIdentifier && !Field.Required) || Value != null;
        }

        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach()
        {
            if (!DefaultBackColor.HasValue)
                DefaultBackColor = (Control as Control).BackColor;
        }

        /// <summary>
        /// Detach events
        /// </summary>
        public virtual void Detach()
        {
            if (DefaultBackColor.HasValue)
                (Control as Control).BackColor = DefaultBackColor.Value;
        }

        /// <summary>
        /// Включает или отключает подсвечивание и тултип подсказкии для элемента
        /// </summary>
        public bool AllowVisualStates { get; set; }

        private Color? DefaultBackColor { get; set; }

        public virtual FieldData Field { get; set; }
        
        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }

        public CardModel ParentModel { get; set; }
    }
}
