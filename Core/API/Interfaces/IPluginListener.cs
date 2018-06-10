using Core.Config;
using Core.Data.Model;
using Core.Data.Table;
using Core.Forms.Main;
using Core.Forms.Main.CardForm;
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
        public virtual void OnCardsFileLoaded(CardsFile cardsFile)
        {
            // Not Implemented
        }

        /// <summary>
        /// Выполняется при создании формы FormTableView
        /// </summary>
        public virtual void OnFormTableCreated(FormTableView form, TableData table)
        {
            // Not Implemented
        }

        /// <summary>
        /// Выполняется при создании диалогового окна для TableData
        /// </summary>
        public virtual void OnFormModelCreated(FormCardView formView, TableData table, ModelCardView modelView)
        {
            // Not Implemented
        }

        /// <summary>
        /// Вызывается до сохранения. Возвращает true если можно сохранять и false если запретить сохранение.
        /// </summary>
        public virtual bool OnModelBeforeSave(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            // Not Implemented
            return true;
        }

        /// <summary>
        /// Вызывается после сохранения.
        /// </summary>
        public virtual void OnModelAfterSave(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            // Not Implemented
        }

        /// <summary>
        /// Вызывается при загрузки модели
        /// </summary>
        public virtual void OnModelLoad(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            // Not Implemented
        }
    }
}
