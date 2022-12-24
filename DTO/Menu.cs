using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    class Menu
    {
        private int maHoaDon;
        private int tien;
        private string teMonAn;
        public Menu() { }

        public Menu(int maHoaDon,int tien,string tenMonAn)
        {

            this.MaHoaDon = maHoaDon;
            this.Tien = tien;
            this.TeMonAn = tenMonAn;

        }
        public Menu(DataRow row)
        {
            this.MaHoaDon =(int)row["MaHoaDon"];
            this.Tien = (int)row["Tien"];
            this.TeMonAn = row["TenMonAn"].ToString();
        }
        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public int Tien { get => tien; set => tien = value; }
        public string TeMonAn { get => teMonAn; set => teMonAn = value; }
    }
}
