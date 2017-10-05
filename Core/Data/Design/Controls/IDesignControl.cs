using Core.Data.Design.Properties;
using Core.Data.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Data.Design.Controls
{
    public interface IDesignControl : IDesignContainer
    {
        /// <summary>
        /// Список пропертей
        /// </summary>
        List<IControlProperties> Properties { get; }
        
        /// <summary>
        /// Тип виртуального контрола (просто элементы, либо связаны с полями БД)
        /// </summary>
        DesignControlType ControlType { get; }
    }
}
