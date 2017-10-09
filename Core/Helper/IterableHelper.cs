using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class IterableHelper
    {
        public static void ForEach<T>(this IEnumerable<T> iter, Action<T> action)
        {
            foreach (var item in iter)
            {
                action(item);
            }
        }
    }
}
