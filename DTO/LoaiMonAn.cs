using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    class LoaiMonAn
    {
        public LoaiMonAn() { }
        public LoaiMonAn(int maLoaiMonAn, string tenLoaiMonAn)
        {
            this.TenLoaiMonAn = tenLoaiMonAn;
            this.MaLoaiMonAn = maLoaiMonAn;

        }
        public LoaiMonAn(DataRow row)
        {

            this.TenLoaiMonAn = row["TenLoaiMonAn"].ToString();
            this.MaLoaiMonAn = (int)row["MaLoaiMonAn"];
           
        }
        private int maLoaiMonAn;
        private string tenLoaiMonAn;

        public int MaLoaiMonAn { get => maLoaiMonAn; set => maLoaiMonAn = value; }
        public string TenLoaiMonAn { get => tenLoaiMonAn; set => tenLoaiMonAn = value; }
    }
}
