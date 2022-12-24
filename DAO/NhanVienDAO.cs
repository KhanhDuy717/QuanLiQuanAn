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
    public class NhanVienDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";

        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO();return NhanVienDAO.instance; }
            private set { NhanVienDAO.instance = value; }
        }
        public NhanVienDAO() {}

        public List<NhanVien> themTTNhanVien(string query)
        {
            List<NhanVien> Nv= new List<NhanVien>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);
            foreach (DataRow drow in data.Rows)
            {
                NhanVien N_v = new NhanVien(drow);
                Nv.Add(N_v);
            }
            data.Clear();
            return Nv;
        }
        public int KtDangNhap(string tenNv, string mk) {
            string qry= "exec dbo.KiemTraDangNhap N'"+tenNv+"', N'"+mk+"'";
            DataTable data = new DataTable();
            data=Database.Instance.KetNoiSql(qry);
            int num= data.Rows.Count;
            data.Clear();
            return num;
        }
        public int KtAdmin(string tenNv, string mk)
        {
            string qry = $"exec dbo.KiemTraAdmin '{tenNv}','{mk}'";
            DataTable data = new DataTable();
            data=Database.Instance.KetNoiSql(qry);
            int num = data.Rows.Count;
            data.Clear();
            return num;
        }
        public int insert(string tenNv, string mk, int macv,string anh)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.themNhanVien @TenNhanVien,@MatKhau, @MaChucVu,@AnhNhanVien", sql_cn);
                sql_cm.Parameters.Add("@TenNhanVien", SqlDbType.NVarChar, 30).Value = tenNv;
                sql_cm.Parameters.Add("@MatKhau", SqlDbType.VarChar, 30).Value = mk;
                sql_cm.Parameters.Add("@MaChucVu", SqlDbType.Int).Value = macv;
                sql_cm.Parameters.Add("@AnhNhanVien", SqlDbType.VarChar,50).Value = anh;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int update(string tenNv, string mk, int macv,int manv)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.updateNhanVien @TenNhanVien,@MatKhau,@MaChucVu, @MaNhanVien", sql_cn);
                sql_cm.Parameters.Add("@TenNhanVien", SqlDbType.NVarChar, 30).Value = tenNv;
                sql_cm.Parameters.Add("@MatKhau", SqlDbType.VarChar, 30).Value = mk;
                sql_cm.Parameters.Add("@MaChucVu", SqlDbType.Int).Value = macv;
                sql_cm.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = manv;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int delete(int manv)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.xoaNv @MaNhanVien", sql_cn);
                sql_cm.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = manv;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }

    }
}
