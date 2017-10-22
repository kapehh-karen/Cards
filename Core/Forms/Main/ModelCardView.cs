using Core.Data.Design.Controls;
using Core.Data.Design.InternalData;
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
        private FormData form;

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

            parent.Controls.Add(element as Control);

            switch (element.ControlType)
            {
                case DesignControlType.FIELD:
                    // TODO: Attach type preprocessor
                    break;
                case DesignControlType.LINKED_TABLE:
                    // TODO: Attack table preprocessor
                    break;
            }

            return element;
        }
    }
}
