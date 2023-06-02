using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyNhanSu
{
    public partial class DangNhap : Form
    {
        public static string UserName = "";
        public DangNhap()
        {
            InitializeComponent();


        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void rbtnShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnShowPass.Checked)
            {
                txtPass.PasswordChar = (char)0;
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }



        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Login";
                cmd.Parameters.AddWithValue("@UserName", txtUser.Text);
                cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                cmd.Connection = conn;
                UserName = txtUser.Text;
                object kq = cmd.ExecuteScalar();
                int code = Convert.ToInt32(kq);
                if (code == 1)
                {
                    MessageBox.Show("Bạn đã đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    (new Form1()).Show();
                }
                else if (code == 2)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPass.Text = "";
                    txtUser.Text = "";
                    txtUser.Focus();
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPass.Text = "";
                    txtUser.Text = "";
                    txtUser.Focus();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
                Application.Exit();
        }

        private void txtUser_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
