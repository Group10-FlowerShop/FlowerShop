using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop
{
    public partial class LoginForm : Form
    {
        private db_flowerDataContext _context;
        public LoginForm()
        {
            InitializeComponent();
            this._context = new db_flowerDataContext();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            input_username.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                input_password.PasswordChar = '\0';
            }
            else
            {
                input_password.PasswordChar = '*';
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {

        }
    }
}
