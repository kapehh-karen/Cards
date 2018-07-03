using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Table;
using Core.Common.DataGrid;
using Core.Storage.Tables;
using Core.Helper;
using Core.Forms.Main;

namespace Core.Data.Design.Controls.LinkedTableControl
{
    public class LinkedTableControl : BaseDataGridView
    {
        public event EventHandler TableStorageInformationUpdated = (s, e) => { };

        public LinkedTableControl() : base()
        {
            Properties.Add(new NameProperty(this));
            Properties.Add(new TextProperty(this));
            Properties.Add(new SizeProperty(this));
            Properties.Add(new FontProperty(this));
            Properties.Add(new PositionProperty(this));
            Properties.Add(new LinkedTableProperty(this));
            Properties.Add(new TabIndexProperty(this));

            BorderStyle = BorderStyle.FixedSingle;
            MultiSelect = false;
            DefaultCellStyle.SelectionBackColor = Color.White;
            DefaultCellStyle.SelectionForeColor = Color.Black;
            AlternatingRowsDefaultCellStyle.BackColor = RowsDefaultCellStyle.BackColor;
        }

        #region IDesignControl Impl

        public override DesignControlType ControlType => DesignControlType.LINKED_TABLE;
        public override List<IControlProperty> Properties { get; } = new List<IControlProperty>();
        public override List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        #endregion

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        #region Table Storage Information

        public override TableStorageType TableStorageType { get; set; } = TableStorageType.LinkedTable;

        protected override bool InitializeFields()
        {
            base.Table.Fields.ForEach(TableStorageInformation.AddColumn);
            return true;
        }

        protected override void OnTableStorageInformationUpdated()
        {
            TableStorageInformationUpdated(this, null);
        }
        
        #endregion
    }
}
