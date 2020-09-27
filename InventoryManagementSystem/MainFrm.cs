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
    public partial class MainFrm : Form
    {
        public int loggedIn { get; set; }
        public int UserID { get; set; }

        public MainFrm()
        {
            InitializeComponent();
            loggedIn = 0;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainFrm_Activated(object sender, EventArgs e)
        {
            if (loggedIn == 0)
            {
                //Open Login Form
                LoginFrm newLogin = new LoginFrm();
                newLogin.ShowDialog();

                if (newLogin.loginFlag == false)
                {
                    Close();
                }
                else
                {
                    UserID = newLogin.UserID;
                    statLblUser.Text = UserID.ToString();
                    loggedIn = 1;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAddCatagory addcatagory = new FormAddCatagory();
            addcatagory.UserID = this.UserID;
            addcatagory.ShowDialog();
        }

        private void MainFrm_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.CatagoryStatsTBL' table. You can move, or remove it, as needed.
            this.catagoryStatsTBLTableAdapter.Fill(this.dataSet1.CatagoryStatsTBL);
            // TODO: This line of code loads data into the 'dataSet1.CatagoriesTBL' table. You can move, or remove it, as needed.
            this.catagoriesTBLTableAdapter.Fill(this.dataSet1.CatagoriesTBL);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductFrm product = new ProductFrm();
            product.CatagoryName = comboBox1.Text;
            product.CatagoryID = (int)comboBox1.SelectedValue;
            product.ShowDialog();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.CatagoryStatsTBLTableAdapter ada = new DataSet1TableAdapters.CatagoryStatsTBLTableAdapter();
            DataTable dt = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);

            if (dt.Rows.Count >= 0)
            {
                //we have stats, so we can edit
                DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;
            }
            else
            {
                //create a status for rach product
                //get the catagory product list
                DataSet1TableAdapters.ProductTableTableAdapter product_adapter = new DataSet1TableAdapters.ProductTableTableAdapter();

                DataTable dt_Product = product_adapter.GetDataBy1CatagoryID((int)comboBox1.SelectedValue);
                foreach (DataRow row in dt_Product.Rows)
                {
                    
                    //Insert a new status for the product
                    ada.InsertQuery((int)row[0], (int)comboBox1.SelectedValue, dateTimePicker1.Text, "", row[1].ToString(), comboBox1.Text);
                    

                }

                DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.CatagoryStatsTBLTableAdapter ada = new DataSet1TableAdapters.CatagoryStatsTBLTableAdapter();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    ada.UpdateQuery(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), (int)comboBox1.SelectedValue, dateTimePicker1.Text);

                }

            }

            DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
            dataGridView1.DataSource = dt_new;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.ProductTableTableAdapter product_adapter = new DataSet1TableAdapters.ProductTableTableAdapter();

            DataTable dt_Product = product_adapter.GetDataBy1CatagoryID((int)comboBox2.SelectedValue);

            DataSet1TableAdapters.CatagoryStatsTBLTableAdapter ada = new DataSet1TableAdapters.CatagoryStatsTBLTableAdapter();

            int I = 0;
            int O = 0;

            foreach (DataRow row in dt_Product.Rows)
            {
 
                I = (int)ada.GetDataByStatus(dateTimePicker2.Value.Month, row[1].ToString(), "InStock").Rows[0][6];

                O = (int)ada.GetDataByStatus(dateTimePicker2.Value.Month, row[1].ToString(), "OutofStock").Rows[0][6];

                ListViewItem Litem = new ListViewItem();
                Litem.Text = row[1].ToString();
                Litem.SubItems.Add(I.ToString());
                Litem.SubItems.Add(O.ToString());
                listView1.Items.Add(Litem);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            reg.ShowDialog();
        }

    }
}
