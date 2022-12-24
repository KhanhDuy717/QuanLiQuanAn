using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public class CTHD
    {

        private int soLuong;
        private int maMonAn;
        private int maHoaDon;

        
      
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }

        public int MaMonAn { get => maMonAn; set => maMonAn = value; }


        //public string Mamonan { get => mamonan; set => mamonan = value; }

        public CTHD() { }

        public CTHD(int soLuong, int mamonan,int maHoaDon)
        {

            this.SoLuong = soLuong;
            this.MaMonAn = mamonan;
            this.MaHoaDon = maHoaDon;
        }
        public CTHD(DataRow row)
        {
            this.SoLuong = (int)row["SoLuongMon"];
            this.MaMonAn = (int) row["MaMonAn"];
            this.MaHoaDon =(int)row["MaHoaDon"];
          
        }
    }
}
