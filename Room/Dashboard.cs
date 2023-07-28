using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Room
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRoom ar = new AddRoom();
            ar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer cr = new Customer();
            cr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckOut co = new CheckOut();
            co.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.Show();
        }
    }
}
