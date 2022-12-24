using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
  public  class MonThinhHanh
    {
        private string tenMonAn;
        private int soLuong;
        private int tien;
        private double phanTram;
        public MonThinhHanh(string tenMonAn,int soLuong, int tien, double phanTram)
        {
            this.SoLuong = soLuong;
            this.TenMonAn = tenMonAn;
            this.Tien = tien;
            this.PhanTram = phanTram;
        }
        public MonThinhHanh(DataRow row)
        {
            this.TenMonAn = row["TenMonAn"].ToString();
            this.SoLuong = (int)row["SoLuong"];
            this.Tien = (int)row["Tien"];
            this.PhanTram = (double)row["PhanTram"];
        }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int Tien { get => tien; set => tien = value; }
        public double PhanTram { get => phanTram; set => phanTram = value; }
    }
}
