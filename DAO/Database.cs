using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace QuanLiQuanAn
{
    public class Database
    {  
        private static Database instance;
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        public static Database Instance
        {
            get { if (instance == null) instance = new Database();return Database.instance; }
            private set { Database.instance = value; }
        }
        public Database(){}
        public DataTable KetNoiSql(string qry)
        {
            DataTable DuLieu = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand(qry, sql_cn);
                SqlDataAdapter sql_ad = new SqlDataAdapter(sql_cm);
                sql_ad.Fill(DuLieu);
                sql_cn.Close();
               
            }
      
            return DuLieu;
        }
 

        public int InsertCTHD(int MaMonAn, int SoLuong)
        {
            int num = 0;
            int count = 0;
            DataTable data = new DataTable();
            List<HoaDon> hd = HoaDonDAO.Instance.themHoaDon($"select top 1* from hoadon order by mahoadon desc");
            foreach (HoaDon HD in hd)
            {
                count = HD.MaHoaDon;
            }
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.insert_CTHD @MaHd,@MaMonAn,@SoLuong", sql_cn);
                sql_cm.Parameters.Add("@MaHd", SqlDbType.Int).Value =count;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }
       
    }
}
