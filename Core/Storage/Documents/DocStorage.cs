﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Storage.Documents
{
    public class DocStorage
    {
        public static readonly DocStorage Instance = new DocStorage();

        private DocStorage()
        {
        }

        private void CheckDirectory()
        {
            if (!Directory.Exists(Consts.DocumentStorageFolder))
            {
                Directory.CreateDirectory(Consts.DocumentStorageFolder);
            }
        }

        public void OpenDocumentsFolder()
        {
            CheckDirectory();
            Process.Start(Consts.DocumentStorageFolder);
        }

        public string GenerateFileName(string title, string extension)
        {
            CheckDirectory();
            return Path.Combine(Consts.DocumentStorageFolder, $"{title} {DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")}.{extension}");
        }
    }
}
