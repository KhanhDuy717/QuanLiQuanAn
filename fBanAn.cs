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
    public partial class fBanAn : Form
    {
        public fBanAn()
        {
            InitializeComponent();
        }
        private fQuanLiQuanAn fQuanAn =null;
        public fBanAn(Form callingForm)
        {
            fQuanAn = callingForm as fQuanLiQuanAn;
            InitializeComponent();
        }

        public void ThenBanAn(string query, FlowLayoutPanel flp)
        {
            List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan(query);
            foreach (BanAn B_a in Ban_an)
            {
                RadioButton rdb = new RadioButton() { Width = 145, Height = 165 };
                rdb.Image = new Bitmap(Application.StartupPath+"\\BanAn\\BanAn.jpg");
                rdb.Text = B_a.TenBanAn + " - " + B_a.SoKhachNgoi + "/4 người";
                rdb.TextAlign = ContentAlignment.BottomLeft;
                rdb.CheckAlign = ContentAlignment.BottomLeft;
                 rdb.BackgroundImageLayout = ImageLayout.Zoom;
                flp.Controls.Add(rdb);
                if (B_a.SoKhachNgoi >=4) {
                   rdb.Enabled = false;
                   
                }

                btnXacNhanBan.Click += (o, e) =>
                {
                    if (rdb.Checked == true)
                    {
                        this.fQuanAn.Txt = B_a.MaBanAn.ToString();

                    }
                    this.Close();
                };
                
            }
            Ban_an.Clear();
            
        }

 
        private void fBanAn_Load(object sender, EventArgs e)
        {
            flpBanAn.Controls.Clear();
            ThenBanAn("select * from banan", flpBanAn);
            this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");
        }

       

        private void btnTrong_Click(object sender, EventArgs e)
        {
            flpBanAn.Controls.Clear();
            ThenBanAn("Select * from banAn where sokhachngoi <4", flpBanAn);
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            flpBanAn.Controls.Clear();
            ThenBanAn("Select * from banAn where sokhachngoi >=4", flpBanAn);
        }

        private void btnTimBan_Click(object sender, EventArgs e)
        {
            string name = "N'" + txtTimKiemBan.Text + "'";
            flpBanAn.Controls.Clear();
            ThenBanAn($@"exec dbo.TimKiemBan {name}", flpBanAn);
        }

      
    }

    
}
