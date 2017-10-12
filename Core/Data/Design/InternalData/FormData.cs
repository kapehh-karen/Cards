using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class FormData
    {
        public Size Size { get; set; }

        public List<PageData> Pages { get; set; } = new List<PageData>();
    }
}
