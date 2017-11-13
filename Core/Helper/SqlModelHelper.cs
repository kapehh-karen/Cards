using Core.Data.Model;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class SqlModelHelper
    {
        public static void DeleteFull(SqlConnection connection, SqlTransaction transaction, TableData table, object id)
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

                ids.ForEach(id_child => DeleteFull(connection, transaction, linkedTable.Table, id_child));
            });

            // Удаляю саму запись
            var sqlDeleteItem = $"DELETE FROM [{table.Name}] WHERE [{table.IdentifierField.Name}] = @id;";
            using (var command = new SqlCommand(sqlDeleteItem, connection, transaction))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
