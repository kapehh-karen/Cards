using System;
using System.IO;

namespace Core
{
    public static class Consts
    {
        public const string ProgramTitle = ".:: CARDS ::.";

        public static readonly string UserSettingsFolder
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CARDS User Settings");

        // File Association

        public static readonly string FileAssociationExtension = ".cards";

        public static readonly string FileAssociationDescription = "Файл настроек CARDS";

        public static readonly string FileAssociationRegKey = "CARDSfile";

        // Table Storage

        public static readonly string TableStorageFolder = Path.Combine(UserSettingsFolder, "Tables");
    }
}