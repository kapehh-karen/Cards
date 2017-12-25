using Core.Data.Design.Controls;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;
using Core.Data.Model.Preprocessors.Impl;
using Core.Data.Table;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model.Preprocessors
{
    public static class Processors
    {
        public static IFieldProcessor GetFieldProcessor(IDesignControl control)
        {
            var property = (FieldProperty)control.GetProperty<FieldProperty>();
            var field = property?.Value as FieldData;
            IFieldProcessor proc;

            if (field is null)
                return null;

            switch (field.Type)
            {
                case FieldType.TEXT:
                    proc = new TextProcessor();
                    break;
                case FieldType.NUMBER:
                    proc = new NumberProcessor();
                    break;
                case FieldType.BOOLEAN:
                    proc = new BooleanProcessor();
                    break;
                case FieldType.DATE:
                    proc = new DateProcessor();
                    break;
                case FieldType.BIND:
                    proc = new BindProcessor();
                    break;
                default:
                    return null;
            }

            proc.Control = control;
            proc.Field = field;
            return proc;
        }

        public static ILinkedTableProcessor GetLinkedTableProcessor(IDesignControl control)
        {
            var property = (LinkedTableProperty)control.GetProperty<LinkedTableProperty>();
            var linkedTable = property?.Value as LinkedTable;

            if (linkedTable is null)
                return null;

            return new LinkedTableProcessor() { LinkedTable = linkedTable, Control = control };
        }
    }
}
