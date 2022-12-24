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
    class BanAnDAO
    {
        
       public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static BanAnDAO instance;
        public static BanAnDAO Instance
        {
            get { if (instance == null) instance = new BanAnDAO(); return BanAnDAO.instance; }
            private set { BanAnDAO.instance = value; }
        }
        public BanAnDAO() { }
        public List<BanAn> themDsBan(string query)
        {
            List<BanAn> DanhSachBan = new List<BanAn>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);

            foreach (DataRow drow in data.Rows)
            {
                BanAn Mon_an = new BanAn(drow);
                DanhSachBan.Add(Mon_an);
            }
            data.Clear();
            return DanhSachBan;
        }
        public int  updateSoKhach(int soKhach,int maban)
        {
            int num = 0;
                using (SqlConnection sql_cn = new SqlConnection(sql_data))
                {
                    sql_cn.Open();
                    SqlCommand sql_cm = new SqlCommand($"exec dbo.update_soKhach {maban},{soKhach} ", sql_cn);
                    num = sql_cm.ExecuteNonQuery();
                    sql_cn.Close();
            }
            return num;
        }
   
        public int updateSoKhachRaVe(int soKhach, int maban)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand($"exec dbo.update_soKhachRaVe {maban},{soKhach} ", sql_cn);
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;

        }
        public int insertBanAn(string tenban)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand($"exec dbo.insert_BanAn @TenBanAn ", sql_cn);
                sql_cm.Parameters.Add("@TenBanAn", SqlDbType.NVarChar, 30).Value = tenban;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();


            }
            return num;

        }
        public int updateBanAn(string tenban,int maban,int sl)
        {

            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand($"exec dbo.update_BanAn @MaBanAn,@TenBanAn,@SoKhachNgoi ", sql_cn);
                sql_cm.Parameters.Add("@TenBanAn", SqlDbType.NVarChar, 30).Value = tenban;
                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value = maban;
                sql_cm.Parameters.Add("@SoKhachNgoi", SqlDbType.Int).Value = sl;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;

        }
        public int updateBanAn2(int maban, int sl,int old_sl)
        {

            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand($"exec dbo.update_soKhach2 @MaBanAn,@SoLuong, @SoLuongCu ", sql_cn);
                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value =maban;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sl;
                sql_cm.Parameters.Add("@SoLuongCu", SqlDbType.Int).Value =old_sl;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;

        }
        public int deleteBanAn(int maban)
        {

            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand($"exec dbo.delete_BanAn @MaBanAn", sql_cn);

                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value = maban;

                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;

        }

    }

}
