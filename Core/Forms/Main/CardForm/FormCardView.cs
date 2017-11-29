using Core.Connection;
using Core.Data.Base;
using Core.Data.Design.InternalData;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
using Core.Notification;
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
        private TableData table;
        private DataBase mainBase;
        private bool isLinkedModel;

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
                }
                else
                {
                    NotificationMessage.Error($"В таблице \"{table?.DisplayName}\" нет формы.");
                }
            }
        }

        public DataBase Base
        {
            get => mainBase;
            set
            {
                mainBase = value;
                modelCardView1.Base = mainBase;
            }
        }
        
        public bool IsLinkedModel
        {
            get => isLinkedModel;
            set
            {
                isLinkedModel = value;
                txtID.Visible = !isLinkedModel;
            }
        }

        public CardModel Model => modelCardView1.Model;
        
        public void InitializeModel(object id = null)
        {
            if (id == null)
            {
                var model = CardModel.CreateFromTable(Table);
                model.IsEmpty = false; // Для новой записи будем считать что она "Полная" а не "Пустая"
                modelCardView1.Model = model;
            }
            else
            {
                if (ModelHelper.Get(Base, Table, id, out var model))
                {
                    modelCardView1.Model = model;
                    UpdateUiText(id);
                }
            }
        }

        public void InitializeModel(CardModel model)
        {
            if (model.IsEmpty)
            {
                InitializeModel(model.ID?.Value);
            }
            else
            {
                modelCardView1.Model = model.Clone() as CardModel;
                UpdateUiText(model.ID.Value);
            }
        }

        public FormCardView()
        {
            InitializeComponent();
        }

        private void FormCardView_Load(object sender, EventArgs e)
        {

        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!modelCardView1.CheckRequired())
                return;

            if (IsLinkedModel)
            {
                DialogResult = DialogResult.OK;
                return;
            }
            
            if (Model.LinkedState == ModelLinkedItemState.UNCHANGED)
            {
                NotificationMessage.Info("Изменений нет. Сохранение не требуется.");
                return;
            }

            if (ModelHelper.Save(Base, Table, Model))
            {
                UpdateUiText(Model.ID.Value);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateUiText(object id)
        {
            if (id != null)
                txtID.Text = id.ToString();

            this.Text = Model.IsNew ? "Новая запись" : "Изменение записи";
        }

        private void FormCardView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }

        public new DialogResult ShowDialog()
        {
            if (modelCardView1.Form == null)
                return DialogResult.Abort;

            return base.ShowDialog();
        }
    }
}
