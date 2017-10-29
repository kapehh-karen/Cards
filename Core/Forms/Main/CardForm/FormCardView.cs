using Core.Connection;
using Core.Data.Base;
using Core.Data.Design.InternalData;
using Core.Data.Field;
using Core.Data.Model;
using Core.Data.Table;
using Core.Helper;
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
        
        public void InitializeModel(object id = null)
        {
            var model = CardModel.CreateFromTable(table);
            var fieldId = Table.IdentifierField;

            if (id != null)
            {
                // Make SQL request
                using (var dbc = WaitDialog.Run("Подождите, идет подключение к SQL Server", () => new SQLServerConnection(Base)))
                {   
                    var query = $"SELECT * FROM {Table.Name} WHERE {fieldId.Name} = @Id";
                    var connection = dbc.Connection;

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter(fieldId.Name, id));

                        SqlDataReader reader = null;
                        var readed = WaitDialog.Run("Получение данных", () =>
                        {
                            reader = command.ExecuteReader();
                            return reader.Read();
                        });

                        if (readed)
                            Table.Fields.ForEach(f => model[f.Name] = FieldHelper.CastValue(f, reader[f.Name]));
                    }
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
            // TODO: Save model to bd
            model.ResetStates();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
