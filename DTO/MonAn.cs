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
    public class MonAn
    {   public MonAn() { }
        public MonAn(int maMonAn, string tenMonAn, int maLoaiMonAn,int gia, string anh)
        {
            this.MaLoaiMonAn = maLoaiMonAn;
            this.TenMonAn = tenMonAn;
            this.MaMonAn = maMonAn;
            this.Gia = gia;
            this.Anh = anh;
        }
        public MonAn(DataRow row)
        {
            
            this.TenMonAn = row["TenMonAn"].ToString();
            this.MaLoaiMonAn = (int)row["MaLoaiMonAn"];
            this.Gia = (int)row["Gia"];
            this.MaMonAn = (int)row["MaMonAn"];
            this.Anh = row["Anh"].ToString();
        }
        private int maMonAn;
        private string tenMonAn;
        private int maLoaiMonAn;
        private int gia;
        private string anh;
        public int MaMonAn { get => maMonAn; set => maMonAn = value; }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public int MaLoaiMonAn { get => maLoaiMonAn; set => maLoaiMonAn = value; }
        public int Gia { get => gia; set => gia = value; }
        public string Anh { get => anh; set => anh = value; }
    }
   
}
