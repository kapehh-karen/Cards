using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Data.Design.Properties;
using Core.Data.Design.Properties.ControlProperties;
using Core.Data.Table;
using Core.Common;

namespace Core.Data.Design.Controls.LinkedTableControl
{
    // TODO: Добавление, изменение и удаление записей можно сделать на клавиши и на контекстное меню
    public class LinkedTableControl : BaseDataGridView, IDesignControl
    {
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
            DefaultColor = BackgroundColor;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Override for red color in FormDesigner
        public override Color BackColor { get => base.BackgroundColor; set => base.BackgroundColor = value; }

        public DesignControlType ControlType => DesignControlType.LINKED_TABLE;

        public List<IControlProperty> Properties { get; set; } = new List<IControlProperty>();

        public List<IDesignControl> DesignControls { get; set; } = new List<IDesignControl>();

        public IDesignControl ParentControl { get; set; }

        public Color DefaultColor { get; set; }

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
    }
}
