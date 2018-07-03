using Core.API;
using Core.Connection;
using Core.Data.Base;
using Core.Data.Design.InternalData;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
using Core.Notification;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.CardForm
{
    public partial class FormCardView : Form
    {
        private HighlightFocusedControl highlight;

        public FormCardView()
        {
            InitializeComponent();
            highlight = new HighlightFocusedControl(this);
        }

        public void SendEventFormCreated()
        {
            // В момент создания формы, вызываем событие
            PluginListener.Instance.EventFormModelCreated(this, Table, modelCardView1);
        }

        private TableData table;
        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                modelCardView1.Table = table;

                if (table?.Form != null)
                {
                    var screen = Screen.FromControl(this);
                    var screenRect = screen.WorkingArea;
                    var tableFormSize = table.Form.Size;
                    Size clientSize;
                    
                    if (screenRect.Width < tableFormSize.Width || screenRect.Height < tableFormSize.Height)
                    {
                        var minWidth = Math.Min(screenRect.Width, tableFormSize.Width);
                        var minHeight = Math.Min(screenRect.Height, tableFormSize.Height);
                        
                        this.Size = new Size(minWidth, minHeight);
                        clientSize = this.ClientSize;

                        if (minHeight + 40 > screenRect.Height)
                            clientSize.Height -= 40;
                        else
                            this.Height += 40;
                    }
                    else
                    {
                        this.Size = new Size(tableFormSize.Width, tableFormSize.Height);
                        clientSize = this.ClientSize;
                        this.Height += 40;
                    }

                    modelCardView1.Size = clientSize;
                    modelCardView1.Form = table.Form;

                    // После обновления UI делаем выделение активных контролов
                    highlight.Install();

                    FieldForFastJump = Table.FastJumpField;
                    if (FieldForFastJump == null)
                    {
                        lblCodeFieldName.Visible = false;
                        txtInputCode.Visible = false;
                    }
                    else
                    {
                        lblCodeFieldName.Text = FieldForFastJump.DisplayName;
                    }
                }
                else
                {
                    NotificationMessage.Error($"В таблице \"{table?.DisplayName}\" нет формы.");
                }
            }
        }

        private bool isLinkedModel;
        public bool IsLinkedModel
        {
            get => isLinkedModel;
            set
            {
                isLinkedModel = value;

                // Во время редактирования внешних данных, такие финты нам не нужны
                if (isLinkedModel)
                {
                    lblCodeFieldName.Visible = false;
                    txtInputCode.Visible = false;
                }
            }
        }

        public CardModel Model => modelCardView1.Model;

        private FieldData FieldForFastJump { get; set; }
        
        private void ModelLoaded()
        {
            // Обновляем UI
            UpdateUiText(Model.ID.Value);

            // После загрузки модели вызываем событие
            PluginListener.Instance.EventModelLoad(Table, Model, modelCardView1, this);
        }

        public void InitializeModel(object id = null, FieldData fieldForSearch = null)
        {
            if (id == null)
            {
                var model = CardModel.CreateFromTable(Table);
                model.IsEmpty = false; // Для новой записи будем считать что она "Полная" а не "Пустая"
                modelCardView1.Model = model;
                ModelLoaded();
            }
            else
            {
                if (ModelHelper.Get(Table, id, out var model, fieldForSearch))
                {
                    modelCardView1.Model = model;
                    ModelLoaded();
                }
            }
        }

        public void InitializeModel(CardModel model)
        {
            if (model.IsEmpty)
            {
                // Если запись IsEmpty, значит её нужно подгрузить полностью
                InitializeModel(model.ID?.Value);
            }
            else
            {
                // В ином случае, мы имеем уже загруженную CardModel с которой можно сразу работать
                // Создаем копию, т.к. изменения в CardModel не всегда нужно применять (если пользователь нажмет "Отмена")
                modelCardView1.Model = model.Clone() as CardModel;
                ModelLoaded();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Если один из плагинов не дает разрешение на сохранение, то не сохраняем изменения
            if (!PluginListener.Instance.EventModelBeforeSave(Table, Model, modelCardView1, this))
                return;

            // Проверяем все обязательные поля и внешние данные
            if (!modelCardView1.CheckRequired())
                return;

            if (IsLinkedModel)
            {
                DialogResult = DialogResult.OK;
                return;
            }
            
            if (Model.State == ModelValueState.UNCHANGED)
            {
                NotificationMessage.Info("Изменений нет. Сохранение не требуется.");
                return;
            }

            if (ModelHelper.Save(Table, Model))
            {
                UpdateUiText(Model.ID.Value);
                modelCardView1.UpdateState();
                PluginListener.Instance.EventModelAfterSave(Table, Model, modelCardView1, this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateUiText(object id)
        {
            this.Text = Model.IsNew ? "Новая запись" : $"Изменение записи #{id}";

            // Если есть поле для быстрого перехода, значит есть текстбокс для значения
            // А если есть текстбокс, значит туда надо вписать текущее значение этого поля
            if (FieldForFastJump != null)
                txtInputCode.Text = Convert.ToString(Model[FieldForFastJump]);
        }

        public new DialogResult ShowDialog()
        {
            if (modelCardView1.Form == null || Model == null)
                return DialogResult.Abort;

            return base.ShowDialog();
        }

        private bool CheckIgnoreChanges()
        {
            if (IsLinkedModel)
                return true;

            // Только если мы нажимаем "Отмена"
            if (DialogResult == DialogResult.Cancel && Model.State == ModelValueState.CHANGED)
            {
                if (MessageBox.Show("Вы уверены? Все несохраненные изменения будут утеряны.",
                    Consts.ProgramTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private void FormCardView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
                e.Cancel = !CheckIgnoreChanges();

            // Если событие не отменено сохраняем настройки внешних таблиц
            if (!e.Cancel)
                modelCardView1.TableSettingsSave();
        }

        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            var newId = txtInputCode.Text;
            if (e.KeyCode == Keys.Enter)
                InitializeModel(newId, FieldForFastJump);
        }
        
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
