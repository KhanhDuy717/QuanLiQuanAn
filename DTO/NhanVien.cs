using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace QuanLiQuanAn
{
    public class NhanVien
    {   public NhanVien() { }
        public NhanVien(string tenNhanVien, string matKhau,int maNhanVien,int maChucVu,string anhNhanVien)
        {

            this.TenNhanVien = tenNhanVien;
            this.MaNhanVien = maNhanVien;
            this.MatKhau = matKhau;
            this.MaChucVu = maChucVu;
            this.AnhNhanVien = anhNhanVien;
        }
        public NhanVien(DataRow row)
        {
 
            this.MaNhanVien = (int)row["MaNhanVien"];
            this.TenNhanVien = row["TenNhanVien"].ToString();
            this.MatKhau = row["MatKhau"].ToString();
            this.MaChucVu = (int)row["MaChucVu"];
            this.AnhNhanVien = row["AnhNhanVien"].ToString();
        }

        private string tenNhanVien;
        private int maNhanVien;
        private string matKhau;
        private int maChucVu;
        private string anhNhanVien;

        public string TenNhanVien { get => tenNhanVien; set => tenNhanVien = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public int MaChucVu { get => maChucVu; set => maChucVu = value; }
        public string AnhNhanVien { get => anhNhanVien; set => anhNhanVien = value; }
    }
}
