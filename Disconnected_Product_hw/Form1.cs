using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Disconnected_Product_hw
{

    public partial class Form1 : Form
    {

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        public DataSet GetAllEmp()
        {
            da = new SqlDataAdapter("select * from Prod1", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Prod1");// Prod is a table name given to DataTable
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Prod1"].NewRow();
                row["name"] = txtName.Text;
                row["price"] = txtPrice.Text;
                row["company"] = txtCompanyname.Text;
                ds.Tables["Prod1"].Rows.Add(row);
                int result = da.Update(ds.Tables["Prod1"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Prod1"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["price"] = txtPrice.Text;
                    row["company"] = txtCompanyname.Text;

                    int result = da.Update(ds.Tables["Prod1"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["Prod1"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Prod1"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnshowall_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                dataGridView1.DataSource = ds.Tables["Prod1"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
