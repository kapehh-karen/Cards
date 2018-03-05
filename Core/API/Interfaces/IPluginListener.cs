using Core.Config;
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
        public virtual void OnCardsFileLoaded(CardsFile cardsFile)
        {

        }

        /// <summary>
        /// Выполняется при создании формы FormTableView
        /// </summary>
        /// <param name="form"></param>
        public virtual void OnFormTableCreated(FormTableView form, TableData table)
        {
            // Not Implemented
        }
    }
}
