using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Forms.Main
{
    public class TableRowIndex
    {
        public static TableRowIndex Create(TableDataGridView dataGridView)
        {
            if (dataGridView == null || dataGridView.CurrentRow == null)
            {
                return null;
            }
            else
            {
                var tableIndex = new TableRowIndex
                {
                    DataGridView = dataGridView
                };
                tableIndex.RecalculateTableIndex(dataGridView.CurrentRow.Index, dataGridView.SelectedID);
                return tableIndex;
            }
        }

        private TableRowIndex() { }

        private TableDataGridView DataGridView { get; set; }

        public int? CurrentIndex { get; set; }

        public object CurrentID { get; set; }

        public int? BackIndex { get; set; }

        public int? NextIndex { get; set; }

        private void RecalculateTableIndex(int index, object id)
        {
            CurrentID = id;
            CurrentIndex = index;
            BackIndex = (index - 1) >= 0 ? (int?)(index - 1) : null;
            NextIndex = (index + 1) < DataGridView.CurrentDataView.Count ? (int?)(index + 1) : null;
        }

        private bool GoTo(int index)
        {
            if (index >= 0 && index < DataGridView.CurrentDataView.Count)
            {
                var id = DataGridView.Rows[index].Cells[DataGridView.FieldID.Name].Value;
                RecalculateTableIndex(index, id);
                return true;
            }
            return false;
        }

        public bool GoBack()
        {
            if (BackIndex.HasValue)
            {
                return GoTo(BackIndex.Value);
            }
            return false;
        }

        public bool GoNext()
        {
            if (NextIndex.HasValue)
            {
                return GoTo(NextIndex.Value);
            }
            return false;
        }
    }
}
