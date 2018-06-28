using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class PageData : ICloneable
    {
        public string Title { get; set; }

        public List<ControlData> Controls { get; set; } = new List<ControlData>();

        public object Clone()
        {
            return new PageData()
            {
                Title = this.Title,
                Controls = this.Controls.Select(it => it.Clone()).Cast<ControlData>().ToList()
            };
        }
    }
}
