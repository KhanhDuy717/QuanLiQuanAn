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
  public class MonAnDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static MonAnDAO instance;
        public static MonAnDAO Instance
        {
            get { if (instance == null) instance = new MonAnDAO();return MonAnDAO.instance; }
            private set { MonAnDAO.instance = value; }
        }
        public MonAnDAO() {}
        public List<MonAn> themDsMon( string query)
        {
            List<MonAn> DanhSachMon = new List<MonAn>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);
        
            foreach (DataRow drow in data.Rows)
            {
                MonAn Mon_an = new MonAn(drow);
                DanhSachMon.Add(Mon_an);
            }
            data.Clear();
            return DanhSachMon;  
        }
        public int InsertMonAn(string TenMonAn, int MaLoaiMonAn, int Gia, string Anh)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.insert_MonAn @TenMonAn,@MaLoaiMonAn,@Gia,@Anh", sql_cn);
                sql_cm.Parameters.Add("@TenMonAn", SqlDbType.NVarChar,30).Value = TenMonAn;
                sql_cm.Parameters.Add("@MaLoaiMonAn", SqlDbType.Int).Value = MaLoaiMonAn;
                sql_cm.Parameters.Add("@Gia", SqlDbType.Int).Value = Gia;
                sql_cm.Parameters.Add("@Anh", SqlDbType.VarChar).Value = Anh;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int DeleteMonAn(string TenMonAn)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_MonAn @TenMonAn", sql_cn);
                sql_cm.Parameters.Add("@TenMonAn", SqlDbType.NVarChar, 30).Value = TenMonAn;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int UpdateMonAn(int MaMonAn, string TenMonAn,int MaLoaiMonAn, int Gia)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_MonAn @TenMonAn,@MaMonAn,@MaLoaiMonAn,@Gia", sql_cn);
                sql_cm.Parameters.Add("@TenMonAn", SqlDbType.NVarChar, 30).Value = TenMonAn;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                sql_cm.Parameters.Add("@MaLoaiMonAn", SqlDbType.Int).Value = MaLoaiMonAn;
                sql_cm.Parameters.Add("@Gia", SqlDbType.Int).Value = Gia;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
    }
}
