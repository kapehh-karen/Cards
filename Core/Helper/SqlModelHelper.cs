using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class SqlModelHelper
    {
        // DELETE

        public static void Delete(SqlConnection connection, SqlTransaction transaction, TableData table, object id)
        {
            // Удаляем все связанные данные
            table.LinkedTables.ForEach(linkedTable =>
            {
                var fieldId = linkedTable.Table.IdentifierField;
                var queryLinkedItem = $"SELECT [{fieldId.Name}] FROM [{linkedTable.Table.Name}] WHERE [{linkedTable.Field.Name}] = @id_parent";
                var ids = new List<object>();

                using (var command = new SqlCommand(queryLinkedItem, connection, transaction))
                {
                    command.Parameters.AddWithValue("id_parent", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader[fieldId.Name]);
                        }
                    }
                }

                ids.ForEach(id_child => Delete(connection, transaction, linkedTable.Table, id_child));
            });

            // Удаляю саму запись
            var sqlDeleteItem = $"DELETE FROM [{table.Name}] WHERE [{table.IdentifierField.Name}] = @id;";
            using (var command = new SqlCommand(sqlDeleteItem, connection, transaction))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
        }

        // SAVE

        public static object Save(SqlConnection connection, SqlTransaction transaction, TableData table, CardModel model)
        {
            if (model.LinkedState == ModelLinkedItemState.UNCHANGED)
                return null;

            var modelId = model.ID;
            object id = modelId.OldValue; // Т.к. идентификатор меняемое значение, то берем "старое" значение
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
                        // Если небыло возвращено идентификатора, то он должен быть уже в modelId.Value
                        if (id != DBNull.Value)
                            modelId.Value = id;
                    }
                    break;

                case ModelLinkedItemState.CHANGED:
                    // Если нечего изменять - пропускаем
                    if (changedFields.Length == 0)
                        break;

                    var sqlSet = string.Join(", ", changedFields.Select(f => $"[{f.Name}] = @value_{f.Name}").ToArray());
                    var sqlUpdateItem = $"UPDATE [{table.Name}] SET {sqlSet} WHERE [{fieldId.Name}] = @id_{fieldId.Name};";

                    using (var command = new SqlCommand(sqlUpdateItem, connection, transaction))
                    {
                        command.Parameters.AddWithValue($"@id_{fieldId.Name}", id);
                        changedFields.ForEach(f => command.Parameters.AddWithValue($"@value_{f.Name}", model[f] ?? DBNull.Value));
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

            model.LinkedValues.ForEach(linkedValue =>
            {
                linkedValue.Items.ForEach(item =>
                {
                    // В Foreign Key записываем значение ID (если менялось, тоже учитывается, в Value будет новый ID)
                    item[linkedValue.LinkedTable.Field] = modelId.Value;
                    Save(connection, transaction, linkedValue.LinkedTable.Table, item);
                });
            });

            // Возвращаем ID (текущий/измененный)
            return modelId.Value;
        }

        // GET

        private static void ModelFieldsFill(CardModel model, TableData tableModel, SqlDataReader reader)
        {
            tableModel.Fields.ForEach(f => model[f.Name] = FieldHelper.CastValueForField(f, reader[f.Name]));
        }

        private static void ModelBindDataFill(SqlConnection connection, CardModel model, TableData tableModel)
        {
            tableModel.Fields.Where(f => f.Type == FieldType.BIND).ForEach(f =>
            {
                var modelFieldValue = model.GetModelField(f);

                if (modelFieldValue.Value != null)
                {
                    modelFieldValue.BindData = GetById(connection, modelFieldValue.Field.BindData.Table, modelFieldValue.Value, false, false);
                }
            });
        }

        public static CardModel GetById(SqlConnection connection, TableData tableModel, object id, bool includeBindData = true, bool includeLinkedData = true)
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
                        NotificationMessage.SystemError($"Запись с идентификатором \"{id}\" в таблице \"{tableModel.Name}\" ({tableModel.DisplayName}) не найдена", "Ошибка результата");
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
                    var linkedItem = model.LinkedValues.Find(lv => lv.LinkedTable.Equals(lt));
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
    }
}
