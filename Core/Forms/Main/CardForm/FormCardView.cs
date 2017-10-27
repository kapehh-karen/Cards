using Core.Data.Base;
using Core.Data.Design.InternalData;
using Core.Data.Model;
using Core.Data.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Forms.Main.CardForm
{
    public partial class FormCardView : Form
    {
        private TableData table;
        private DataBase mainBase;

        public TableData Table
        {
            get => table;
            set
            {
                table = value;
                modelCardView1.Table = table;

                if (table?.Form != null)
                {
                    this.Size = new Size(table.Form.Size.Width + 15, table.Form.Size.Height + 80);
                    modelCardView1.Size = table.Form.Size;
                    modelCardView1.Form = table.Form;
                }
            }
        }

        public DataBase Base
        {
            get => mainBase;
            set
            {
                mainBase = value;
                modelCardView1.Base = mainBase;
            }
        }
        
        public void InitializeModel()
        {
            modelCardView1.Model = CardModel.CreateFromTable(table);
        }

        public FormCardView()
        {
            InitializeComponent();
        }

        private void FormCardView_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var model = modelCardView1.Model;
            model.ResetStates();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            /*var model = modelCardView1.Model;
            model["username"] = "Keked";
            modelCardView1.UpdateElements();*/

            this.Close();
        }
    }
}
