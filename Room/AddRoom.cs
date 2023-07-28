using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Room
{
    public partial class AddRoom : Form
    {
        public AddRoom()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GNQC0AU;Initial Catalog=ProRoom;Integrated Security=True ");

        public int RoomId;
        private void AddRoom_Load(object sender, EventArgs e)
        {

            GetRoomDetailsRecord();
        }

        private void GetRoomDetailsRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from RoomTb", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            RoomDetailsDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO RoomTb VALUES (@RoomNo, @RoomType, @BedType,@Price)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                cmd.Parameters.AddWithValue("@RoomType", txtRoomType.Text);
                cmd.Parameters.AddWithValue("@BedType", txtBedType.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Room Added", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetRoomDetailsRecord();
                clearAll();
            }

        }


        private bool isValid()
        {
            if (txtRoomNo.Text == string.Empty)
            {
                MessageBox.Show("Room NO is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void clearAll()
        {
            RoomId = 0;
            txtRoomNo.Clear();
            txtRoomType.Clear();
            txtBedType.Clear();
            txtPrice.Clear();
            txtRoomNo.Focus();
        }

        private void RoomDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.RoomId = Convert.ToInt32(RoomDetailsDataGridView.SelectedRows[0].Cells[0].Value);
            txtRoomNo.Text = RoomDetailsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtRoomType.Text = RoomDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtBedType.Text = RoomDetailsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtPrice.Text = RoomDetailsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (RoomId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE RoomTb  WHERE RoomID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.RoomId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Room is deleted from the record ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetRoomDetailsRecord();
                clearAll();
            }
            else
            {
                MessageBox.Show("Please, select Room to Delete", "slected?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
