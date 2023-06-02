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

namespace QuanLyNhanSu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LayDSNS();
        }

        private void LayDSNS()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "SP_LayDSNS";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgDSNS.DataSource = dt;
                con.Close();
                dtgDSNS.Columns[0].Width = 35;
                dtgDSNS.Columns[0].HeaderText = "ID";
                dtgDSNS.Columns[1].Width = 130;
                dtgDSNS.Columns[1].HeaderText = "Họ Tên";
                dtgDSNS.Columns[2].Width = 80;
                dtgDSNS.Columns[2].HeaderText = "Số CCCD";
                dtgDSNS.Columns[3].Width = 80;
                dtgDSNS.Columns[3].HeaderText = "Giới tính";
                dtgDSNS.Columns[4].Width = 80;
                dtgDSNS.Columns[4].HeaderText = "Ngày sinh";
                dtgDSNS.Columns[5].Width = 135;
                dtgDSNS.Columns[5].HeaderText = "Email";
                dtgDSNS.Columns[6].Width = 80;
                dtgDSNS.Columns[6].HeaderText = "Địa chỉ";
                dtgDSNS.Columns[7].Width = 70;
                dtgDSNS.Columns[7].HeaderText = "Phone";
                dtgDSNS.Columns[8].Width = 80;
                dtgDSNS.Columns[8].HeaderText = "Phòng ban";
                dtgDSNS.Columns[9].Width = 80;
                dtgDSNS.Columns[9].HeaderText = "Chức vụ";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSapXep.SelectedIndex == 0)
            {
                this.dtgDSNS.Sort(this.dtgDSNS.Columns["PhongBan"], ListSortDirection.Ascending);
            }
            else
            {
                this.dtgDSNS.Sort(this.dtgDSNS.Columns["ID"], ListSortDirection.Ascending);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "HoTen", "*" + txtTimKiem.Text + "*");
            (dtgDSNS.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                LayDSNS();
            }
        }

        private void Reset()
        {
            txtCCCD.Text = "";
            txtChucVu.Text = "";
            txtPhongBan.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtHoTen.Text = "";
            txtID.Text = "";
            txtPhone.Text = "";
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
        }

        private bool KiemTraThongTin()
        {
            if (txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng điền họ và tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }
            if (txtCCCD.Text == "")
            {
                MessageBox.Show("Vui lòng điền CCCD nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCCCD.Focus();
                return false;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng điền địa chỉ của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng điền Email của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            if (rdoNam.Checked == false && rdoNu.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính cho nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Vui lòng điền số điện thoại của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }
            if (txtPhongBan.Text == "")
            {
                MessageBox.Show("Vui lòng điền phòng ban nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhongBan.Focus();
                return false;
            }
            if (txtChucVu.Text == "")
            {
                MessageBox.Show("Vui lòng điền Chức vụ nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtChucVu.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_ThemNhanVien";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = txtHoTen.Text;
                    cmd.Parameters.Add("@SoCCCD_CMT", SqlDbType.NVarChar).Value = txtCCCD.Text;

                    if (rdoNam.Checked == true)
                    {
                        cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = rdoNam.Text;
                    }
                    else
                    {
                        cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = rdoNu.Text;
                    }
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dtpNgaySinh.Text;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text;
                    cmd.Parameters.Add("@PhongBan", SqlDbType.NVarChar).Value = txtPhongBan.Text;
                    cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar).Value = txtChucVu.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSNS();
                    MessageBox.Show("Thêm mới nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtID_Enter(object sender, EventArgs e)
        {
            if (txtID.Text == "Thêm mới không cần ID")
            {
                txtID.Clear();
                txtID.ForeColor = SystemColors.Highlight;
            }
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                txtID.Text = "Thêm mới không cần ID";
                txtID.ForeColor = SystemColors.InactiveCaption;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtID.Text == "" || txtID.Text == "Thêm mới không cần ID")
            {
                MessageBox.Show("Vui lòng điền ID nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                txtID.SelectAll();
            }
            else if (KiemTraThongTin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_SuaNhanVien";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(txtID.Text);
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = txtHoTen.Text;
                    cmd.Parameters.Add("@SoCCCD_CMT", SqlDbType.NVarChar).Value = txtCCCD.Text;
                    if (rdoNam.Checked == true)
                    {
                        cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = rdoNam.Text;
                    }
                    else
                    {
                        cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = rdoNu.Text;
                    }
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dtpNgaySinh.Text;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text;
                    cmd.Parameters.Add("@PhongBan", SqlDbType.NVarChar).Value = txtPhongBan.Text;
                    cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar).Value = txtChucVu.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSNS();
                    MessageBox.Show("Sửa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dtgDSNS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgDSNS.Rows[e.RowIndex];
            txtID.Text = Convert.ToString(row.Cells["ID"].Value);
            txtHoTen.Text = Convert.ToString(row.Cells["HoTen"].Value);
            txtCCCD.Text = Convert.ToString(row.Cells["SoCCCD_CMT"].Value);
            dtpNgaySinh.Text = Convert.ToString(row.Cells["NgaySinh"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
            txtPhongBan.Text = Convert.ToString(row.Cells["PhongBan"].Value);
            txtChucVu.Text = Convert.ToString(row.Cells["ChucVu"].Value);
            string GioiTinh = Convert.ToString(row.Cells["GioiTinh"].Value);
            if (GioiTinh.Trim() == "Nu")
            {
                rdoNu.Checked = true;
            }
            else
            {
                rdoNam.Checked = true;
            }
            txtPhone.Text = Convert.ToString(row.Cells["Phone"].Value);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "Thêm mới không cần ID" || txtID.Text == "")
            {
                MessageBox.Show("Vui lòng điền ID nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_XoaNhanVien";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(txtID.Text);
                   
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSNS();
                    MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
