using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
  public  class ChucVuDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static ChucVuDAO instance;
        public static ChucVuDAO Instance
        {
            get { if (instance == null) instance = new ChucVuDAO(); return ChucVuDAO.instance; }
            private set { ChucVuDAO.instance = value; }
        }
        public ChucVuDAO(){ }
        public List<ChucVu> themDsCV(string query)
        {
            List<ChucVu> DanhSachCV = new List<ChucVu>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);

            foreach (DataRow drow in data.Rows)
            {
                ChucVu Chuc_Vu = new ChucVu(drow);
                DanhSachCV.Add(Chuc_Vu);
            }
            data.Clear();
            return DanhSachCV;

        }
        public int InsertCv(string TenChucVu)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.themcv @TenChucVu", sql_cn);
                sql_cm.Parameters.Add("@TenChucVu", SqlDbType.NVarChar, 30).Value = TenChucVu;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteCv(string TenChucVu)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.xoaTencv @TenChucVu", sql_cn);
                sql_cm.Parameters.Add("@TenChucVu", SqlDbType.NVarChar, 30).Value = TenChucVu;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int UpdateCv(string TenChucVu, string TenChucVuMoi)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.updateTencv @TenChucVu,@TenChucVuMoi", sql_cn);
                sql_cm.Parameters.Add("@TenChucVu", SqlDbType.NVarChar, 30).Value = TenChucVu;
                sql_cm.Parameters.Add("@TenChucVuMoi", SqlDbType.NVarChar, 30).Value = TenChucVuMoi;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
    }
}
