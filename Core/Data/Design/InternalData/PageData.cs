using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class PageData
    {
        public string Title { get; set; }

        public List<ControlData> Controls { get; set; } = new List<ControlData>();
    }
}
