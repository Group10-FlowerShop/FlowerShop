using FlowerShop.Model;
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
            var result = (from account in _context.accounts
                          join employee in _context.employees on account.account_id equals employee.account_id
                          join emp_role in _context.emp_roles on employee.employee_id equals emp_role.employee_id
                          join emp_perm in _context.emp_permissions on emp_role.perm_id equals emp_perm.perm_id
                          where account.username == input_username.Text && account.password == input_password.Text && account.account_id.StartsWith("ACCNV")
                          group emp_perm by new { employee.employee_id, employee.name } into empGroup
                          select new TTNhanVien
                          {
                              Id = empGroup.Key.employee_id,
                              Name = empGroup.Key.name,
                              Roles = empGroup.Select(perm => perm.name).ToList()
                          }).FirstOrDefault();

            if (result != null)
            {
                if (result.Roles.Count == 0)
                {
                    MessageBox.Show("Tài khoản này chưa có quyền. Vui lòng liên hệ với nhân viên khác để cập nhật quyền!", "Thông báo");
                }
                else
                {
                    Main m = new Main(result);
                    m.FormClosed += (s, args) => this.Show();
                    this.Hide();
                    m.Show();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo");
            }

        }
    }
}
