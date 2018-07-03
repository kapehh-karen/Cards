using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Design.Controls.LinkedTableControl;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors
{
    public abstract class ILinkedTableProcessor
    {
        public event Action<ILinkedTableProcessor> ValueChanged = (p) => { };

        protected void OnValueChanged(ILinkedTableProcessor processor)
        {
            // Вызываем событие что значение изменилось
            ValueChanged(processor);
        }

        public void UpdateState()
        {
            (Control as Control)?.Invalidate();
        }

        public LinkedTable LinkedTable { get; set; }

        public CardModel ParentModel { get; set; }

        public ModelLinkedValue ModelLinkedTable { get; set; }

        public abstract IDesignControl Control { get; set; }

        public virtual void Load() { }

        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach() { }

        /// <summary>
        /// Detach events
        /// </summary>
        public virtual void Detach() { }

        public bool CheckRequired()
        {
            return !LinkedTable.Required || (ModelLinkedTable.Items.Count > 0);
        }
    }
}
