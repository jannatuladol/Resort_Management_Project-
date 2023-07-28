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
    public partial class CheckOut : Form
    {
        public CheckOut()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GNQC0AU;Initial Catalog=CheckOut;Integrated Security=True");
        public int CheckOutId;
        private void CheckOut_Load(object sender, EventArgs e)
        {
            GetCheckOutRecords();
           
        }

        private void GetCheckOutRecords()
        {

            SqlCommand cmd = new SqlCommand("Select * from CheckOutTb", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            CheckOutDetailsDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CheckOutTb VALUES (@Name, @MobileNo, @Gender,@Nationality,@RoomNo,@Price,@CheckOutDate)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@CheckOutDate", txtCheckOutDate.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Details inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCheckOutRecords();
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
            CheckOutId = 0;
            txtName.Clear();
            txtMobileNo.Clear();
            txtGender.Clear();
            txtNationality.Clear();
            txtRoomNo.Clear();
            txtPrice.Clear();
            txtCheckOutDate.Clear();
            txtName.Focus();

        }

        private void CheckOutDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CheckOutId = Convert.ToInt32(CheckOutDetailsDataGridView.SelectedRows[0].Cells[0].Value);
            txtName.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobileNo.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtGender.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtNationality.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtRoomNo.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtPrice.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            txtCheckOutDate.Text = CheckOutDetailsDataGridView.SelectedRows[0].Cells[7].Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckOutId > 0)
           {  
            SqlCommand cmd = new SqlCommand("UPDATE CheckOutTb SET Name=@Name, MobileNo= @MobileNo,Gender= @Gender,Nationality=@Nationality,RoomNo=@RoomNo,Price=@Price,CheckOutDate=@CheckOutDate WHERE CheckOutId= @ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
            cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
            cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
            cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
            cmd.Parameters.AddWithValue("@CheckOutDate", txtCheckOutDate.Text);
            cmd.Parameters.AddWithValue("@ID", this.CheckOutId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("New Details Updated", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetCheckOutRecords();
            clearAll();
           }
            else
            {
              MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CheckOutId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE CheckOutTb  WHERE CheckOutID = @ID", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@ID", this.CheckOutId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student Deleted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCheckOutRecords();
                clearAll();
            }
            else
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       
    }

   
    }
    

