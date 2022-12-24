using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn { 
   public class MonThinhHanhDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static MonThinhHanhDAO instance;
        public static MonThinhHanhDAO Instance
        {
            get { if (instance == null) instance = new MonThinhHanhDAO(); return MonThinhHanhDAO.instance; }
            private set { MonThinhHanhDAO.instance = value; }
        }
        public MonThinhHanhDAO() { }
        public List<MonThinhHanh> DsMonThinhHanh(string query)
        {
            List<MonThinhHanh> DanhSachMonTh = new List<MonThinhHanh>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);

            foreach (DataRow drow in data.Rows)
            {
                MonThinhHanh Mon_an_th = new MonThinhHanh(drow);
                DanhSachMonTh.Add(Mon_an_th);
            }
            data.Clear();
            return DanhSachMonTh;
        }
    }
}
