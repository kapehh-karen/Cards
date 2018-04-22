using Core.Data.Base;
using Core.Data.Design.Controls;
using Core.Data.Design.InternalData;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Model.Preprocessors;
using Core.Data.Table;
using Core.Forms.Design;
using Core.Helper;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.CardForm
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
                    proc.ModelField = Model.GetModelField(proc.Field);
                    proc.ParentModel = model;
                    fieldProcessors.Add(proc);
                }
            });

            linkedTableControls.ForEach(element =>
            {
                var proc = Processors.GetLinkedTableProcessor(element);
                if (proc != null)
                {
                    proc.Base = Base;
                    proc.ModelLinkedTable = Model.LinkedValues.FirstOrDefault(lv => lv.LinkedTable == proc.LinkedTable);
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
                    NotificationMessage.Error($"Поле \"{proc.Field.DisplayName}\" не заполнено. Необходимо заполнить.");
                    return false;
                }
            }
            foreach (var proc in linkedTableProcessors)
            {
                if (!proc.CheckRequired())
                {
                    NotificationMessage.Error($"Таблица \"{proc.LinkedTable.Table.DisplayName}\" пуста. Необходимо добавить запись.");
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
                var tabPage = new ModelTabPage() { Text = page.Title };
                tabPage.DesignControls = page.Controls.Select(cdata => CreateDesignControl(cdata, tabPage)).ToList();
                return tabPage;
            }).ToArray();

            this.TabPages.AddRange(pages);
        }

        private IDesignControl CreateDesignControl(ControlData control, Control parent)
        {
            var type = Type.GetType(control.FullClassName);
            var element = Activator.CreateInstance(type) as IDesignControl;
            var elementAsControl = element as Control;

            element.ParentControl = parent as IDesignControl;
            element.Properties.ForEach(property =>
            {
                var p = control.Properties.FirstOrDefault(pdata => pdata.Name == property.Name);

                if (p != null)
                    property.Value = p.Value;
            });
            element.DesignControls = control.Chields.Select(cdata => CreateDesignControl(cdata, elementAsControl)).ToList();

            switch (element.ControlType)
            {
                case DesignControlType.FIELD:
                    fieldControls.Add(element);
                    break;
                case DesignControlType.LINKED_TABLE:
                    linkedTableControls.Add(element);
                    break;
            }

            parent.Controls.Add(elementAsControl);
            elementAsControl.HelpRequested += Element_HelpRequested;
            return element;
        }

        private void Element_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            var element = sender as Control;
            var proc = element.Tag;

            if (proc == null)
                return;

            if ((proc is IFieldProcessor fieldProc) && fieldProc.Field != null)
            {
                var field = fieldProc.Field;
                MessageBox.Show($"Название поля: {field.DisplayName}\n" +
                        $"Имя поля: {field.Name}\n" +
                        $"Тип поля: {field.Type.GetTextFieldType()}\n" +
                        (field.Type == FieldType.TEXT ? $"Длина текстового поля: {field.Size}" : string.Empty),
                    Consts.ProgramTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if ((proc is ILinkedTableProcessor linkedProc) && linkedProc.LinkedTable != null)
            {
                var linkedTable = linkedProc.LinkedTable;
                MessageBox.Show(linkedTable.ToString(),
                    Consts.ProgramTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #region API

        public IFieldProcessor GetFieldProcessor(string fieldName) =>
            GetFieldProcessor(Model.GetModelField(fieldName));
        public IFieldProcessor GetFieldProcessor(FieldData field) =>
            GetFieldProcessor(Model.GetModelField(field));
        public IFieldProcessor GetFieldProcessor(ModelFieldValue mfv) =>
            fieldProcessors.SingleOrDefault(it => it.ModelField?.Equals(mfv) ?? false);

        public ILinkedTableProcessor GetLinkedTableProcessor(string outerTableName) =>
            GetLinkedTableProcessor(Model.GetModelLinked(outerTableName));
        public ILinkedTableProcessor GetLinkedTableProcessor(TableData outerTable) =>
            GetLinkedTableProcessor(Model.GetModelLinked(outerTable));
        public ILinkedTableProcessor GetLinkedTableProcessor(ModelLinkedValue mlv) =>
            linkedTableProcessors.SingleOrDefault(it => it.ModelLinkedTable?.Equals(mlv) ?? false);

        #endregion
    }
}
