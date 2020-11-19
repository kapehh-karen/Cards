using Core.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.Interfaces;
using Core.Config;
using Core.Data.Model;
using Core.Forms.Main.CardForm;
using Core.Data.Table;

namespace Core.API
{
    public class PluginListener
    {
        public static readonly PluginListener Instance = new PluginListener();

        private PluginListener() { }

        public List<IPluginListener> Listeners { get; } = new List<IPluginListener>();

        public void AddListener(IPluginListener listener)
        {
            if (!Listeners.Contains(listener))
                Listeners.Add(listener);
        }

        public void RemoveListener(IPluginListener listener)
        {
            Listeners.Remove(listener);
        }

        #region Only for Core using

        internal bool EventCommandHandle(FormTableView form, string command)
        {
            // Если команда пустая, то просто возвращаем true чтобы ошибок небыло
            // Если команда не пустая, ищем слушателя который сможет обработать её
            return string.IsNullOrWhiteSpace(command) ||
                   Listeners.Any(listener => listener.OnCommandHandle(form, form.Table, command));
        }

        internal void EventFormTableCreated(FormTableView form)
        {
            Listeners.ForEach(listener => listener.OnFormTableCreated(form, form.Table));
        }

        internal void EventFormModelCreated(FormCardView formView, TableData table, ModelCardView modelView)
        {
            Listeners.ForEach(listener => listener.OnFormModelCreated(formView, table, modelView));
        }

        internal void EventCardsFileLoaded(CardsFile cardsFile)
        {
            Listeners.ForEach(listener => listener.OnCardsFileLoaded(cardsFile));
        }

        internal bool EventModelBeforeSave(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            return Listeners.All(listener => listener.OnModelBeforeSave(table, model, modelView, formView));
        }

        internal void EventModelAfterSave(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            Listeners.ForEach(listener => listener.OnModelAfterSave(table, model, modelView, formView));
        }

        internal void EventModelLoad(TableData table, CardModel model, ModelCardView modelView, FormCardView formView)
        {
            Listeners.ForEach(listener => listener.OnModelLoad(table, model, modelView, formView));
        }

        #endregion
    }
}
