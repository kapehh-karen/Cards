using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class Consts
    {
        public static readonly string DirectoryBase = "BASE";

        public static readonly string ConfigBaseExtension = "cards";

        // File Association

        public static readonly string FileAssociationExtension = "." + ConfigBaseExtension;

        public static readonly string FileAssociationDescription = "Файл настроек CARDS";

        public static readonly string FileAssociationRegKey = "CARDSfile";
    }
}
