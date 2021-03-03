using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

namespace MyClassProject_R44
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        string gender;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text !="" && DateTimePicDOB.Text !="")
            {
                using (con = new SqlConnection(cs))
                {
                    cmd = new SqlCommand("INSERT INTO Student(Name,Gender,DOB) VALUES(@name,@gender,@dob)", con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dob", DateTimePicDOB.Text);
                    con.Open();

                    int rowCount = cmd.ExecuteNonQuery();
                    if(rowCount>0)
                    {
                        MessageBox.Show("Data Inserted Successfully!!!");
                        adapter = new SqlDataAdapter("SELECT * FROM Student", con);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dgViewStudent.DataSource = dt;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Provide All Values!!!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblSid.Text !="")
            {
                using (con = new SqlConnection(cs))
                {
                    cmd = new SqlCommand("DELETE FROM Student WHERE StudentID=@sid", con);
                    cmd.Parameters.AddWithValue("@sid", lblSid.Text);
                    con.Open();

                    int rowCount = cmd.ExecuteNonQuery();
                    if (rowCount > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully!!!");
                        adapter = new SqlDataAdapter("SELECT * FROM Student", con);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dgViewStudent.DataSource = dt;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Provide All Values!!!");
            }
            lblSid.Text = "";
            txtName.Text = "";
            DateTimePicDOB.ResetText();
            radBtnFemale.Checked = false;
            radBtnMale.Checked = false;

            btnAdd.Show();
            btnDelete.Hide();
            btnEdit.Hide();
            btnCancel.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text !="" && DateTimePicDOB.Text !="")
            {
                using (con = new SqlConnection(cs))
                {
                    cmd = new SqlCommand("UPDATE Student SET Name=@name,Gender=@gender,DOB=@dob WHERE StudentID=@sid", con);
                    cmd.Parameters.AddWithValue("@sid", lblSid.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dob", DateTimePicDOB.Text);
                    con.Open();

                    int rowCount = cmd.ExecuteNonQuery();
                    if (rowCount > 0)
                    {
                        MessageBox.Show("Data Updated Successfully!!!");
                        adapter = new SqlDataAdapter("SELECT * FROM Student", con);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dgViewStudent.DataSource = dt;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Provide All Values!!!");
            }
            lblSid.Text = "";
            txtName.Text = "";
            DateTimePicDOB.ResetText();
            radBtnFemale.Checked = false;
            radBtnMale.Checked = false;

            btnAdd.Show();
            btnDelete.Hide();
            btnEdit.Hide();
            btnCancel.Hide();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblSid.Text = "";
            txtName.Text = "";
            DateTimePicDOB.ResetText();
            radBtnFemale.Checked = false;
            radBtnMale.Checked = false;

            btnAdd.Show();
            btnDelete.Hide();
            btnEdit.Hide();
            btnCancel.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
            {
                adapter = new SqlDataAdapter("SELECT * FROM Student", con);
                dt = new DataTable();
                adapter.Fill(dt);
                dgViewStudent.DataSource = dt;
            }
        }

        private void radBtnMale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void radBtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void dgViewStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = this.dgViewStudent.CurrentRow.Cells["Name"].Value.ToString();
            DateTimePicDOB.Text = this.dgViewStudent.CurrentRow.Cells["DOB"].Value.ToString();
            lblSid.Text = this.dgViewStudent.CurrentRow.Cells["StudentID"].Value.ToString();
            if(this.dgViewStudent.CurrentRow.Cells["Gender"].Value.ToString()== "Male")
            {
                radBtnMale.Checked = true;
            }
            else
            {
                radBtnFemale.Checked = true;
            }
            btnAdd.Hide();
            btnDelete.Show();
            btnEdit.Show();
            btnCancel.Show();
            lblSid.Hide();
        }
    }
}
