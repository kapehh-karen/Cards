using Core.Config;
using Core.Data.Model;
using Core.Data.Table;
using Core.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Interfaces
{
    public abstract class IPluginListener
    {
        /// <summary>
        /// Выполняется после удачной загрузки CardsFile
        /// </summary>
        /// <param name="cardsFile"></param>
        public virtual void OnCardsFileLoaded(CardsFile cardsFile)
        {
            // Not Implemented
        }

        /// <summary>
        /// Выполняется при создании формы FormTableView
        /// </summary>
        /// <param name="form"></param>
        public virtual void OnFormTableCreated(FormTableView form, TableData table)
        {
            // Not Implemented
        }

        /// <summary>
        /// Вызывается до сохранения. Возвращает true если можно сохранять и false если запретить сохранение.
        /// </summary>
        /// <returns></returns>
        public virtual bool OnModelBeforeSave(CardModel model)
        {
            // Not Implemented
            return true;
        }

        /// <summary>
        /// Вызывается после сохранения.
        /// </summary>
        public virtual void OnModelAfterSave(CardModel model)
        {
            // Not Implemented
        }
    }
}
