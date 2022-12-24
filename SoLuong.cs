using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class SoLuong : Form
    {
        public SoLuong()
        {
            InitializeComponent();

        }
        private fQuanLiQuanAn QuanAn = null;
        public SoLuong(Form callingForm)
        {
            QuanAn = callingForm as fQuanLiQuanAn;
            InitializeComponent();
        }


        private void btnXn_Click(object sender, EventArgs e)

        {
            int sl = 0;
            if (int.TryParse(txtSl.Text, out sl))
                this.Close();
            else if (int.TryParse(txtSl.Text, out sl)==false)
            {
                MessageBox.Show("Vui nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SoLuong_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.QuanAn.soLuong = txtSl.Text;
        }

        private void SoLuong_Load(object sender, EventArgs e)
        {
            this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");
        }
    }
}
