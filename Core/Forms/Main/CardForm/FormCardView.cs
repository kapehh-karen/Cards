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
                    this.Size = new Size(table.Form.Size.Width, table.Form.Size.Height);
                    var clientSize = this.ClientSize;
                    this.Height += 40;
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

        public bool IsNew { get; private set; }

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

        private void ModelFieldsFill(CardModel model, TableData tableModel, SqlDataReader reader)
        {
            tableModel.Fields.ForEach(f => model[f.Name] = FieldHelper.CastValue(f, reader[f.Name]));
        }

        private void ModelBindDataFill(SqlConnection connection, CardModel model, TableData tableModel)
        {
            tableModel.Fields.Where(f => f.Type == FieldType.BIND).ForEach(f =>
            {
                var modelFieldValue = model.FieldValues.First(fv => fv.Field.Equals(f));

                if (modelFieldValue.Value != null)
                {
                    modelFieldValue.BindData = GetModelById(connection, modelFieldValue.Field.BindData.Table, modelFieldValue.Value, false, false);
                }
            });
        }
        
        private CardModel GetModelById(SqlConnection connection, TableData tableModel, object id, bool includeBindData = true, bool includeLinkedData = true)
        {
            if (id == null)
                return null;

            var model = CardModel.CreateFromTable(tableModel);
            var fieldId = tableModel.IdentifierField;
            var query = $"SELECT * FROM [{tableModel.Name}] WHERE [{fieldId.Name}] = @{fieldId.Name}";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter(fieldId.Name, id));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ModelFieldsFill(model, tableModel, reader);
                    }
                    else
                    {
                        MessageBox.Show($"Запись с идентификатором {id} в таблице {tableModel.Name} ({tableModel.DisplayName}) не найдена", "Ошибка результата");
                        return null;
                    }
                }
            }

            // Получаем связанные значения
            if (includeBindData)
            {
                ModelBindDataFill(connection, model, tableModel);
            }

            // Получаем внешние данные
            if (includeLinkedData)
            {
                tableModel.LinkedTables.ForEach(lt =>
                {
                    var linkedItem = model.LinkedValues.Find(lv => lv.Table.Equals(lt));
                    var queryLinkedItem = $"SELECT * FROM [{lt.Table.Name}] WHERE [{lt.Field.Name}] = @{lt.Field.Name}";

                    using (var command = new SqlCommand(queryLinkedItem, connection))
                    {
                        command.Parameters.Add(new SqlParameter(lt.Field.Name, id));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var innerModel = CardModel.CreateFromTable(lt.Table);
                                
                                ModelFieldsFill(innerModel, lt.Table, reader);

                                linkedItem.Items.Add(innerModel);
                            }
                        }
                    }

                    linkedItem.Items.ForEach(innerModel => ModelBindDataFill(connection, innerModel, lt.Table));
                });

                // Так как получены внешние данные, то эта запись не заглушка
                model.IsEmpty = false;
            }

            model.ResetStates();
            return model;
        }

        public void InitializeModel(object id = null)
        {
            IsNew = id == null;

            if (IsNew)
            {
                var model = CardModel.CreateFromTable(Table);
                model.IsEmpty = false; // Для новой записи будем считать что она "Полная" а не "Пустая"
                modelCardView1.Model = model;
            }
            else
            {
                using (var dbc = new SQLServerConnection(Base))
                {
                    var model = GetModelById(dbc.Connection, Table, id);

                    if (model != null)
                    {
                        modelCardView1.Model = model;
                        UpdateUiText(id);
                    }
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

        private object SaveModel(SqlConnection connection, SqlTransaction transaction, TableData table, CardModel model)
        {
            if (model.LinkedState == ModelLinkedItemState.UNCHANGED)
                return null;

            object id = model.ID.Value;
            var fieldId = table.IdentifierField;
            var changedFields = model.FieldValues.Where(fv => fv.State == ModelValueState.CHANGED).Select(fv => fv.Field).ToArray();
            
            switch (model.LinkedState)
            {
                case ModelLinkedItemState.ADDED:
                    // Нельзя добавить совсем пустую запись
                    if (changedFields.Length == 0)
                    {
                        NotificationMessage.SystemError("Нельзя добавить запись с пустыми полями.");
                        return id;
                    }

                    var sqlFields = string.Join(", ", changedFields.Select(f => $"[{f.Name}]").ToArray());
                    var sqlValues = string.Join(", ", changedFields.Select(f => $"@{f.Name}").ToArray());
                    var sqlNewItem = $"INSERT INTO [{table.Name}]({sqlFields}) VALUES({sqlValues}); SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(sqlNewItem, connection, transaction))
                    {
                        changedFields.ForEach(f => command.Parameters.AddWithValue(f.Name, model[f] ?? DBNull.Value));
                        id = command.ExecuteScalar();
                    }
                    break;

                case ModelLinkedItemState.CHANGED:
                    // Если нечего изменять - пропускаем
                    if (changedFields.Length == 0)
                        break;

                    var sqlSet = string.Join(", ", changedFields.Select(f => $"[{f.Name}] = @{f.Name}").ToArray());
                    var sqlUpdateItem = $"UPDATE [{table.Name}] SET {sqlSet} WHERE [{fieldId.Name}] = @{fieldId.Name};";

                    using (var command = new SqlCommand(sqlUpdateItem, connection, transaction))
                    {
                        command.Parameters.AddWithValue(fieldId.Name, id);
                        changedFields.ForEach(f => command.Parameters.AddWithValue(f.Name, model[f] ?? DBNull.Value));
                        command.ExecuteNonQuery();
                    }
                    break;

                case ModelLinkedItemState.DELETED:
                    // Если запись не создана, то её нет смысла удалять
                    if (id == null)
                        break;

                    var sqlDeleteItem = $"DELETE FROM [{table.Name}] WHERE [{fieldId.Name}] = @{fieldId.Name};";

                    using (var command = new SqlCommand(sqlDeleteItem, connection, transaction))
                    {
                        command.Parameters.AddWithValue(fieldId.Name, id);
                        command.ExecuteNonQuery();
                    }
                    break;
            }

            model.ID.Value = id;

            model.LinkedValues.Where(lv => lv.State != ModelValueState.UNCHANGED).ForEach(lv =>
            {
                lv.Items.Where(item => item.LinkedState != ModelLinkedItemState.UNCHANGED).ForEach(item =>
                {
                    item[lv.Table.Field] = id;
                    SaveModel(connection, transaction, lv.Table.Table, item);
                });
            });

            return id;
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

            var model = modelCardView1.Model;
            if (model.LinkedState == ModelLinkedItemState.UNCHANGED)
            {
                NotificationMessage.Info("Изменений нет. Сохранение не требуется.");
                return;
            }
            
            object id = null;
            
            // Make SQL request
            using (var dbc = new SQLServerConnection(Base))
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    model.LinkedState = IsNew ? ModelLinkedItemState.ADDED : ModelLinkedItemState.CHANGED;
                    id = SaveModel(connection, transaction, Table, model);
                    
                    transaction.Commit();
                    
                    model.ResetStates();
                    IsNew = false;

                    NotificationMessage.Info("Сохранено!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error($"Ошибка при сохранении:\r\n\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }

                transaction.Dispose();
            }

            UpdateUiText(id);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (IsLinkedModel)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            this.Close();
        }

        private void UpdateUiText(object id)
        {
            txtID.Text = id?.ToString();
            this.Text = IsNew ? "Новая запись" : "Изменение";
        }

        private void FormCardView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }
    }
}
