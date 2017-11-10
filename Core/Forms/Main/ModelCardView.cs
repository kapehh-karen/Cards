using Core.Data.Base;
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
        private List<ILinkedTableProcessor> linkedTableProcessors = new List<ILinkedTableProcessor>();
        private FormData form;
        private CardModel model;

        public TableData Table { get; set; }

        public DataBase Base { get; set; }

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
            linkedTableProcessors.ForEach(p => p.Load());
        }

        private void AttachModel()
        {
            fieldProcessors.ForEach(p => p.Detach());
            fieldProcessors.Clear();

            linkedTableProcessors.ForEach(p => p.Detach());
            linkedTableProcessors.Clear();

            fieldControls.ForEach(element =>
            {
                var proc = Processors.GetFieldProcessor(element);
                if (proc != null)
                {
                    proc.Base = Base;
                    proc.ModelField = Model.FieldValues.FirstOrDefault(fv => fv.Field == proc.Field);
                    fieldProcessors.Add(proc);
                }
            });

            linkedTableControls.ForEach(element =>
            {
                var proc = Processors.GetLinkedTableProcessor(element);
                if (proc != null)
                {
                    proc.Base = Base;
                    proc.ModelLinkedTable = Model.LinkedValues.FirstOrDefault(lv => lv.Table == proc.Table);
                    proc.ParentModel = model;
                    proc.Attach();
                    linkedTableProcessors.Add(proc);
                }
            });

            UpdateElements();
        }

        public bool CheckRequired()
        {
            foreach (var proc in fieldProcessors)
            {
                if (!proc.CheckRequired())
                {
                    MessageBox.Show($"Поле '{proc.Field.DisplayName}' не заполнено.", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
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
