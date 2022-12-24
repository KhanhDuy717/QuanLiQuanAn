using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{

    public class NewCTHDDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static NewCTHDDAO instance;
        public static NewCTHDDAO Instance
        {
            get { if (instance == null) instance = new NewCTHDDAO(); return NewCTHDDAO.instance; }
            private set { NewCTHDDAO.instance = value; }
        }
        public NewCTHDDAO() { }
        public List<NewCTHD> newCthd(string query)
        {
            List<NewCTHD> HD = new List<NewCTHD>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);
            foreach (DataRow drow in data.Rows)
            {
                NewCTHD h_d = new NewCTHD(drow);
                HD.Add(h_d);
            }
            data.Clear();
            return HD;
        }

    }
}