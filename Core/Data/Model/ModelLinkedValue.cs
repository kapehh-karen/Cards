using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelLinkedValue : ICloneable
    {
        public ModelValueState State =>
            Items.Any(item => item.State != ModelValueState.UNCHANGED) ? ModelValueState.CHANGED : ModelValueState.UNCHANGED;

        public LinkedTable Table { get; set; } = null;

        public List<CardModel> Items { get; set; } = new List<CardModel>();

        public object Clone()
        {
            var value = new ModelLinkedValue()
            {
                Table = Table
            };

            Items.ForEach(value.Items.Add);

            return value;
        }
    }
}
