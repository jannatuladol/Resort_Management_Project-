using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Room
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GNQC0AU;Initial Catalog=Employee;Integrated Security=True ");
        public int EmployeeID;

        private void Employee_Load(object sender, EventArgs e)
        {
            GetEmployeerRecords();
        }

        private void GetEmployeerRecords()
        {

            SqlCommand cmd = new SqlCommand("Select * from EmployeeTb", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            EmployeeDetailsDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeTb VALUES (@Name, @MobileNo, @Address,@Email,@Designation,@WorkingStatus)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@WorkingStatus", txtWorkingStatus.Text);
               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetEmployeerRecords();
                clearAll();


            }
        }

        private bool isValid()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void clearAll()
        {
            EmployeeID = 0;
            txtName.Clear();
            txtMobileNo.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtDesignation.Clear();
            txtWorkingStatus.Clear();
            
            txtName.Focus();

        }

        private void EmployeeDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.EmployeeID = Convert.ToInt32(EmployeeDetailsDataGridView.SelectedRows[0].Cells[0].Value);
            txtName.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobileNo.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtAddress.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtEmail.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtDesignation.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtWorkingStatus.Text = EmployeeDetailsDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmployeeID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE EmployeeTb SET Name=@Name, MobileNo= @MobileNo,Address= @Address,Email=@Email,Designation=@Designation,WorkingStatus=@WorkingStatus WHERE EmployeeID= @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@WorkingStatus", txtWorkingStatus.Text);
                cmd.Parameters.AddWithValue("@ID", this.EmployeeID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details Updated", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetEmployeerRecords();
                clearAll();
            }
            else
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmployeeID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE EmployeeTb  WHERE EmployeeID = @ID", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.EmployeeID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details Deleted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetEmployeerRecords();
                clearAll();
            }
            else
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
