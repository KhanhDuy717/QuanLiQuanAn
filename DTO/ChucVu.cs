using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public class ChucVu
    {
        private int maChucVu;
        private string tenChucVu;

        public ChucVu() { }
        public ChucVu(int maChucVu, string tenChucVu)
        {
            this.MaChucVu = maChucVu;
            this.TenChucVu = tenChucVu;
        }
        public ChucVu(DataRow row)
        {
            this.MaChucVu = (int)row["MaChucVu"];
            this.TenChucVu = row["TenChucVu"].ToString();

        }
        public int MaChucVu { get => maChucVu; set => maChucVu = value; }
        public string TenChucVu { get => tenChucVu; set => tenChucVu = value; }
    }
}
