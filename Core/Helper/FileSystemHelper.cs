using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Helper
{
    public static class FileSystemHelper
    {
        public static IEnumerable<string> GetFilesFromFolder(string directoryName, string extensionName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            
            foreach (var fileName in Directory.GetFiles(directoryName)
                                              .Where(fname =>
                                                Path.GetExtension(fname).Equals($".{extensionName}", StringComparison.CurrentCultureIgnoreCase)))
            {
                yield return fileName;
            }
        }
    }
}
