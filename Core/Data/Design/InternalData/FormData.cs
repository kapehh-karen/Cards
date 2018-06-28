using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.Data.Design.InternalData
{
    public class FormData : ICloneable
    {
        public Size Size { get; set; }

        public List<PageData> Pages { get; set; } = new List<PageData>();

        public object Clone()
        {
            return new FormData()
            {
                Size = this.Size,
                Pages = this.Pages.Select(it => it.Clone()).Cast<PageData>().ToList()
            };
        }
    }
}
