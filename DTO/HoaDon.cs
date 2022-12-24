using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public class HoaDon
    {
        private int maBanAn;
        private int maHoaDon;
        private DateTime? ngayThanhToan;
        private int thanhTien;
        private int maNhanVien;
        private string thanhToan;
        private int slNguoi;
        public HoaDon() { }

        public HoaDon(int maBanAn, int maHoaDon, DateTime? ngayThanhToan, int thanhTien, int maNhanVien,string thanhToan,int slNguoi)
        {
            this.MaBanAn = maBanAn;
            this.MaHoaDon = maHoaDon;
            this.NgayThanhToan = ngayThanhToan;
            this.ThanhTien = thanhTien;
            this.MaNhanVien = maNhanVien;
            this.ThanhToan = thanhToan;
            this.SlNguoi =slNguoi;
        }
        public HoaDon(DataRow row)
        {
            this.MaBanAn = (int)row["MaBanAn"];
            this.MaHoaDon = (int)row["MaHoaDon"];
            this.NgayThanhToan = (DateTime)row["NgayThanhToan"];
            this.ThanhTien = (int)row["ThanhTien"];
            this.MaNhanVien = (int)row["MaNhanVien"];
            this.ThanhToan = row["thanhToan"].ToString();
            this.SlNguoi = (int)row["soluongnguoi"];
        }
        public int MaBanAn { get => maBanAn; set => maBanAn = value; }
        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public DateTime? NgayThanhToan { get => ngayThanhToan; set => ngayThanhToan = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string ThanhToan { get => thanhToan; set => thanhToan = value; }
        public int SlNguoi { get => slNguoi; set => slNguoi = value; }
    }
}
