using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public class HoaDonDAO
    {
        public string sql_data = @"Data Source=.\sqlexpress;Initial Catalog=QuanLiQuanAn;Integrated Security=True";
        private static HoaDonDAO instance;
        public static HoaDonDAO Instance
        {
            get { if (instance == null) instance = new HoaDonDAO(); return HoaDonDAO.instance; }
            private set { HoaDonDAO.instance = value; }
        }
        public HoaDonDAO() {}
        public List<HoaDon> themHoaDon(string query)
        {
            List<HoaDon> HD= new List<HoaDon>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);
            foreach (DataRow drow in data.Rows)
            {
                HoaDon h_d= new HoaDon(drow);
                HD.Add(h_d);
            }
            data.Clear();
            return HD;
        }
        public int InsertHoaDon(DateTime? Date, int MaBanAn, int ThanhTien, int MaNv, string ThanhToan, int SlNguoi)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.insert_HoaDon @Date,@MaBanAn,@ThanhToan,@ThanhTien,@MaNv,@SlNguoi", sql_cn);
                sql_cm.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value = MaBanAn;
                sql_cm.Parameters.Add("@ThanhToan", SqlDbType.NVarChar, 20).Value = ThanhToan;
                sql_cm.Parameters.Add("@ThanhTien", SqlDbType.Int).Value = ThanhTien;
                sql_cm.Parameters.Add("@MaNv", SqlDbType.Int).Value = MaNv;
                sql_cm.Parameters.Add("@SlNguoi", SqlDbType.Int).Value = SlNguoi;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int InsertHoaDon(DateTime? Date, int MaBanAn,  int MaNv, string ThanhToan, int SlNguoi)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.insert_HoaDon @Date,@MaBanAn,@ThanhToan,@ThanhTien,@MaNv,@SlNguoi", sql_cn);
                sql_cm.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value = MaBanAn;
                sql_cm.Parameters.Add("@ThanhToan", SqlDbType.NVarChar, 20).Value = ThanhToan;
                sql_cm.Parameters.Add("@ThanhTien", SqlDbType.Int).Value = 0;
                sql_cm.Parameters.Add("@MaNv", SqlDbType.Int).Value = MaNv;
                sql_cm.Parameters.Add("@SlNguoi", SqlDbType.Int).Value = SlNguoi;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int updateHoaDon(int maHoaDon)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_hoadon @MaHoaDon", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int updateHoaDon2(int maHoaDon,int maBan, int maNv,int Sl,string thanhtoan,DateTime? date)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.update_hoadon2 @MaHoaDon,@MaBan,@MaNhanVien,@Date,@SoLuong, @ThanhToan", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                sql_cm.Parameters.Add("@MaBan", SqlDbType.Int).Value = maBan;
                sql_cm.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = maNv;
                sql_cm.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                sql_cm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Sl;
            //    sql_cm.Parameters.Add("@Gia", SqlDbType.Int).Value = tt;
                sql_cm.Parameters.Add("@ThanhToan", SqlDbType.NVarChar,30).Value = thanhtoan;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteHoaDonByMaHd(int maHoaDon)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_hoadonbyMahd @MaHoaDon", sql_cn);
                sql_cm.Parameters.Add("@MaHoaDon", SqlDbType.Int).Value = maHoaDon;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteHoaDonByMaban(int maban)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_hoadonbyMaBan @MaBanAn", sql_cn);
                sql_cm.Parameters.Add("@MaBanAn", SqlDbType.Int).Value = maban;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteHoaDonByManv(int manv)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_hoadonbyMaBan @MaNhanVien", sql_cn);
                sql_cm.Parameters.Add("@MaNhanVien", SqlDbType.Int).Value = manv;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
        public int deleteHoaDonByNgay(DateTime date)
        {
            int num = 0;
            using (SqlConnection sql_cn = new SqlConnection(sql_data))
            {

                sql_cn.Open();
                SqlCommand sql_cm = new SqlCommand("exec dbo.delete_hoadonbyngay @NgayThanhToan", sql_cn);
                sql_cm.Parameters.Add("@NgayThanhToan", SqlDbType.Date).Value = date;
                num = sql_cm.ExecuteNonQuery();
                sql_cn.Close();
            }
            return num;
        }
    }
}
