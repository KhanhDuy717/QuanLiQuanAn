using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class DangNhap : Form
    {
       
        public DangNhap()
        {
            InitializeComponent();

            this.Load += (o, e) =>
            {
                this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");

            };
            txtTenDN.Leave += (o, e) =>
            {
                if (txtTenDN.Text != "")
                {
                    btnXacNhan.Enabled = true;
                }
            };

            txtMK.Leave += (o, e) =>
            {
                if (txtMK.Text != "")
                {
                    btnXacNhan.Enabled = true;
                }
            };
            btnThoat.Click += (o, e) =>
            {
                Application.Exit();
            };
            this.FormClosing += (o, e) =>
            {
                if (MessageBox.Show("Bạn có muốn đóng ứng dụng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    e.Cancel = true;
            };
            btnXacNhan.Click += (o, e) =>
            {
               
                btnXacNhan.Cursor = Cursors.AppStarting;
                KiemTraDangNhap();
                
            };
            btnThoat.MouseHover+= (o, e) =>
            {
             btnThoat.BackColor = Color.DarkGray;
            };
            btnThoat.MouseLeave += (o, e) =>
            {
                btnThoat.BackColor = Color.White;
            };
            btnXacNhan.MouseHover += (o, e) =>
            {
                btnXacNhan.BackColor = Color.DarkGray;
            };
            btnXacNhan.MouseLeave += (o, e) =>
            {
                btnXacNhan.BackColor = Color.White;
            };
            

        }

        void KiemTraDangNhap()
        {
            string TenDn = txtTenDN.Text;
            string Mk = txtMK.Text;
            
            if (NhanVienDAO.Instance.KtDangNhap(TenDn, Mk) ==1)
            {
                fQuanLiQuanAn Ql = new fQuanLiQuanAn();
                this.Hide();
                Ql.ShowDialog();
              
            }
            else
                MessageBox.Show("Sai tên hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


    }
}
