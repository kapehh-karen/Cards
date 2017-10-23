using Core.Data.Design.Controls;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Field;
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
            var field = property.Value as FieldData;
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
                default:
                    return null;
            }

            proc.Control = control;
            proc.Field = field;
            return proc;
        }
    }
}
