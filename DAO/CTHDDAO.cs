using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{

    public class CTHDDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static CTHDDAO instance;
        public static CTHDDAO Instance
        {
            get { if (instance == null) instance = new CTHDDAO(); return CTHDDAO.instance; }
            private set { CTHDDAO.instance = value; }
        }
        public CTHDDAO() { }
        public List<CTHD> cthd(string query)
        {
            List<CTHD> HD = new List<CTHD>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);
            foreach (DataRow drow in data.Rows)
            {
                CTHD h_d = new CTHD(drow);
                HD.Add(h_d);
            }
            data.Clear();
            return HD;
        }
        public int InsertCTHD2(int maHoaDon, int MaMonAn, int SoLuong)
        {
            int num = 0;
            DataTable data = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.insert_CTHD_2 @MaHd,@MaMonAn,@SoLuong", sql_cn);
                sql_cm.Parameters.Add("@MaHd", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }
        public int update_sl_cthd(int maHoaDon, int MaMonAn, int SoLuong,int SoLuongCu)
        {
            int num = 0;
            DataTable data = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_sl_CTHD @MaHd,@MaMonAn,@SoLuong,@SoLuongCu", sql_cn);
                sql_cm.Parameters.Add("@MaHd", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
                sql_cm.Parameters.Add("@SoLuongCu", SqlDbType.Int).Value = SoLuongCu;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }

        public int update_cthd(int maHoaDon, int MaMonAnCu, int MaMonAnMoi, int SoLuong)
        {
            int num = 0;
            DataTable data = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_CTHD @MaHoaDon,@MaMonAnCu,@MaMonAnMoi,@SoLuong", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaMonAnCu", SqlDbType.Int).Value = MaMonAnCu;
                sql_cm.Parameters.Add("@MaMonAnMoi", SqlDbType.Int).Value = MaMonAnMoi;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }
        public int delete_cthd(int maHoaDon, int MaMonAn )
        {
            int num = 0;
            DataTable data = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_CTHD @MaHoaDon,@MaMonAn", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }

        public int update_tt_hd(int maHoaDon, int MaMonAn)
        {
            int num = 0;
            DataTable data = new DataTable();
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_tt_cthd @MaMonAn,@MaHoaDon", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaMonAn", SqlDbType.Int).Value = MaMonAn;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            data.Clear();
            return num;
        }
    }
}


