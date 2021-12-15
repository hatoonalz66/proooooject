using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace proooooject
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Server=.; DataBase=EmpInfo;Initial Catalog= EmpInfo ; Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable Dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                cmd = new SqlCommand("insert into emp(DeptNO,Name,job) values(@DeptNo,@Name,@job)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@DeptNo", textBox1.Text);

                // cmd.Parameters.AddWithValue("@empNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox3.Text);
                cmd.Parameters.AddWithValue("@job", textBox4.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }

        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("select * from emp", con);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }


        //Clear Data
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";

            textBox3.Text = "";
            textBox4.Text = "";


        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                cmd = new SqlCommand("delete emp where empNo=@empNo", con);
                con.Open();
                cmd.Parameters.AddWithValue("@empNo", textBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();

                ClearData();

            }

            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void btn_upadate_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {

                cmd = new SqlCommand("update emp set DeptNo=@DeptNO ,Name=@Name , job=@job ", con);
                con.Open();
                cmd.Parameters.AddWithValue("@DeptNo", textBox2.Text);
                // cmd.Parameters.AddWithValue("@empNo", textBox1dno.Text);
                cmd.Parameters.AddWithValue("@Name", textBox3.Text);
                cmd.Parameters.AddWithValue("@job", textBox4.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");

                con.Close();
                DisplayData();

            }


            else
            {
                MessageBox.Show("Please Select Record to Update");

            }
            ClearData();
            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string empNo = textBox5.Text;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from emp where empNo='" + empNo + "'", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetValue(0).ToString();
                    textBox2.Text = reader.GetValue(1).ToString();
                    textBox3.Text = reader.GetValue(2).ToString();
                    textBox4.Text = reader.GetValue(3).ToString();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("error in searching " + ex);
            }
            finally
            {
                con.Close();
            }
        }
    }
   
}
