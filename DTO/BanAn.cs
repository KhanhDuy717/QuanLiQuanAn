using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace QuanLiQuanAn
{
    public class BanAn
    {

        public BanAn() { }

        public BanAn(int maBanAn, string tenBanAn, int soKhachNgoi)
        {
            this.MaBanAn = maBanAn;
            this.TenBanAn = tenBanAn;
            this.SoKhachNgoi = soKhachNgoi;
        }
        public BanAn(DataRow row)
        {
            this.MaBanAn = (int)row["MaBanAn"];
            this.TenBanAn = row["TenBanAn"].ToString();
            this.SoKhachNgoi =(int)row["SoKhachNgoi"];
        }
        private int maBanAn;
        private string tenBanAn;
        private int soKhachNgoi;

        public int MaBanAn { get => maBanAn; set => maBanAn = value; }
        public string TenBanAn { get => tenBanAn; set => tenBanAn = value; }
        public int SoKhachNgoi { get => soKhachNgoi; set => soKhachNgoi = value; }
    }
}
