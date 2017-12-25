using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public abstract class ILinkedTableProcessor
    {
        public DataBase Base { get; set; }

        public LinkedTable LinkedTable { get; set; }

        public CardModel ParentModel { get; set; }

        public ModelLinkedValue ModelLinkedTable { get; set; }

        public abstract IDesignControl Control { get; set; }

        public virtual void Load() { }

        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach()
        {
            //Detach();
            //Load();
        }

        /// <summary>
        /// Detach events
        /// </summary>
        public virtual void Detach() { }
    }
}
