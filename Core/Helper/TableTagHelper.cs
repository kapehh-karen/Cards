using Core.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Helper
{
    public static class TableTagHelper
    {
        public static TableColumnTag GetTag(this DataGridViewColumn column)
        {
            var tag = column.Tag;

            if ((tag == null) || !(tag is TableColumnTag))
            {
                tag = column.Tag = new TableColumnTag();
            }

            return tag as TableColumnTag;
        }

        public static TableRowTag GetTag(this DataGridViewRow row)
        {
            var tag = row.Tag;

            if ((tag == null) || !(tag is TableRowTag))
            {
                tag = row.Tag = new TableRowTag();
            }

            return tag as TableRowTag;
        }
    }
}
