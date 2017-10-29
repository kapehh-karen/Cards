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
        
        public void InitializeModel(object id = null)
        {
            var model = CardModel.CreateFromTable(table);
            var fieldId = Table.IdentifierField;

            IsNew = id == null;

            if (id != null)
            {
                // Make SQL request
                using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server", () => new SQLServerConnection(Base)))
                {   
                    var query = $"SELECT * FROM [{Table.Name}] WHERE [{fieldId.Name}] = @{fieldId.Name}";
                    var connection = dbc.Connection;

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter(fieldId.Name, id));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Table.Fields.ForEach(f => model[f.Name] = FieldHelper.CastValue(f, reader[f.Name]));
                                
                                txtID.Text = id.ToString(); // Просто для отображения, если запись найдена
                            }
                            else
                            {
                                MessageBox.Show($"Запись с идентификатором {id} не найдена", "Запись не найдена");
                            }
                        }
                    }
                    
                    // TODO: Помимо всего прочего, получать и связанные строки, и внешние данные
                }
                
                model.ResetStates();
            }

            modelCardView1.Model = model;
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

            if (model.State == ModelValueState.UNCHANGED)
                return;

            // Make SQL request
            using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server", () => new SQLServerConnection(Base)))
            {
                var connection = dbc.Connection;
                var transaction = connection.BeginTransaction();
                object id = model[fieldId];

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

                    model[fieldId] = id;
                    txtID.Text = id?.ToString(); // Просто для отображения, если запись добавлена
                    IsNew = false;
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error(ex.Message, ex);
                }

                transaction.Dispose();
            }

            model.ResetStates();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
