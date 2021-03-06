﻿using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public class ModelLinkedValue : ICloneable
    {
        public ModelValueState State =>
            Items.Any(item => item.LinkedState != ModelLinkedItemState.UNCHANGED)
            ? ModelValueState.CHANGED
            : ModelValueState.UNCHANGED;

        public LinkedTable LinkedTable { get; set; } = null;

        public List<CardModel> Items { get; set; } = new List<CardModel>();

        public object Clone()
        {
            var value = new ModelLinkedValue()
            {
                LinkedTable = LinkedTable
            };

            Items.ForEach(value.Items.Add);

            return value;
        }
    }
}
