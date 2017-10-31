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

        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                modelCardView1.Table = table;

                if (table?.Form != null)
                {
                    this.Size = new Size(table.Form.Size.Width + 15, table.Form.Size.Height + 80);
                    modelCardView1.Size = table.Form.Size;
                    modelCardView1.Form = table.Form;
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
            }

            model.ResetStates();
            return model;
        }

        public void InitializeModel(object id = null)
        {
            IsNew = id == null;

            if (IsNew)
            {
                modelCardView1.Model = CardModel.CreateFromTable(Table);
            }
            else
            {
                using (var dbc = new SQLServerConnection(Base))
                {
                    var model = GetModelById(dbc.Connection, Table, id);

                    if (model != null)
                    {
                        modelCardView1.Model = model;
                        txtID.Text = id.ToString(); // Просто для отображения, если запись найдена
                    }
                }
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
            var model = modelCardView1.Model;
            var fieldId = Table.IdentifierField;
            object id = model[fieldId];

            if (model.State == ModelValueState.UNCHANGED)
                return;

            // Make SQL request
            using (var dbc = new SQLServerConnection(Base))
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();

                try
                {
                    var changedFields = model.FieldValues.Where(fv => fv.State == ModelValueState.CHANGED).Select(fv => fv.Field).ToArray();

                    if (IsNew)
                    {
                        var sqlFields = string.Join(", ", changedFields.Select(f => $"[{f.Name}]").ToArray());
                        var sqlValues = string.Join(", ", changedFields.Select(f => $"@{f.Name}").ToArray());
                        var sqlNewItem = $"INSERT INTO [{Table.Name}]({sqlFields}) VALUES({sqlValues}); SELECT SCOPE_IDENTITY();";
                        
                        using (var command = new SqlCommand(sqlNewItem, connection, transaction))
                        {
                            changedFields.ForEach(f => command.Parameters.AddWithValue(f.Name, model[f]));
                            id = command.ExecuteScalar();
                        }
                    }
                    else
                    {
                        var sqlSet = string.Join(", ", changedFields.Select(f => $"[{f.Name}] = @{f.Name}").ToArray());
                        var sqlUpdateItem = $"UPDATE [{Table.Name}] SET {sqlSet} WHERE [{fieldId.Name}] = @{fieldId.Name};";

                        using (var command = new SqlCommand(sqlUpdateItem, connection, transaction))
                        {
                            command.Parameters.AddWithValue(fieldId.Name, model[fieldId]);
                            changedFields.ForEach(f => command.Parameters.AddWithValue(f.Name, model[f]));
                            command.ExecuteNonQuery();
                        }
                    }

                    // TODO: Insert/Update linked data
                    
                    transaction.Commit();

                    model[fieldId] = id; // Присваиваю тут, убедившись что commit транзакции успешен, раньше присваивать нельзя
                    IsNew = false;

                    model.ResetStates();
                    txtID.Text = id?.ToString(); // Просто для отображения, если запись добавлена

                    NotificationMessage.Info("Сохранено!");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error($"Ошибка при сохранении:\r\n\r\n{ex.Message}");
                }

                transaction.Dispose();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
