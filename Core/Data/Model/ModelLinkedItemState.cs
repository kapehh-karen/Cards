using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Model
{
    public enum ModelLinkedItemState
    {
        UNKNOWN = 0,
        UNCHANGED = 1,
        CHANGED = 2,
        ADDED = 3,
        DELETED = 4
    }
}
