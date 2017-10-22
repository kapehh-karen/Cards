using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public enum ModelLinkedItemState
    {
        UNCHANGED = 0,
        CHANGED = 1,
        ADDED = 2,
        DELETED = 3
    }
}
