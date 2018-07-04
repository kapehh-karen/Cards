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
using System.Windows.Forms;

namespace Core.Data.Model.Preprocessors
{
    public static class Processors
    {
        public static IFieldProcessor GetFieldProcessor(TableData table, IDesignControl control)
        {
            var property = (FieldProperty)control.GetProperty<FieldProperty>();
            if (property?.Value == null)
                return null;

            var field = table.GetFieldByName(property.Value as string);
            if (field == null)
                return null;

            IFieldProcessor proc;

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

            proc.AllowVisualStates = true;
            proc.Field = field;
            proc.Control = control;
            (control as Control).Tag = proc;
            return proc;
        }

        public static ILinkedTableProcessor GetLinkedTableProcessor(TableData table, IDesignControl control)
        {
            var property = (LinkedTableProperty)control.GetProperty<LinkedTableProperty>();
            if (property?.Value == null)
                return null;

            var linkedTable = table.GetLinkedTableByName(property.Value as string);
            if (linkedTable == null)
                return null;

            ILinkedTableProcessor proc = new LinkedTableProcessor()
            {
                LinkedTable = linkedTable,
                Control = control
            };
            (control as Control).Tag = proc;
            return proc;
        }
    }
}
