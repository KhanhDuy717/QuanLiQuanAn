using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public class NewCTHD
    {
        private int soLuong;
        private string tenMonAn;
        private int thanhTien;
        private int maHoaDon;


        public int SoLuong { get => soLuong; set => soLuong = value; }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }



        //public string Mamonan { get => mamonan; set => mamonan = value; }

        public NewCTHD() { }

        public NewCTHD(int soLuong, string tenMonAn, int maHoaDon, int thanhTien)
        {

            this.SoLuong = soLuong;
            this.TenMonAn = tenMonAn;
            this.MaHoaDon = maHoaDon;
            this.ThanhTien = thanhTien;
        }
        public NewCTHD(DataRow row)
        {
            this.SoLuong = (int)row["SoLuongMon"];
            this.TenMonAn = row["TenMonAn"].ToString();
            this.MaHoaDon = (int)row["MaHoaDon"];
            this.thanhTien = (int)row["ThanhTien"];
        }
    }
}
