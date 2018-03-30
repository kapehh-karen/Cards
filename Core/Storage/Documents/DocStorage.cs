using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            return Path.Combine(Consts.DocumentStorageFolder, $"{title} {DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")}.{extension}");
        }

        public void OpenDocumentFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"Файла \"{fileName}\" не существует! Открыть невозможно.",
                    Consts.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var shortName = Path.GetFileName(fileName);
            if (MessageBox.Show($"Документ \"{shortName}\" сформирован и сохранен. Просмотреть?",
                Consts.ProgramTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    Process.Start(fileName);
                }
                catch (Win32Exception)
                {
                    Process.Start("explorer", $"/select,\"{fileName}\"");
                }
            }
        }
    }
}
