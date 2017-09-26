using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;

namespace TestApp
{
    public partial class frmSerializer : Form
    {
        public frmSerializer()
        {
            InitializeComponent();
        }

        public enum Genre
        {
            Male,
            Female
        }

        [DataContract(IsReference = true)]
        public class PERSON
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public Genre Genre { get; set; }

            public List<PERSON> Parents { get; set; }
            public List<PERSON> Children { get; set; }

            public PERSON(string name, Genre genre)
            {
                this.Name = name;
                this.Genre = genre;
                Parents = new List<PERSON>();
                Children = new List<PERSON>();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myFamily = new List<PERSON>();
            var k = new PERSON("Obshee", Genre.Female);
            var a = new PERSON("Femma", Genre.Female);
            var b = new PERSON("Malli", Genre.Male);
            a.Children.Add(b);
            b.Children.Add(k);
            k.Children.Add(a);
            myFamily.Add(a);

            var serializer = new DataContractSerializer(myFamily.GetType());

            using (FileStream stream = File.Create(@"test.xml"))
            {
                serializer.WriteObject(stream, myFamily);
            }

            using (FileStream stream = File.OpenRead(@"test.xml"))
            {
                List<PERSON> data = (List<PERSON>)serializer.ReadObject(stream);
            }
        }
    }
}
