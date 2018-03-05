using System;
using System.IO;
using System.Windows.Forms;

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
            File.Create(@"C:\CARDS\res\file.txt");

            /*using (var dialog = new OpenFileDialog())
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
            }*/
        }
    }
}
