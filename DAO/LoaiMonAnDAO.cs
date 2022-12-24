using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    class LoaiMonAnDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";

        private static LoaiMonAnDAO instance;
        public static LoaiMonAnDAO Instance
        {
            get { if (instance == null) instance = new LoaiMonAnDAO(); return LoaiMonAnDAO.instance; }
            private set { LoaiMonAnDAO.instance = value; }
        }
        public LoaiMonAnDAO() { }
        public List<LoaiMonAn> themDsLoaiMon(string query)
        {
            List<LoaiMonAn> DanhSachLMon = new List<LoaiMonAn>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);

            foreach (DataRow drow in data.Rows)
            {
                LoaiMonAn LMon_an = new LoaiMonAn(drow);
                DanhSachLMon.Add(LMon_an);
            }
            data.Clear();
            return DanhSachLMon;
        }

        public int themLmon(string tenLoaiMon)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.themloaiman @TenLoaiMonAn", sql_cn);
                sql_cm.Parameters.Add("@TenLoaiMonAn", SqlDbType.NVarChar, 30).Value = tenLoaiMon;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int updateLoaiMon(string tenLoaiMon,string oldTenLoaiMon)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.updateLoaiMonAn @OldTenLoaiMonAn,@TenLoaiMonAn", sql_cn);
                sql_cm.Parameters.Add("@OldTenLoaiMonAn", SqlDbType.NVarChar, 30).Value = oldTenLoaiMon;
                sql_cm.Parameters.Add("@TenLoaiMonAn", SqlDbType.NVarChar, 30).Value = tenLoaiMon;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteLoaiMon(string tenLoaiMon)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {
                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.deleteLoaiMonAn @TenLoaiMonAn", sql_cn);
                sql_cm.Parameters.Add("@TenLoaiMonAn", SqlDbType.NVarChar, 30).Value = tenLoaiMon;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }

    }
}
