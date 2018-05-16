using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Utils
{
    public static class ArgumentsUtil
    {
        public static bool GetCardsFileName(string[] args, out string fileName)
        {
            if (args.Length > 0)
            {
                fileName = args[0];
                return true;
            }

            using (OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Выберите файл конфигурации",
                Filter = Consts.FileAssociationFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    return true;
                }
            }

            fileName = null;
            return false;
        }
    }
}
