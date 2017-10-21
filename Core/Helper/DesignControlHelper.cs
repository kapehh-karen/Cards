using Core.Data.Design.Controls;
using Core.Data.Design.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class DesignControlHelper
    {
        public static IControlProperty GetProperty<T>(this IDesignControl control)
        {
            return control.Properties.FirstOrDefault(p => p is T);
        }
    }
}
