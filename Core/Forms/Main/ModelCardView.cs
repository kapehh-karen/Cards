using Core.Data.Design.Controls;
using Core.Data.Design.InternalData;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Model.Preprocessors;
using Core.Data.Table;
using Core.Forms.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main
{
    public class ModelCardView : TabControl
    {
        private List<IFieldProcessor> fieldProcessors = new List<IFieldProcessor>();
        private TableData table;

        public TableData Table
        {
            get => table;
            set
            {
                table = value;

                if (table?.Form != null)
                {
                    Model = CardModel.CreateFromTable(table);
                    LoadFromData(table.Form);
                }
            }
        }

        public CardModel Model { get; private set; }

        private void LoadFromData(FormData formData)
        {
            var pages = formData.Pages.Select(page =>
            {
                var cardTabPage = new CardTabPage() { Text = page.Title };
                cardTabPage.DesignControls = page.Controls.Select(cdata => MapDataToDesignControl(cdata, cardTabPage)).ToList();
                return cardTabPage;
            }).ToArray();

            this.TabPages.AddRange(pages);
        }

        private IDesignControl MapDataToDesignControl(ControlData control, Control parent)
        {
            var type = Type.GetType(control.FullClassName);
            var element = Activator.CreateInstance(type) as IDesignControl;

            element.ParentControl = parent as IDesignControl;
            element.Properties.ForEach(property =>
            {
                var p = control.Properties.FirstOrDefault(pdata => pdata.Name == property.Name);

                if (p != null)
                    property.Value = p.Value;
            });
            element.DesignControls = control.Chields.Select(cdata => MapDataToDesignControl(cdata, element as Control)).ToList();
            
            switch (element.ControlType)
            {
                case DesignControlType.FIELD:
                    var preprocessor = Processors.GetFieldProcessor(element);
                    if (preprocessor != null)
                    {
                        preprocessor.ModelField = Model.FieldValues.FirstOrDefault(fv => fv.Field == preprocessor.Field);
                        fieldProcessors.Add(preprocessor);
                    }
                    break;
                case DesignControlType.LINKED_TABLE:
                    break;
            }
            
            parent.Controls.Add(element as Control);
            return element;
        }
    }
}
