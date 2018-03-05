using Core.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.Interfaces;
using Core.Config;

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

        internal void EventFormTableCreated(FormTableView form)
        {
            Listeners.ForEach(listener => listener.OnFormTableCreated(form, form.Table));
        }

        internal void EventCardsFileLoaded(CardsFile cardsFile)
        {
            Listeners.ForEach(listener => listener.OnCardsFileLoaded(cardsFile));
        }

        #endregion
    }
}
