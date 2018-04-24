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
        public static readonly string FileAssociationFilter = $"{FileAssociationDescription}|*{FileAssociationExtension}";

        // Table Storage
        public static readonly string TableStorageFolder = Path.Combine(UserSettingsFolder, "Tables");

        // Filter Storage
        public static readonly string FilterStorageFolder = Path.Combine(UserSettingsFolder, "Filters");

        // Documents Storage
        public static readonly string DocumentStorageFolder = Path.Combine(UserSettingsFolder, "Documents");
    }
}