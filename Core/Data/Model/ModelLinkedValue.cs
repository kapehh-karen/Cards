using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelLinkedValue
    {
        public ModelValueState State =>
            Items.Any(item => item.State == ModelValueState.CHANGED) ? ModelValueState.CHANGED : ModelValueState.UNCHANGED;

        public LinkedTable Table { get; set; } = null;

        public List<CardModel> Items { get; set; } = new List<CardModel>();
    }
}
