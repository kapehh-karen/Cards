using Core.Connection;
using Core.Data.Field;
using Core.Data.Table;
using Core.GroupEdit.Controls;
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

namespace Core.GroupEdit.Forms
{
    public partial class FormGroupEdit : Form
    {
        private class ListBoxFieldItem
        {
            public FieldData Field { get; set; }
            public override string ToString() => Field.DisplayName;
        }

        public FormGroupEdit()
        {
            InitializeComponent();
        }

        private TableData table = null;
        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                if (table == null)
                    return;

                table.Fields.Where(field => !field.IsIdentifier)
                    .ForEach(field => lstFields.Items.Add(new ListBoxFieldItem() { Field = field }));
                lstFields.Sorted = true;
            }
        }

        public ICollection<object> SelectedIDs { get; set; }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            lstFields.SelectedItems
                .Cast<ListBoxFieldItem>()
                .ToArray()
                .ForEach(item =>
                {
                    var control = new ItemFieldValue() { Field = item.Field };
                    control.ItemDelete += Control_ItemDelete;
                    panelContainer.Controls.Add(control);
                    lstFields.Items.Remove(item);
                });
        }

        private void Control_ItemDelete(object sender, EventArgs e)
        {
            var control = sender as ItemFieldValue;
            var item = new ListBoxFieldItem() { Field = control.Field };
            lstFields.Items.Add(item);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (SelectedIDs.Count < 1)
            {
                NotificationMessage.Error("Отсутствуют выбранные поля в таблице. Необходимо закрыть текущее окно и выбрать записи для редактирования.");
                return;
            }

            var newValues = panelContainer.Controls
                .Cast<ItemFieldValue>()
                .ToDictionary(control => control.Field, control => control.Value);

            if (newValues.Count > 0)
            {
                if (MessageBox.Show("Вы уверены?\nДанная операция не обратима!",
                        "Подтверждение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    return;

                var updated = WaitDialog.Run("Выполняется операция замены.", (s) => DoUpdate(newValues));
                if (updated > 0)
                {
                    NotificationMessage.Info($"Готово! Количество обработанных записей: {updated}");
                    DialogResult = DialogResult.OK;
                }
                else
                    NotificationMessage.Warning("Обновлено 0 записей. Возможно произошла ошибка.");
            }
            else
                NotificationMessage.Error("Требуется добавить хотя бы одну замену для поля.");
        }

        private int DoUpdate(Dictionary<FieldData, object> values)
        {
            var updated = 0;
            using (var sqlCon = new SQLServerConnection())
            {
                var connenction = sqlCon.Connection;
                var transaction = connenction.BeginTransaction();
                try
                {
                    var strSet = string.Join(", ", values.Select(item => $"[{item.Key.Name}] = @var_{item.Key.Name}"));
                    var strSql = $"UPDATE [{Table.Name}] SET {strSet} WHERE [{Table.IdentifierField.Name}] = @row_id";
                    using (var command = new SqlCommand(strSql, connenction, transaction))
                    {
                        // Добавляем значения для полей
                        command.Parameters.AddWithValue("@row_id", 0);
                        values.ForEach(item => command.Parameters.AddWithValue($"@var_{item.Key.Name}", item.Value ?? DBNull.Value));
                        // Выполняем замену для каждой выделенной записи
                        SelectedIDs.ForEach(id =>
                        {
                            command.Parameters["@row_id"].Value = id;
                            updated += command.ExecuteNonQuery();
                        });
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    NotificationMessage.Error($"Ошибка при замене:\r\n\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }
                transaction.Dispose();
            }
            return updated;
        }
    }
}
