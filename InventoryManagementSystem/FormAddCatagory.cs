using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FormAddCatagory : Form
    {
        public int UserID { get; set; }
        public FormAddCatagory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.CatagoriesTBLTableAdapter ada = new DataSet1TableAdapters.CatagoriesTBLTableAdapter();
            ada.AddCatagory(textBox1.Text, UserID);
            Close();

        }
    }
}
