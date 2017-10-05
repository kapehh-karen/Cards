using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Design.Controls
{
    public interface IDesignContainer
    {
        List<IDesignControl> DesignControls { get; set; }
    }
}
