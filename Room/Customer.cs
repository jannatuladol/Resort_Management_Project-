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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GNQC0AU;Initial Catalog=Customer;Integrated Security=True");
        public int CustomerId;
        private void Customer_Load(object sender, EventArgs e)
        {
            GetCustomerRecords();
        }

        private void GetCustomerRecords()
        {

            SqlCommand cmd = new SqlCommand("Select * from CustomerTb", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            CustomerDetailsDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTb VALUES (@Name, @MobileNo, @Address,@DOB,@RoomNo,@Nationality,@CheckIn)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                cmd.Parameters.AddWithValue("@CheckIn", txtCheckIn.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecords();
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
            CustomerId = 0;
            txtName.Clear();
            txtMobileNo.Clear();
            txtAddress.Clear();
            txtDOB.Clear();
            txtNationality.Clear();
            txtRoomNo.Clear();
            txtCheckIn.Clear();
            txtName.Focus();

        }

        private void CustomerDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CustomerId = Convert.ToInt32(CustomerDetailsDataGridView.SelectedRows[0].Cells[0].Value);
            txtName.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobileNo.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtAddress.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtDOB.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtRoomNo.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtNationality.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            txtCheckIn.Text = CustomerDetailsDataGridView.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CustomerId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE CustomerTb SET Name=@Name, MobileNo= @MobileNo,Address= @Address,DOB=@DOB,RoomNo=@RoomNo,Nationality=@Nationality,CheckIn=@CheckIn WHERE CustomerId= @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text); 
                cmd.Parameters.AddWithValue("@CheckIn", txtCheckIn.Text);
                cmd.Parameters.AddWithValue("@ID", this.CustomerId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details Updated", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecords();
                clearAll();
            }
            else
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CustomerId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE CustomerTb  WHERE CheckOutID = @ID", con);
                cmd.CommandType = CommandType.Text;
               
                cmd.Parameters.AddWithValue("@ID", this.CustomerId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details Deleted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecords();
                clearAll();
            }
            else
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        
    }
}
