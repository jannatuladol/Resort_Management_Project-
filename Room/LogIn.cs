using System;
using System.Windows.Forms;



namespace Room
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Admin" && txtPassword.Text == "0000")
            {
               Dashboard d = new Dashboard();
                 d.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("The User name or Password you entered is incorrect , please try again");
                txtUserName.Clear();
                txtPassword.Clear();
                txtUserName.Focus();
            }
        
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false ;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true ;

            }
        }
    }
}
    

