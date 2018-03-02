using Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TemplateEngine.Docx;

namespace TestApp
{
    public partial class frmTestUserControls : Form
    {
        public frmTestUserControls()
        {
            InitializeComponent();
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = dialog.FileName;

                    var valuesToFill = new Content(new FieldContent("fio", "ПРИВЕТ Пока Здаровович"));

                    var stream = new MemoryStream(File.ReadAllBytes(fileName));

                    using (var outputDocument = new TemplateProcessor(stream)
                        .SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                    }

                    File.WriteAllBytes($"{fileName} GENERATED.docx", stream.ToArray());

                    MessageBox.Show("OK");
                }
            }
        }
    }
}
