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
        private List<IDesignControl> fieldControls = new List<IDesignControl>();
        private List<IDesignControl> linkedTableControls = new List<IDesignControl>();
        private FormData form;
        private CardModel model;

        public FormData Form
        {
            get => form;
            set
            {
                form = value;

                if (form != null)
                    LoadFromData(form);
            }
        }

        public CardModel Model
        {
            get => model;
            set
            {
                model = value;

                if (model != null)
                    AttachModel();
            }
        }

        public void UpdateElements()
        {
            fieldProcessors.ForEach(p => p.Load());
        }

        private void AttachModel()
        {
            fieldProcessors.ForEach(p => p.Detach());
            fieldProcessors.Clear();

            fieldControls.ForEach(element =>
            {
                var preprocessor = Processors.GetFieldProcessor(element);
                if (preprocessor != null)
                {
                    preprocessor.ModelField = Model.FieldValues.FirstOrDefault(fv => fv.Field == preprocessor.Field);
                    preprocessor.Attach();
                    fieldProcessors.Add(preprocessor);
                }
            });
        }

        private void LoadFromData(FormData formData)
        {
            fieldControls.Clear();
            linkedTableControls.Clear();

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
                    fieldControls.Add(element);
                    break;
                case DesignControlType.LINKED_TABLE:
                    linkedTableControls.Add(element);
                    break;
            }
            
            parent.Controls.Add(element as Control);
            return element;
        }
    }
}
