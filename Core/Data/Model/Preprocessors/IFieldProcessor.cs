﻿using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Field;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public abstract class IFieldProcessor
    {
        public void Save()
        {
            if (ModelField != null)
                ModelField.Value = Value;
        }

        public void Load()
        {
            Detach();
            if (ModelField != null)
                Value = ModelField.Value;
            Attach();
        }
        
        public bool CheckRequired()
        {
            return !Field.Required || Value != null;
        }

        /// <summary>
        /// Attach events
        /// </summary>
        public virtual void Attach() { }

        /// <summary>
        /// Detach events
        /// </summary>
        public virtual void Detach() { }

        public FieldData Field { get; set; }

        public DataBase Base { get; set; }

        public ModelFieldValue ModelField { get; set; }

        public abstract object Value { get; set; }

        public abstract IDesignControl Control { get; set; }
    }
}
