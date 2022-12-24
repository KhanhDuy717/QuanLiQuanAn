using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Graph;
using Application = System.Windows.Forms.Application;

namespace QuanLiQuanAn
{

    public partial class Admin : Form
    {
        public string qry = "select ct.MaHoaDon,MonAn.TenMonAn,ct.SoLuongMon,(ct.SoLuongMon*MonAn.Gia)as ThanhTien from chitiethoadon  as ct inner join hoadon on ct.MaHoaDon=HoaDon.MaHoaDon inner join MonAn on MonAn.MaMonAn=ct.MaMonAn ";
        List<int> lmanv = new List<int>();
        List<int> lmaban = new List<int>();
        List<int> xoamanv = new List<int>();
        List<int> xoamaban = new List<int>();
        List<int> xoamahd = new List<int>();

        public int maban;
        public int Save_maMonAn = 0;
        public Admin()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            groupBox9.Visible = false;
            groupBox10.Visible = false;
            groupBox11.Visible = false;
            groupBox12.Visible = false;
            groupBox13.Visible = false;
            groupBox14.Visible = false;
            groupBox15.Visible = false;
            groupBox16.Visible = false;
            groupBox17.Visible = false;
            groupBox18.Visible = false;
            groupBox19.Visible = false;
            groupBox20.Visible = false;
            groupBox21.Visible = false;
            dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql("select * from loaimonan ");
            dtgvMonAn.DataSource = Database.Instance.KetNoiSql("select * from monan");
            listLoaiMon("select * from loaimonan", cmbLLoai, 2);
            listLoaiMon("select * from loaimonan", cmbMaLoai, 0);
            listTenMon("select * from monan", cmbXoaMon);
            listTenMon("select * from monan", cmbCapNhatMon);
            cmbLGia.SelectedIndex = cmbLGia.Items.IndexOf("Tất cả");
            cmbSx.SelectedIndex = cmbSx.Items.IndexOf("Mã món ăn");
            cmbTG.SelectedIndex = cmbTG.Items.IndexOf("Tăng");
            cmbTgLoaiMon.SelectedIndex = 0;
            cmbSxLoaiMon.SelectedIndex = 0;
            //món ăn
            txtGia.TextChanged += (o, e) =>
            {
                int num = 0;
                if (int.TryParse(txtGia.Text, out num) == false)
                {
                    MessageBox.Show("Vui lòng nhập giá lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGia.Text = "";
                    txtGia.Focus();
                }

            };
            txtTenMon.Leave += (o, e) =>
            {

                if (Database.Instance.KetNoiSql($"select * from monan where tenmonan=N'{txtTenMon.Text}'").Rows.Count > 0)
                {
                    MessageBox.Show("Tên món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenMon.Clear();
                    txtTenMon.Focus();
                }

                else if (txtTenMon.Text.Length > 16)
                {
                    MessageBox.Show("Tên món đã nhiều hơn 16 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenMon.Clear();
                    txtTenMon.Focus();
                }
            };
            btnDatLai.Click += (o, e) =>
              {
                  cmbLGia.SelectedIndex = cmbLGia.Items.IndexOf("Tất cả");
                  cmbSx.SelectedIndex = cmbSx.Items.IndexOf("Mã món ăn");
                  cmbTG.SelectedIndex = cmbTG.Items.IndexOf("Tăng");
                  cmbLLoai.SelectedIndex = cmbLLoai.Items.IndexOf("Tất cả");
              };
            cmbLGia.SelectedIndexChanged += (o, e) =>
             {
                 boLocMonAn();
             };
            cmbLLoai.SelectedIndexChanged += (o, e) =>
            {
                boLocMonAn();
            };
            cmbSx.SelectedIndexChanged += (o, e) =>
            {
                boLocMonAn();
            };
            cmbTG.SelectedIndexChanged += (o, e) =>
            {
                boLocMonAn();
            };
            dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan order by maloaimonan asc");
            btnChon.Click += (o, e) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg)|*.jpg";

                if (open.ShowDialog() == DialogResult.OK)
                {

                    ptbMonAn.Image = new Bitmap(open.FileName);

                }
            };
            btnCnAnh.Click += (o, e) =>
            {
                picCnAnh.Image.Dispose();
                picCnAnh.Image = null;
             
                  OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg)|*.jpg";

                if (open.ShowDialog() == DialogResult.OK)
                {

                    picCnAnh.Image = new Bitmap(open.FileName);

                }
            };
            //txtCapNhatMon.Leave += (o, e) =>
            //{
            //    if (Database.Instance.KetNoiSql($"select * from monan where tenmonan=N'{txtCapNhatMon.Text}'").Rows.Count > 0)
            //    {
            //        MessageBox.Show("Tên món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtCapNhatMon.Clear();
            //    }

            //};
            btnThem.Click += (o, e) =>
            {
                if (ptbMonAn.Image == null || txtGia.Text == " " || txtTenMon.Text == " ")
                {
                    MessageBox.Show("Vui lòng không để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtTenMon.Text.Length > 16)
                {
                    MessageBox.Show("Tên món đã nhiều hơn 16 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenMon.Clear();
                    txtTenMon.Focus();
                }
                else if (Database.Instance.KetNoiSql($"select * from monan where tenmonan=N'{txtTenMon.Text}'").Rows.Count > 0)
                {
                    MessageBox.Show("Tên món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenMon.Clear();
                    txtTenMon.Focus();
                }
                else
                {
                   
                    
                    DataTable data = new DataTable();

                    List<MonAn> ma_mon  = MonAnDAO.Instance.themDsMon("select top 1* from monan order by MaMonAn desc");
                    ptbMonAn.Image.Save(Application.StartupPath + "\\Mon\\mon" + (ma_mon[0].MaMonAn+1) + ".jpg");
                   
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql("select * from monan");
                    cmbLGia.SelectedIndex = cmbLGia.Items.IndexOf("Tất cả");
                    cmbSx.SelectedIndex = cmbSx.Items.IndexOf("Mã món ăn");
                    cmbTG.SelectedIndex = cmbTG.Items.IndexOf("Tăng");
                    cmbLLoai.SelectedIndex = cmbLLoai.Items.IndexOf("Tất cả");
                    listTenMon("select * from monan", cmbXoaMon);
                    listTenMon("select * from monan", cmbCapNhatMon);
                   string ten_anh = "mon"+(ma_mon[0].MaMonAn + 1) + ".jpg";
                    if (MonAnDAO.Instance.InsertMonAn(txtTenMon.Text, (cmbMaLoai.SelectedIndex) + 1, int.Parse(txtGia.Text),ten_anh) > 0)
                    {

                        MessageBox.Show("Thêm món thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    ma_mon.Clear();
                }
            };
            txtCapNhatGia.TextChanged += (o, e) =>
            {
                int gia = 0;
                if (int.TryParse(txtCapNhatGia.Text, out gia) == false)
                {
                    MessageBox.Show("Vui lòng nhập số ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCapNhatGia.Clear();
                }
            };

            cmbCapNhatMon.SelectedIndexChanged += (o, e) =>
            {
                picCnAnh.Image = null;
                string tenLmon = "";
                List<MonAn> lv_Ma = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbCapNhatMon.SelectedItem}'");
                List<LoaiMonAn> lv_LMa = LoaiMonAnDAO.Instance.themDsLoaiMon($"select loaimonan.* from monan inner join loaimonan on monan.maloaimonan = loaimonan.maloaimonan where TenMonAn =N'{cmbCapNhatMon.SelectedItem.ToString()}'");
                listLoaiMon("select * from loaimonan", cmbCapNhatLoaiMon, 0);
                foreach (LoaiMonAn lma in lv_LMa)
                {
                    tenLmon = lma.TenLoaiMonAn;
                }
                foreach (MonAn ma in lv_Ma)
                {
                    txtCapNhatMon.Text = ma.TenMonAn;
                    txtCapNhatGia.Text = ma.Gia.ToString();
                    cmbCapNhatLoaiMon.SelectedIndex = cmbCapNhatLoaiMon.Items.IndexOf(tenLmon);
                    Save_maMonAn = ma.MaMonAn;
                }
                picCnAnh.Image = new Bitmap(Application.StartupPath + "\\Mon\\" +lv_Ma[0].Anh) ;
                lv_LMa.Clear();
                lv_Ma.Clear();
            };
            //món ăn

            //bàn ăn
            dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
            cmbTgBanAn.SelectedIndex = 0;
            cmbTrangThaiBanAn.SelectedIndex = 0;
            cmbSxBanAn.SelectedIndex = 0;
            cmbTrangThaiBanAn.SelectedIndexChanged += (o, e) =>
            {
                bocLocBanAn();
            };
            cmbTgBanAn.SelectedIndexChanged += (o, e) =>
            {
                bocLocBanAn();
            };
            cmbSxBanAn.SelectedIndexChanged += (o, e) =>
            {
                bocLocBanAn();
            };
            cmbChonBan.SelectedIndexChanged += (o, e) =>
            {
                txtCnTenBan.Text = cmbChonBan.SelectedItem.ToString();
                laySoGhe(txtCnTenBan.Text);
            };
            //txtTenBanAn.Leave += (o, e) =>
            //{
            //    if (Database.Instance.KetNoiSql($"select * from banan where tenbanan=N'{txtTenBanAn.Text}'").Rows.Count > 0)
            //    {
            //        MessageBox.Show("Tên bàn đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtTenBanAn.Clear();
            //    }
            //};



            btnXnThemBan.Click += (o, e) =>
            {
         
                if (Database.Instance.KetNoiSql($"select * from banan where tenbanan=N'{txtTenBanAn.Text}'").Rows.Count > 0)
                {
                    MessageBox.Show("Tên bàn đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenBanAn.Clear();
                }
                else
                {
                    if (BanAnDAO.Instance.insertBanAn(txtTenBanAn.Text) > 0)
                    {
                        MessageBox.Show("Đã thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                        bocLocBanAn();
                    }
                }





            };

            //txtCnTenBan.TextChanged += (o, e) =>
            //{
            //    if (txtCnTenBan.Text.Equals(cmbChonBan.SelectedItem.ToString()) == false)
            //    {
            //        if (Database.Instance.KetNoiSql($"select * from banan where tenbanan=N'{txtCnTenBan.Text}'").Rows.Count > 0)
            //        {
            //            MessageBox.Show("Tên bàn đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtCnTenBan.Text = cmbChonBan.SelectedItem.ToString();
            //            txtCnTenBan.Focus();
            //        }
            //    }
            //};
            txtCnSL.TextChanged += (o, e) =>
            {
                int num = 0;
                if (int.TryParse(txtCnSL.Text, out num) == false || num > 4)
                {
                    MessageBox.Show("Vui lòng nhập số và nhỏ hơn hoặc bằng 4", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCnSL.Undo();
                    txtCnSL.ClearUndo();
                    txtCnSL.Focus();
                }

            };

            btnXnCapNhat.Click += (o, e) =>
            {
                if (txtCnTenBan.Text == " ")
                {
                    MessageBox.Show("Vui lòng nhập tên bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (txtCnTenBan.Text.Equals(cmbChonBan.SelectedItem.ToString()) == false)
                    {
                        List<BanAn> ban = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbChonBan.SelectedItem}'");

                        if (Database.Instance.KetNoiSql($"select * from banan where tenbanan=N'{txtCnTenBan.Text}' and mabanan<>{ban[0].MaBanAn}").Rows.Count <= 0)
                        {
                            if (BanAnDAO.Instance.updateBanAn(txtCnTenBan.Text, maban, int.Parse(txtCnSL.Text)) <= 0)
                            {
                                MessageBox.Show("Đã cập nhật thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                                bocLocBanAn();
                                themBan(cmbChonBan, "select * from banan");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên bàn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        ban.Clear();
                    }
                    else if (txtCnTenBan.Text.Equals(cmbChonBan.SelectedItem.ToString()) == true)

                    {
                        if (BanAnDAO.Instance.updateBanAn(txtCnTenBan.Text, maban, int.Parse(txtCnSL.Text)) > 0)
                        {
                            MessageBox.Show("Đã cập nhật thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                            bocLocBanAn();
                            themBan(cmbChonBan, "select * from banan");
                        }
                    }
                }
            };

            btnXnXoaBan.Click += (o, e) =>
            {
                laySoGhe(cmbXoaBan.SelectedItem.ToString());
                if (BanAnDAO.Instance.deleteBanAn(maban) > 0)
                {
                    MessageBox.Show("Đã xoá thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                    bocLocBanAn();
                    themBan(cmbXoaBan, "select * from banan");
                }
            };

            cmbXoaBan.SelectedIndexChanged += (o, e) =>
            {
                laySoGhe(cmbXoaBan.SelectedItem.ToString());
            };

            //bàn ăn

            //LoaiMonAn 
            //txtThemTenLMon.Leave += (o, e) =>
            //{
            //    if (Database.Instance.KetNoiSql($"select * from loaimonan where tenloaimonan=N'{txtThemTenLMon.Text}'").Rows.Count > 0)
            //    {
            //        MessageBox.Show("Tên loại món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtThemTenLMon.Clear();
            //    }

            //};

            btnThemLoaiMon.Click += (o, e) =>
            {
                if (txtThemTenLMon.Text == "")
                {
                    MessageBox.Show("Vui lòng điền tên loại món ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (Database.Instance.KetNoiSql($"select * from loaimonan where tenloaimonan=N'{txtThemTenLMon.Text}'").Rows.Count > 0)
                {
                    MessageBox.Show("Tên loại món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtThemTenLMon.Clear();
                }
                else
                {
                    if (LoaiMonAnDAO.Instance.themLmon(txtThemTenLMon.Text) > 0)
                    {
                        MessageBox.Show("Đã thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan");
                    }
                }
            };

            cmbCnLoaiMon.SelectedIndexChanged += (o, e) =>
            {
                txtCnTenLMon.Text = cmbCnLoaiMon.Items[cmbCnLoaiMon.SelectedIndex].ToString();

            };
            btnCnTenLoaiMon.Click += (o, e) =>
            {
                if (txtCnTenLMon.Text == " ")
                {
                    MessageBox.Show("Vui lòng nhập tên loại món", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (txtCnTenLMon.Text.Equals(cmbCnLoaiMon.SelectedItem.ToString()) == false)
                {
                    List<LoaiMonAn> lmon = LoaiMonAnDAO.Instance.themDsLoaiMon($"select * from loaimonan where tenloaimonan=N'{cmbCnLoaiMon.SelectedItem}'");

                    if (Database.Instance.KetNoiSql($"select * from loaimonan where tenloaimonan=N'{txtCnTenLMon.Text}' and maloaimonan<>{lmon[0].MaLoaiMonAn}").Rows.Count <= 0)
                    {

                        if (LoaiMonAnDAO.Instance.updateLoaiMon(txtCnTenLMon.Text, cmbCnLoaiMon.SelectedItem.ToString()) > 0)
                        {
                            MessageBox.Show("Đã cập nhật thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDsLoaiMon(cmbCnLoaiMon);
                            dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên loại món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCnTenLMon.Text = cmbCnLoaiMon.SelectedItem.ToString();
                    }
                    lmon.Clear();
                }

                else if (txtCnTenLMon.Text.Equals(cmbCnLoaiMon.SelectedItem.ToString()) == true)
                {
                    if (LoaiMonAnDAO.Instance.updateLoaiMon(txtCnTenLMon.Text, cmbCnLoaiMon.SelectedItem.ToString()) > 0)
                    {
                        MessageBox.Show("Đã cập nhật thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDsLoaiMon(cmbCnLoaiMon);
                        dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan");
                    }
                }

            };
            btnXoaLoaiM.Click += (o, e) =>
            {
                if (LoaiMonAnDAO.Instance.deleteLoaiMon(cmbXoaLoaiMon.SelectedItem.ToString()) > 0)
                {
                    MessageBox.Show("Đã xoá thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDsLoaiMon(cmbXoaLoaiMon);
                    dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan");
                }
            };
            cmbSxLoaiMon.SelectedIndexChanged += (o, e) =>
            {
                sxLoaiMonAn();
            };
            cmbTgLoaiMon.SelectedIndexChanged += (o, e) =>
            {
                sxLoaiMonAn();
            };
            //endLoaiMon

            //Hoadon
            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
            cmbSxNew.SelectedIndex = 0;
            cmbLocThanhToanNew.SelectedIndex = 0;
            cmbSxTgNew.SelectedIndex = 0;
            listBan("select * from banan");
            listManv("select * from nhanvien");
            dtpLichSuNew.ValueChanged += (o, e) =>
            {
                boloc();
            };

            cmbBanNew.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbManvNew.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            ckbAllNew.CheckedChanged += (o, e) =>
            {
                boloc();
            };
            cmbSx.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbSxTgNew.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbLocThanhToanNew.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            btnDatLaiNew.Click += (o, e) =>
            {
                ckbAllNew.Checked = true;
                cmbManvNew.SelectedIndex = cmbManvNew.Items.IndexOf("Tất cả");
                cmbBanNew.SelectedIndex = cmbBanNew.Items.IndexOf("Tất cả");
                cmbSxNew.SelectedIndex = cmbSxNew.Items.IndexOf("Mã hoá đơn");
                cmbLocThanhToanNew.SelectedIndex = 0;
                cmbSxTgNew.SelectedIndex = 0;
            };
            //txtThanhTienHD.Leave += (o, e) =>
            //{
            //    int num = 0;

            //    if (int.TryParse(txtThanhTienHD.Text, out num) == false && txtThanhTienHD.Text.Equals("") == false)
            //    {
            //        MessageBox.Show("Vui nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //};

            numSLNguoi.ValueChanged += (o, e) =>
            {

                List<BanAn> banAn = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbBanAnHd.SelectedItem}'");
                foreach (BanAn B_a in banAn)
                {
                    if (numSLNguoi.Value > (4 - B_a.SoKhachNgoi))
                    {
                        MessageBox.Show("Đã vuợt quá số ghế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        numSLNguoi.Value = numSLNguoi.Value - 1;
                    }
                }
                banAn.Clear();
            };
           

            btnXnThemHD.Click += (o, e) =>
            {

                //if (txtThanhTienHD.Text.Equals(""))
                //{
                //    MessageBox.Show("Vui lòng không bỏ trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtThanhTienHD.Select();
                //}
                //else
                //{
                List<BanAn> ban = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbBanAnHd.SelectedItem.ToString()}'");
                if (ban[0].SoKhachNgoi == 4)
                {
                    MessageBox.Show("Bàn đã hết chổ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
              
                else if (HoaDonDAO.Instance.InsertHoaDon(DateTime.UtcNow.Date, lmaban[cmbBanAnHd.SelectedIndex], lmanv[cmbNhanVienHd.SelectedIndex], cmbTTHoaDon.SelectedItem.ToString(), ((int)numSLNguoi.Value)) > 0)
                {

                    MessageBox.Show("Thêm hoá đơn thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BanAnDAO.Instance.updateSoKhach(int.Parse(numSLNguoi.Value.ToString()), lmaban[cmbBanAnHd.SelectedIndex]);
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                    dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                    themBan(cmbBanAnHd, "select * from banan where sokhachngoi<4");
                }

                themBan(cmbBanAnHd, "select * from banan where sokhachngoi<4");

                //}
            };
            cmbXoaTheo.SelectedIndexChanged += (o, e) =>
            {
                cmbDanhSachXoa.Items.Clear();
                xoamaban.Clear();
                xoamahd.Clear();
                xoamanv.Clear();
                if (cmbXoaTheo.SelectedIndex == 0)
                {

                    cmbDanhSachXoa.Visible = true;
                    dtpkXoaNgay.Visible = false;
                    List<HoaDon> hd = HoaDonDAO.Instance.themHoaDon($"select * from hoadon");
                    foreach (HoaDon HD in hd)
                    {
                        cmbDanhSachXoa.Items.Add(HD.MaHoaDon);
                        xoamahd.Add(HD.MaHoaDon);
                    }
                    hd.Clear();
                    cmbDanhSachXoa.SelectedIndex = 0;
                }
                else if (cmbXoaTheo.SelectedIndex == 1)
                {

                    cmbDanhSachXoa.Visible = false;
                    dtpkXoaNgay.Location = new Point(106, 47);
                    dtpkXoaNgay.Visible = true;

                }
                else if (cmbXoaTheo.SelectedIndex == 2)
                {
                    cmbDanhSachXoa.Visible = true;
                    dtpkXoaNgay.Visible = false;
                    List<BanAn> ba = BanAnDAO.Instance.themDsBan($"select  distinct banan.tenbanan,hoadon.mabanan,banan.sokhachngoi  from banan inner join hoadon on hoadon.mabanan=banan.mabanan");
                    foreach (BanAn BA in ba)
                    {
                        cmbDanhSachXoa.Items.Add(BA.TenBanAn);
                        xoamaban.Add(BA.MaBanAn);
                    }
                    ba.Clear();
                    cmbDanhSachXoa.SelectedIndex = 0;
                }
                else if (cmbXoaTheo.SelectedIndex == 3)
                {
                    cmbDanhSachXoa.Visible = true;
                    dtpkXoaNgay.Visible = false;
                    List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien($"select  distinct nhanvien.tennhanvien, nhanvien.manhanvien,nhanvien.machucvu,nhanvien.matkhau from nhanvien inner join hoadon on nhanvien.manhanvien=hoadon.manhanvien");
                    foreach (NhanVien NV in nv)
                    {
                        cmbDanhSachXoa.Items.Add(NV.TenNhanVien);
                        xoamanv.Add(NV.MaNhanVien);
                    }
                    nv.Clear();
                    cmbDanhSachXoa.SelectedIndex = 0;

                }

            };
            btnXnXoaHd.Click += (o, e) =>
            {
                if (MessageBox.Show("Bạn có muốn xoá hoá đơn", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    if (cmbXoaTheo.SelectedIndex == 0)
                    {
                        if (HoaDonDAO.Instance.deleteHoaDonByMaHd(xoamahd[cmbDanhSachXoa.SelectedIndex]) > 0)
                        {
                            MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            boloc();
                            cmbXoaTheo.SelectedIndex = cmbXoaTheo.Items.IndexOf(cmbXoaTheo.SelectedItem.ToString());
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                        }
                    }
                    else if (cmbXoaTheo.SelectedIndex == 1)
                    {
                        if (HoaDonDAO.Instance.deleteHoaDonByNgay(dtpkXoaNgay.Value) > 0)
                        {
                            MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            boloc();
                            cmbXoaTheo.SelectedIndex = cmbXoaTheo.Items.IndexOf(cmbXoaTheo.SelectedItem.ToString());
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                        }
                    }
                    else if (cmbXoaTheo.SelectedIndex == 2)
                    {
                        if (HoaDonDAO.Instance.deleteHoaDonByMaban(xoamaban[cmbDanhSachXoa.SelectedIndex]) > 0)
                        {
                            MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            boloc();
                            cmbXoaTheo.SelectedIndex = cmbXoaTheo.Items.IndexOf(cmbXoaTheo.SelectedItem.ToString());
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                        }
                    }
                    else if (cmbXoaTheo.SelectedIndex == 3)
                    {
                        if (HoaDonDAO.Instance.deleteHoaDonByManv(xoamanv[cmbDanhSachXoa.SelectedIndex]) > 0)
                        {
                            MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            boloc();
                            cmbXoaTheo.SelectedIndex = cmbXoaTheo.Items.IndexOf(cmbXoaTheo.SelectedItem.ToString());
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                        }

                    }
                }
                dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
            };
            cmbCapNhatHd.SelectedIndexChanged += (o, e) =>
            {
                List<HoaDon> hd = HoaDonDAO.Instance.themHoaDon($"select * from hoadon where mahoadon={int.Parse(cmbCapNhatHd.SelectedItem.ToString())}");
                foreach (HoaDon HD in hd)
                {
                    dtptCapNhatNgay.Value = (DateTime)HD.NgayThanhToan;
                    List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan($"select * from banan inner join hoadon on hoadon.mabanan=banan.mabanan where hoadon.mabanan={HD.MaBanAn}");
                    cmbCapNhatBanan.SelectedItem = Ban_an[0].TenBanAn;
                    Ban_an.Clear();
                    numCatNhapSl.Value = HD.SlNguoi;
                    List<NhanVien> Nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien inner join hoadon on hoadon.manhanvien=nhanvien.manhanvien where hoadon.manhanvien={HD.MaNhanVien}");
                    cmbCapNhatNV.SelectedItem = Nv[0].TenNhanVien;
                    Nv.Clear();
                    if (HD.ThanhToan.Equals("Chưa"))
                    {
                        cmbCapNhatThanhToan.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbCapNhatThanhToan.SelectedIndex = 0;
                    }
                    // txtCapNhatTien.Text = HD.ThanhTien.ToString();
                }
                hd.Clear();
            };
            cmbCapNhatBanan.SelectedIndexChanged += (o, e) =>
            {
                List<BanAn> banAn = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbCapNhatBanan.SelectedItem.ToString()}'");
                //List<BanAn> MBA = BanAnDAO.Instance.themDsBan($"select banan.mabanan, banan.tenbanan, banan.sokhachngoi from banan inner join hoadon on hoadon.mabanan=banan.mabanan where hoadon.mahoadon={int.Parse(cmbCapNhatHd.SelectedItem.ToString())}");
                //foreach (BanAn B_a in banAn)
                //{
                //    if (B_a.SoKhachNgoi>=4)
                //    {
                //        MessageBox.Show("Bàn này đã đầy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        cmbCapNhatBanan.SelectedItem = MBA[0].TenBanAn;
                //    }
                //}

                numCatNhapSl.Value = 1;
                numCatNhapSl.Maximum = 4 - banAn[0].SoKhachNgoi+1;
            };
            numCatNhapSl.ValueChanged += (o, e) =>
            {
                List<BanAn> banAn = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbCapNhatBanan.SelectedItem}'");
                List<HoaDon> Hd = HoaDonDAO.Instance.themHoaDon($"select * from hoadon where mahoadon={int.Parse(cmbCapNhatHd.SelectedItem.ToString())}");
                    //if (numCatNhapSl.Value > (4 - banAn[0].SoKhachNgoi))
                    //{
                    //    MessageBox.Show("Đã vuợt quá số ghế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    numCatNhapSl.Value = numCatNhapSl.Value - 1;
                    //}
                
                Hd.Clear();
                banAn.Clear();
            };

            btnCapNhatHoaDon.Click += (o, e) =>
            {
                List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{cmbCapNhatBanan.SelectedItem.ToString()}'");
                List<NhanVien> Nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien where tennhanvien=N'{cmbCapNhatNV.SelectedItem.ToString()}'");
                List<HoaDon> Hd = HoaDonDAO.Instance.themHoaDon($"select * from hoadon where mahoadon={int.Parse(cmbCapNhatHd.SelectedItem.ToString())}");
                if (HoaDonDAO.Instance.updateHoaDon2(int.Parse(cmbCapNhatHd.SelectedItem.ToString()), Ban_an[0].MaBanAn, Nv[0].MaNhanVien, int.Parse(numCatNhapSl.Value.ToString()), /*int.Parse(txtCapNhatTien.Text),*/ cmbCapNhatThanhToan.SelectedItem.ToString(), DateTime.UtcNow.Date) > 0)
                {
                    MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<HoaDon> Hd2 = HoaDonDAO.Instance.themHoaDon($"select * from hoadon where mahoadon={int.Parse(cmbCapNhatHd.SelectedItem.ToString())}");
                    if (Hd[0].ThanhToan.Equals("Đã thanh toán") == true && Hd2[0].ThanhToan.Equals("Chưa") == true)
                    {
                        //     BanAnDAO.Instance.updateSoKhachRaVe(Hd[0].SlNguoi, Ban_an[0].MaBanAn);
                        BanAnDAO.Instance.updateSoKhach(int.Parse(numCatNhapSl.Value.ToString()), Ban_an[0].MaBanAn);
                    }
                    else if (Hd[0].ThanhToan.Equals("Đã thanh toán") == false && Hd2[0].ThanhToan.Equals("Chưa") == false)
                    {
                        BanAnDAO.Instance.updateSoKhachRaVe(Hd[0].SlNguoi, Ban_an[0].MaBanAn);
                        //  BanAnDAO.Instance.updateSoKhach(int.Parse(numCatNhapSl.Value.ToString()), Ban_an[0].MaBanAn);
                        ///   Hd[0].SlNguoi != int.Parse(numCatNhapSl.Value.ToString()) &&
                        
                    }
                    else if (Hd[0].SlNguoi != int.Parse(numCatNhapSl.Value.ToString()) && Hd[0].ThanhToan.Equals("Đã thanh toán") == false && Hd2[0].ThanhToan.Equals("Chưa") == true)
                    {
                        BanAnDAO.Instance.updateSoKhachRaVe(Hd[0].SlNguoi, Ban_an[0].MaBanAn);
                        BanAnDAO.Instance.updateSoKhach(int.Parse(numCatNhapSl.Value.ToString()), Ban_an[0].MaBanAn);

                    }

                    //else if (Hd[0].SlNguoi == int.Parse(numCatNhapSl.Value.ToString()) && Hd2[0].ThanhToan.Equals("Đã thanh toán") == true)
                    //{
                    //    BanAnDAO.Instance.updateSoKhachRaVe(Hd[0].SlNguoi, Ban_an[0].MaBanAn);
                    //    boloc();
                    //} 
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql("select * from banan");
                    dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                    boloc();
                    Hd2.Clear();
                }

                Nv.Clear();
                Ban_an.Clear();
                Hd.Clear();
            };
            //Hoadon

            //cthd
            if (Database.Instance.KetNoiSql("select * from hoadon").Rows.Count > 0)
            {
                listMaHD(cmbChonHd, "select * from hoadon");
                loaddsMon(cmbChonMonAn);
                cmbChonMonAn.SelectedIndex = 0;
                cmbChonHd.SelectedIndex = 0;
                cmbSxCthd.SelectedIndex = 0;
                cmbTgCthd.SelectedIndex = 0;
                dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry);
                boLocCTHD();
          
                cmbSxCthd.SelectedIndexChanged += (o, e) =>
            {
                boLocCTHD();
            };
            cmbTgCthd.SelectedIndexChanged += (o, e) =>
            {
                boLocCTHD();
            };
            cmbChonMonAn.SelectedIndexChanged += (o, e) =>
            {
                boLocCTHD();
            };
            cmbChonHd.SelectedIndexChanged += (o, e) =>
            {
                boLocCTHD();
            };
            btnXnThemCTHD.Click += (o, e) =>
            {

                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbThemMonCTHD.SelectedItem.ToString()}'");
                List<CTHD> cthd = CTHDDAO.Instance.cthd($"select * from chitiethoadon where mamonan={mon[0].MaMonAn}");

                if (Database.Instance.KetNoiSql($"select * from chitiethoadon where mamonan={mon[0].MaMonAn} and mahoadon={int.Parse(cmbCapNhatCTHD.SelectedItem.ToString())}").Rows.Count > 0)
                {

                    if (CTHDDAO.Instance.update_sl_cthd(int.Parse(cmbChonHDThem.SelectedItem.ToString()), mon[0].MaMonAn, int.Parse(numThemSlCTHD.Value.ToString()), cthd[0].SoLuong) > 0)
                    {
                        MessageBox.Show("Đã thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        boLocCTHD();

                    }
                }
                else
                {
                    if (CTHDDAO.Instance.InsertCTHD2(int.Parse(cmbChonHDThem.SelectedItem.ToString()), mon[0].MaMonAn, int.Parse(numThemSlCTHD.Value.ToString())) > 0)
                    {
                        MessageBox.Show("Đã thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        boLocCTHD();

                    }
                }

                dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                mon.Clear();
                cthd.Clear();
            };
            themMaHD(cmbCapNhatCTHD, "select * from hoadon");

            loaddsMon2(cmbTenMonCu, int.Parse(cmbCapNhatCTHD.SelectedItem.ToString()));
            cmbCapNhatCTHD.SelectedIndexChanged += (o, e) =>
            {

                loaddsMon2(cmbTenMonCu, int.Parse(cmbCapNhatCTHD.SelectedItem.ToString()));
                listTenMon("select * from monan ", cmbMonMoi);
            };
            btnXacNhanCnCTHD.Click += (o, e) =>
            {
                int sl = 0;
                List<MonAn> monCu = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbTenMonCu.SelectedItem}'");
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbMonMoi.SelectedItem}'");
                List<CTHD> cthd = CTHDDAO.Instance.cthd($"select * from chitiethoadon where mahoadon={int.Parse(cmbCapNhatCTHD.SelectedItem.ToString())}");
                foreach (CTHD ct in cthd)
                {
                    if (ct.MaMonAn == monCu[0].MaMonAn)
                    {
                        sl = ct.SoLuong;
                        break;
                    }
                }

                if (monCu[0].MaMonAn == mon[0].MaMonAn)
                {
                    if (CTHDDAO.Instance.update_sl_cthd(int.Parse(cmbCapNhatCTHD.SelectedItem.ToString()), mon[0].MaMonAn, int.Parse(numCapNhatSlMonCTHD.Value.ToString()), sl) > 0)
                    {
                        MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        boLocCTHD();
                    }
                }
                else
                {
                    if (CTHDDAO.Instance.update_cthd(int.Parse(cmbCapNhatCTHD.SelectedItem.ToString()), monCu[0].MaMonAn, mon[0].MaMonAn, int.Parse(numThemSlCTHD.Value.ToString())) > 0)
                    {
                        MessageBox.Show("Đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        boLocCTHD();
                    }
                }
            };
            cmbXacNhanXoaMaHD.SelectedIndexChanged += (o, e) =>
            {

                loaddsMon2(cmbXacNhanXoaMon, int.Parse(cmbXacNhanXoaMaHD.SelectedItem.ToString()));

            };
            btnXacNhanXoaCTHD.Click += (o, e) =>
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbXacNhanXoaMon.SelectedItem.ToString()}'");

                if (CTHDDAO.Instance.update_tt_hd(int.Parse(cmbXacNhanXoaMaHD.SelectedItem.ToString()), mon[0].MaMonAn) > 0 && CTHDDAO.Instance.delete_cthd(int.Parse(cmbXacNhanXoaMaHD.SelectedItem.ToString()), mon[0].MaMonAn) > 0)
                {
                    MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
                    boLocCTHD();
                }
            };
            numThemSlCTHD.ValueChanged += (o, e) =>
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbThemMonCTHD.SelectedItem.ToString()}'");
                tinhTien(txtThemTien, numThemSlCTHD, mon[0].Gia);
            };
            cmbThemMonCTHD.SelectedIndexChanged += (o, e) =>
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbThemMonCTHD.SelectedItem.ToString()}'");
                tinhTien(txtThemTien, numThemSlCTHD, mon[0].Gia);
                txtThemGiaMon.Text = mon[0].Gia.ToString();
            };
            numCapNhatSlMonCTHD.ValueChanged += (o, e) =>
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbMonMoi.SelectedItem.ToString()}'");
                tinhTien(txtTienCapNhat, numCapNhatSlMonCTHD, mon[0].Gia);
            };
            cmbMonMoi.SelectedIndexChanged += (o, e) =>
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbMonMoi.SelectedItem.ToString()}'");
                tinhTien(txtTienCapNhat, numCapNhatSlMonCTHD, mon[0].Gia);
                txtThemGia.Text = mon[0].Gia.ToString();
            };
                //cthd
            }
            //cv
            txtTenCv.Leave += (o, e) =>
            {
                if (Database.Instance.KetNoiSql($"select * from chucvu where tenchucvu=N'{txtTenCv.Text}'").Rows.Count > 0)
                {
                    MessageBox.Show("Tên này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenCv.Text = " ";
                }
            };

            txtCNTenCv.Leave += (o, e) =>
            {
                List<ChucVu> cv = ChucVuDAO.Instance.themDsCV($"select * from chucvu where tenchucvu=N'{cmbChonCv.SelectedItem}'");

                if (Database.Instance.KetNoiSql($"select * from chucvu where tenchucvu=N'{txtCNTenCv.Text}' and  machucvu<>{cv[0].MaChucVu}").Rows.Count > 0)
                {
                    MessageBox.Show("Tên này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCNTenCv.Text = cmbChonCv.SelectedItem.ToString();
                }
            };

            btnThemTenCv.Click += (o, e) =>
            {
                if (txtTenCv.Text == null)
                {
                    MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else if (Database.Instance.KetNoiSql($"select * from chucvu where tenchucvu=N'{txtTenCv.Text}'").Rows.Count <= 0)
                {
                    if (ChucVuDAO.Instance.InsertCv(txtTenCv.Text) > 0)
                    {
                        MessageBox.Show("Đã thêm chức vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvChucVu.Columns.Clear();
                        dtgvChucVu.DataSource = Database.Instance.KetNoiSql("select * from chucvu");

                    }
                }
            };
            dtgvChucVu.DataSource = Database.Instance.KetNoiSql("select * from chucvu");
            cmbChonCv.SelectedIndexChanged += (o, e) =>
            {
                txtCNTenCv.Text = cmbChonCv.SelectedItem.ToString();
            };

            btnCNTenCv.Click += (o, e) =>
            {

                if (txtCNTenCv.Text == null)
                {
                    MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (txtCNTenCv.Text.Equals(cmbChonCv.SelectedItem.ToString()))
                {
                    MessageBox.Show("Đã cập nhật chức vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (Database.Instance.KetNoiSql($"select * from chucvu where tenchucvu=N'{ txtCNTenCv.Text}'").Rows.Count <= 0)
                {
                    if (ChucVuDAO.Instance.UpdateCv(cmbChonCv.SelectedItem.ToString(), txtCNTenCv.Text) > 0)
                    {
                        MessageBox.Show("Đã cập nhật chức vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvChucVu.Columns.Clear();
                        dtgvChucVu.DataSource = Database.Instance.KetNoiSql("select * from chucvu");
                        listCv(cmbChonCv, "select * from chucvu");
                    }
                }
            };
            btnXoaCv.Click += (o, e) =>
            {

                if (ChucVuDAO.Instance.deleteCv(cmbXoaTenCv.SelectedItem.ToString()) > 0)
                {
                    MessageBox.Show("Đã xoá chức vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvChucVu.Columns.Clear();
                    dtgvChucVu.DataSource = Database.Instance.KetNoiSql("select * from chucvu");
                    listCv(cmbXoaTenCv, "select * from chucvu");
                }

            };
            //cv
            //nv
            cmbSxNhanVien.SelectedIndex = 0;
            cmbTgNhanVien.SelectedIndex = 0;
            dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien");
            //nv
            listChucVu("select * from chucvu");
            bolocNhanVien();
            cmbSxNhanVien.SelectedIndexChanged += (o, e) =>
            {
                bolocNhanVien();

            };
            cmbTgNhanVien.SelectedIndexChanged += (o, e) =>
            {
                bolocNhanVien();

            };
            cmbChucVu.SelectedIndexChanged += (o, e) =>
            {
                bolocNhanVien();

            };
            txtTenNv.Leave += (o, e) =>
             {
                 if (Database.Instance.KetNoiSql($"select * from nhanvien where tennhanvien='{txtTenNv.Text}'").Rows.Count > 0)
                 {
                     MessageBox.Show("Tên nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     txtTenNv.Clear();
                     txtTenNv.Focus();
                 }
             };

            txtMatKhau.Leave += (o, e) =>
             {
                 string pass = txtMatKhau.Text.ToUpper();
                 int check = 0;
                 if (txtMatKhau.Text.Length != 0)
                 {

                     foreach (char c in txtMatKhau.Text)
                     {
                         if (System.Convert.ToInt32(c) > 191)
                         {
                             MessageBox.Show("Mật khẩu không được có dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             txtTenNv.Focus();
                             check = 1;
                             break;
                         }
                     }
                     if (check != 1)
                     {
                         if (!txtMatKhau.Text.Contains('@') ||txtMatKhau.Text.Length < 8 || txtMatKhau.Text[0] != pass[0])
                         {
                             MessageBox.Show("Mật khẩu bao gồm kí tự @, viết hoa chữ cái đầu  và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         }
                         //else if (txtMatKhau.Text.Length < 8 && txtMatKhau.Text[0] != pass[0])
                         //{
                         //    MessageBox.Show("Mật khẩu cần viết hoa chữ cái đầu và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                         //else if (txtMatKhau.Text.Contains('@') && txtMatKhau.Text[0] != pass[0])
                         //{
                         //    MessageBox.Show("Mật khẩu bao gồm kí tự @ và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                         //else if (txtMatKhau.Text.Length < 8 && !txtMatKhau.Text.Contains('@'))
                         //{
                         //    MessageBox.Show("Mật khẩu bao gồm kí tự @ và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                         //else if (txtMatKhau.Text.Length < 8)
                         //{
                         //    MessageBox.Show("Mật khẩu cần tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                         //else if (!txtMatKhau.Text.Contains('@'))
                         //{
                         //    MessageBox.Show("Mật khẩu bao gồm kí tự @", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                         //else if (txtMatKhau.Text[0] != pass[0])
                         //{
                         //    MessageBox.Show("Mật khẩu cần viết hoa chữ cái đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //}
                     }
                 }
                 else
                 {
                     MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     txtMatKhau.Focus();
                 }
             };
            btnThemNhanVien.Click += (o, e) =>
            {
                int check = 0;
                foreach (char c in txtMatKhau.Text)
                {
                    if (System.Convert.ToInt32(c) > 191)
                    {
                        MessageBox.Show("Mật khẩu không được có dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTenNv.Focus();
                        check = 1;
                        break;
                    }
                }
                string pass = txtMatKhau.Text.ToUpper();
                if (txtMatKhau.Text.Length <= 0 || txtTenNv.Text.Length <= 0)
                {
                    MessageBox.Show("Mật khẩu và tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (!txtMatKhau.Text.Contains('@') && txtMatKhau.Text.Length < 8 && txtMatKhau.Text[0] != pass[0] || txtTenNv.Text == " " || check == 1)
                {
                    MessageBox.Show("Mật khẩu không đúng yêu cầu hoặc tên bị trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (Database.Instance.KetNoiSql($"select * from nhanvien where tennhanvien=N'{txtTenNv.Text}' ").Rows.Count > 0)
                    {
                        MessageBox.Show("Tên nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        List<ChucVu> Chuc_vu = ChucVuDAO.Instance.themDsCV($"select * from chucvu where tenchucvu='{cmbChucVuNv.SelectedItem.ToString()}'");
                        List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien("select top 1 * from nhanvien order by manhanvien desc ");
                        Bitmap bmp = new Bitmap(Application.StartupPath + "\\user\\user.png");
                        string img = "user" + (nv[0].MaNhanVien + 1) + ".png";
                        bmp.Save(Application.StartupPath + "\\user\\" + img);
                        if (NhanVienDAO.Instance.insert(txtTenNv.Text, txtMatKhau.Text, Chuc_vu[0].MaChucVu, img) > 0)
                        {
                            MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien");

                        }
                        Chuc_vu.Clear();
                    }
                }




            };
            cmbChonNhanVien.SelectedIndexChanged += (o, e) =>
            {

                txtCnTenNv.Text = cmbChonNhanVien.SelectedItem.ToString();
                List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien where tennhanvien=N'{cmbChonNhanVien.SelectedItem.ToString()}'");
                List<ChucVu> Chuc_vu = ChucVuDAO.Instance.themDsCV($"select * from chucvu where machucvu={nv[0].MaChucVu}");
                cmbCNCVNV.SelectedItem = Chuc_vu[0].TenChucVu;
                nv.Clear();
                Chuc_vu.Clear();
            };

            txtCnTenNv.Leave += (o, e) =>
            {
                List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien where tennhanvien=N'{cmbChonNhanVien.SelectedItem}'");

                if (cmbChonNhanVien.SelectedItem.ToString().Equals(txtCnTenNv.Text) == false)
                if (Database.Instance.KetNoiSql($"select * from nhanvien where tennhanvien='{ txtCnTenNv.Text}' and manhanvien<>{nv[0].MaNhanVien}").Rows.Count > 0)
                    {
                        MessageBox.Show("Tên nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCnTenNv.Clear();
                        txtCnTenNv.Focus();
                    }
                nv.Clear();
            };
            txtCNMatKhau.Leave += (o, e) =>
            {
                int check = 0;
                string pass = txtCNMatKhau.Text.ToUpper();
                if (txtCNMatKhau.Text.Length != 0)
                {
                    foreach (char c in txtCNMatKhau.Text)
                    {   if(System.Convert.ToInt32(c)>191)
                        { 
                            MessageBox.Show("Mật khẩu không được có dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTenNv.Focus();
                            check = 1;
                            break;
                        }
                    }
                    if (check != 1)
                    {
                        if (!txtCNMatKhau.Text.Contains('@')|| txtCNMatKhau.Text.Length < 8 || txtCNMatKhau.Text[0] != pass[0])
                        {
                            MessageBox.Show("Mật khẩu bao gồm kí tự @, viết hoa chữ cái đầu  và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        //else if (txtCNMatKhau.Text.Length < 8 && txtCNMatKhau.Text[0] != pass[0])
                        //{
                        //    MessageBox.Show("Mật khẩu cần viết hoa chữ cái đầu và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (txtCNMatKhau.Text.Contains('@') && txtCNMatKhau.Text[0] != pass[0])
                        //{
                        //    MessageBox.Show("Mật khẩu bao gồm kí tự @ và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (txtCNMatKhau.Text.Length < 8 && !txtCNMatKhau.Text.Contains('@'))
                        //{
                        //    MessageBox.Show("Mật khẩu bao gồm kí tự @ và tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (txtCNMatKhau.Text.Length < 8)
                        //{
                        //    MessageBox.Show("Mật khẩu cần tối thiểu 8 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (!txtCNMatKhau.Text.Contains('@'))
                        //{
                        //    MessageBox.Show("Mật khẩu bao gồm kí tự @", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (txtCNMatKhau.Text[0] != pass[0])
                        //{
                        //    MessageBox.Show("Mật khẩu cần viết hoa chữ cái đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }

                }
                else
                {
                    MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCNMatKhau.Focus();
                }
            };
            btnCNNhanVien.Click += (o, e) =>
            {
                int check = 0;
                foreach (char c in txtCNMatKhau.Text)
                {
                    if (System.Convert.ToInt32(c) > 191)
                    {
                        MessageBox.Show("Mật khẩu không được có dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTenNv.Focus();
                        check = 1;
                        break;
                    }
                }
                string pass = txtCNMatKhau.Text.ToUpper();
                if (txtCNMatKhau.Text.Length <= 0 || txtCnTenNv.Text.Length <= 0)
                {
                    MessageBox.Show("Mật khẩu và tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (!txtCNMatKhau.Text.Contains('@') && txtCNMatKhau.Text.Length < 8 && txtCNMatKhau.Text[0] != pass[0] || txtCnTenNv.Text == " "|| check==1)
                {
                    MessageBox.Show("Mật khẩu không đúng yêu cầu hoặc tên bị trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien where tennhanvien=N'{cmbChonNhanVien.SelectedItem.ToString()}'");
                    List<ChucVu> Chuc_vu = ChucVuDAO.Instance.themDsCV($"select * from chucvu where tenchucvu='{cmbCNCVNV.SelectedItem.ToString()}'");

                    if (NhanVienDAO.Instance.update(txtCnTenNv.Text, txtCNMatKhau.Text, Chuc_vu[0].MaChucVu, nv[0].MaNhanVien) > 0)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien");
                        listTenManv(cmbChonNhanVien);
                    }
                    nv.Clear();
                    Chuc_vu.Clear();
                }
            };
            btnXoaNv.Click += (o, e) =>
            {
                List<NhanVien> nv = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien where tennhanvien=N'{cmbXoaNhanVien.SelectedItem.ToString()}'");

                if (Database.Instance.KetNoiSql("select * from nhanvien where machucvu=1").Rows.Count <= 1 && nv[0].MaChucVu == 1)
                {
                    MessageBox.Show("Không thể xoá admin cuối cùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (NhanVienDAO.Instance.delete(nv[0].MaNhanVien) > 0)
                {
                    MessageBox.Show("Xoá nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien");
                    listTenManv(cmbXoaNhanVien);
                }

                nv.Clear();

            };
        }
        void bolocNhanVien()
        {
            string oder = "";
            if (cmbSxNhanVien.SelectedIndex == 0)
            {
                oder = "MaNhanVien";
            }
            else if (cmbSxNhanVien.SelectedIndex == 1)
            {
                oder = "TenNhanVien";
            }
            else if (cmbSxNhanVien.SelectedIndex == 2)
            {
                oder = "MatKhau";
            }
            else if (cmbSxNhanVien.SelectedIndex == 3)
                oder = "MaChucVu";

            if (cmbTgNhanVien.SelectedIndex == 0)
            {
                if (cmbChucVu.SelectedIndex == 0)
                {
                    dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien order by'{oder}' asc");

                }
                else if (cmbChucVu.SelectedIndex != 0)
                {
                    dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien inner join chucvu on chucvu.machucvu=nhanvien.machucvu where chucvu.tenchucvu=N'{cmbChucVu.SelectedItem.ToString()}' order by'{oder}' asc");

                }

            }
            else
            {
                if (cmbChucVu.SelectedIndex == 0)
                {
                    dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien order by'{oder}' desc");

                }
                else if (cmbChucVu.SelectedIndex != 0)
                {
                    dtgvNhanVien.DataSource = NhanVienDAO.Instance.themTTNhanVien($"select * from nhanvien inner join chucvu on chucvu.machucvu=nhanvien.machucvu where chucvu.tenchucvu=N'{cmbChucVu.SelectedItem.ToString()}' order by'{oder}' desc");

                }
            }
        }
        void listChucVu(string query)
        {
            List<ChucVu> Chuc_vu = ChucVuDAO.Instance.themDsCV(query);
            cmbChucVu.Items.Add("Tất cả");
            foreach (ChucVu cv in Chuc_vu)
            {
                cmbChucVu.Items.Add(cv.TenChucVu);
            }
            cmbChucVu.SelectedIndex = cmbChucVu.Items.IndexOf("Tất cả");
        }
        void tinhTien(TextBox txt, NumericUpDown num, int gia)
        {
            txt.Text = (num.Value * gia).ToString();
        }
        void listBan(string query)
        {
            List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan(query);
            cmbBanNew.Items.Add("Tất cả");
            foreach (BanAn B_a in Ban_an)
            {
                cmbBanNew.Items.Add(B_a.TenBanAn);
            }
            cmbBanNew.SelectedIndex = cmbBanNew.Items.IndexOf("Tất cả");
        }
        void listManv(string query)
        {

            List<NhanVien> MaNv = NhanVienDAO.Instance.themTTNhanVien(query);
            cmbManvNew.Items.Add("Tất cả");
            foreach (NhanVien MNV in MaNv)
            {
                cmbManvNew.Items.Add(MNV.MaNhanVien);
            }
            cmbManvNew.SelectedIndex = cmbManvNew.Items.IndexOf("Tất cả");
        }
        void listTenManv(ComboBox cmb)
        {
            cmb.Items.Clear();
            List<NhanVien> MaNv = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien");
            foreach (NhanVien MNV in MaNv)
            {
                cmb.Items.Add(MNV.TenNhanVien);
                lmanv.Add(MNV.MaNhanVien);
            }
            cmb.SelectedIndex = 0;
        }
        void boloc()
        {
            string nDay = dtpLichSuNew.Value.Day.ToString();
            string nMonth = dtpLichSuNew.Value.Month.ToString();
            string nYear = dtpLichSuNew.Value.Year.ToString();
            string date = nMonth + "/" + nDay + "/" + nYear;
            string name = null;
            string thanhtoan = null;
            if (cmbSxNew.SelectedIndex == 0)
            {
                name = "mahoadon";
            }
            else if (cmbSxNew.SelectedIndex == 1)
            {
                name = "ngaythanhtoan";
            }
            else if (cmbSxNew.SelectedIndex == 2)
            {
                name = "mabanan";
            }
            else if (cmbSxNew.SelectedIndex == 3)
            {
                name = "thanhtien";
            }


            if (cmbLocThanhToanNew.SelectedIndex == 0)
            {
                thanhtoan = "Tất cả";
            }
            else if (cmbLocThanhToanNew.SelectedIndex == 2)
            {
                thanhtoan = "Chưa";
            }
            else if (cmbLocThanhToanNew.SelectedIndex == 1)
            {
                thanhtoan = "Đã thanh toán";
            }


            if (ckbAllNew.CheckState == CheckState.Unchecked)
            {
                if (cmbSxTgNew.SelectedIndex == 0)
                {
                    if (cmbLocThanhToanNew.SelectedIndex == 0)
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' order by '{name}' asc");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                    else
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' order by '{name}' asc");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon  inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                }


                else if (cmbSxTgNew.SelectedIndex == 1)
                {
                    if (cmbLocThanhToanNew.SelectedIndex == 0)
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' order by '{name}' desc");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where inner join banan on banan.mabanan=hoadon.mabanan ngaythanhtoan= '{date}' and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where inner join banan on banan.mabanan=hoadon.mabanan ngaythanhtoan= '{date}' and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' desc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                    else
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' order by '{name}' desc");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where inner join banan on banan.mabanan=hoadon.mabanan ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where inner join banan on banan.mabanan=hoadon.mabanan ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' and banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' desc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                }
            }
            else
            {
                if (cmbSxTgNew.SelectedIndex == 0)
                {
                    if (cmbLocThanhToanNew.SelectedIndex == 0)
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon order by '{name}' asc  ");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                    else
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where thanhtoan=N'{thanhtoan}' order by '{name}' asc  ");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}'  order by '{name}' asc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManvNew.SelectedIndex.ToString()}'and thanhtoan=N'{thanhtoan}'  order by '{name}' asc");
                        }
                    }
                }
                else
                {
                    if (cmbLocThanhToanNew.SelectedIndex == 0)
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon order by '{name}' desc  ");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}'  order by '{name}' desc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                    else
                    {
                        if (cmbBanNew.SelectedIndex == 0 && cmbManvNew.SelectedIndex == 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where thanhtoan=N'{thanhtoan}' order by '{name}' desc  ");
                        }

                        else if (cmbBanNew.SelectedIndex != 0 && cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}'and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManvNew.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBanNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where banan.tenbanan=N'{cmbBanNew.SelectedItem.ToString()}' and thanhtoan=N'{thanhtoan}'  order by '{name}' desc");
                        }
                        else if (cmbManvNew.SelectedIndex != 0)
                        {
                            dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManvNew.SelectedIndex.ToString()}' and thanhtoan=N'{thanhtoan}'  order by '{name}' desc");
                        }
                    }
                }
            }
        }
        void sxLoaiMonAn()
        {
            if (cmbTgLoaiMon.SelectedIndex == 0)
            {
                if (cmbSxLoaiMon.SelectedIndex == 0)
                {
                    dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan order by maloaimonan asc");
                }
                else
                {
                    dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan order by tenloaimonan asc");

                }
            }
            else
            {
                if (cmbSxLoaiMon.SelectedIndex == 0)
                {
                    dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan order by maloaimonan desc");
                }
                else
                {
                    dtgvLoaiMon.DataSource = Database.Instance.KetNoiSql($"Select * from loaimonan order by tenloaimonan desc");

                }

            }
        }
        void loadDsLoaiMon(ComboBox cmb)
        {
            cmb.Items.Clear();
            List<LoaiMonAn> l_mon = LoaiMonAnDAO.Instance.themDsLoaiMon("select * from loaimonan");
            foreach (LoaiMonAn lmon_item in l_mon)
            {
                cmb.Items.Add(lmon_item.TenLoaiMonAn);

            }
            l_mon.Clear();
            cmb.SelectedIndex = 0;
        }
        void bocLocBanAn()
        {
            string oder = "";
            if (cmbSxBanAn.SelectedIndex == 0)
            {
                oder = "MaBanAn";
            }
            else if (cmbSxBanAn.SelectedIndex == 1)
            {
                oder = "TenBanAn";
            }
            else if (cmbSxBanAn.SelectedIndex == 2)
            {
                oder = "SoKhachNgoi";
            }
            int trangThai = 0;
            if (cmbTrangThaiBanAn.SelectedIndex == 1)
            {
                trangThai = 1;
            }
            else if (cmbTrangThaiBanAn.SelectedIndex == 2)
            {
                trangThai = 2;
            }

            if (cmbTgBanAn.SelectedIndex == 0)
            {
                if (trangThai == 0)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan order by'{oder}' asc");
                }
                else if (trangThai == 1)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan where sokhachngoi=4 order by'{oder}' asc");
                }
                else if (trangThai == 2)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan where sokhachngoi<4 order by'{oder}' asc");
                }
            }
            else
            {
                if (trangThai == 0)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan order by'{oder}' desc");
                }
                else if (trangThai == 1)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan where sokhachngoi=4 order by'{oder}' desc");
                }
                else if (trangThai == 2)
                {
                    dtgvBanAn.DataSource = Database.Instance.KetNoiSql($"select * from banan where sokhachngoi<4 order by'{oder}' desc");
                }
            }
        }
        void boLocMonAn()
        {
            int min = 0;
            int max = 0;
            string name = null;
            if (cmbSx.SelectedIndex == 0)
            {
                name = "mamonan";
            }
            else if (cmbSx.SelectedIndex == 1)
            {
                name = "tenmonan";
            }
            else if (cmbSx.SelectedIndex == 2)
            {
                name = "maloaimonan";
            }
            else if (cmbSx.SelectedIndex == 3)
            {
                name = "gia";
            }

            if (cmbLGia.SelectedIndex != 0)
            {
                string[] gia = cmbLGia.SelectedItem.ToString().Split('-');
                min = int.Parse(gia[0]);
                max = int.Parse(gia[1]);

            }


            if (cmbTG.SelectedIndex == 0)
            {
                if (cmbLLoai.SelectedIndex == 0 && cmbLGia.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan order by '{name}' asc");
                }
                else if (cmbLLoai.SelectedIndex != 0 && cmbLGia.SelectedIndex != 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan inner join loaimonan on monan.maloaimonan=loaimonan.maloaimonan where  gia between {min} and {max} and tenloaimonan=N'{cmbLLoai.SelectedItem.ToString()}'  order by '{name}'asc");
                }
                else if (cmbLLoai.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan where  gia between {min} and {max}  order by '{name}'asc");
                }
                else if (cmbLGia.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan inner join loaimonan on monan.maloaimonan=loaimonan.maloaimonan where  tenloaimonan=N'{cmbLLoai.SelectedItem.ToString()}' order by '{name}'asc");
                }
            }
            else
            {
                if (cmbLLoai.SelectedIndex == 0 && cmbLGia.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan order by '{name}' desc");
                }
                else if (cmbLLoai.SelectedIndex != 0 && cmbLGia.SelectedIndex != 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan inner join loaimonan on monan.maloaimonan=loaimonan.maloaimonan where  gia between {min} and {max} and tenloaimonan=N'{cmbLLoai.SelectedItem.ToString()}'  order by '{name}'desc");
                }
                else if (cmbLLoai.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan where  gia between {min} and {max}  order by '{name}'desc");
                }
                else if (cmbLGia.SelectedIndex == 0)
                {
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql($"select * from monan inner join loaimonan on monan.maloaimonan=loaimonan.maloaimonan where  tenloaimonan=N'{cmbLLoai.SelectedItem.ToString()}' order by '{name}'desc");
                }
            }
        }
        void listLoaiMon(string query, ComboBox cmb, int check)
        {
            cmb.Items.Clear();
            List<LoaiMonAn> LMon = LoaiMonAnDAO.Instance.themDsLoaiMon(query);
            if (check > 1)
            {
                cmb.Items.Add("Tất cả");
                cmb.SelectedIndex = cmb.Items.IndexOf("Tất cả");
                foreach (LoaiMonAn LM in LMon)
                {
                    cmb.Items.Add(LM.TenLoaiMonAn);
                }
            }
            else
            {
                foreach (LoaiMonAn LM in LMon)
                {
                    cmb.Items.Add(LM.TenLoaiMonAn);
                }
                cmb.SelectedIndex = 0;
            }

            LMon.Clear();
        }
        void listTenMon(string query, ComboBox cmb)
        {
            cmb.Items.Clear();
            List<MonAn> TMon = MonAnDAO.Instance.themDsMon(query);

            foreach (MonAn TM in TMon)
            {
                cmb.Items.Add(TM.TenMonAn);
            }
            TMon.Clear();
            cmb.SelectedIndex = 0;
        }



        private void btnCNThem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Size = new Size(296, 215);
        }

        private void btnCNSuaMon_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            cmbCapNhatMon.SelectedIndex = 1;
            cmbCapNhatMon.SelectedIndex = 0;
          
        }

        private void btnCNXoaMon_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            List<MonAn> ma_mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbXoaMon.SelectedItem}'");
            if (MonAnDAO.Instance.DeleteMonAn(cmbXoaMon.SelectedItem.ToString()) > 0)
            {
               
                MessageBox.Show("Xoá món thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtgvMonAn.DataSource = Database.Instance.KetNoiSql("select * from monan");
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Delete(Application.StartupPath + "\\Mon\\" + ma_mon[0].Anh);
                listTenMon("select * from monan", cmbXoaMon);
                listTenMon("select * from monan", cmbCapNhatMon);
            }



        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            List<MonAn> ma_mon = MonAnDAO.Instance.themDsMon($"select top 1* from monan where tenmonan=N'{txtCapNhatMon.Text}'");
           
            if (txtCapNhatGia.Text == " " || txtCapNhatMon.Text == " ")
            {
                MessageBox.Show("Vui lòng không để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtCapNhatMon.Text.Equals(cmbCapNhatMon) == false)
            {
                List<MonAn> mon = MonAnDAO.Instance.themDsMon($"select * from monan where tenmonan=N'{cmbCapNhatMon.SelectedItem}'");
                if (Database.Instance.KetNoiSql($"select * from monan where tenmonan=N'{txtCapNhatMon.Text}' and mamonan<>{mon[0].MaMonAn}").Rows.Count > 0)
                {

                    MessageBox.Show("Tên món đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCapNhatMon.Clear();
                    txtCapNhatMon.Focus();
                }
                else
                {
                    if (MonAnDAO.Instance.UpdateMonAn(Save_maMonAn, txtCapNhatMon.Text, cmbCapNhatLoaiMon.SelectedIndex + 1, int.Parse(txtCapNhatGia.Text)) > 0) {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(Application.StartupPath + "\\Mon\\" + ma_mon[0].Anh);
                        picCnAnh.Image.Save(Application.StartupPath + "\\Mon\\" + ma_mon[0].Anh);
                        MessageBox.Show("Cập nhật món thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtgvMonAn.DataSource = Database.Instance.KetNoiSql("select * from monan");
                        listTenMon("select * from monan", cmbXoaMon);
                        listTenMon("select * from monan", cmbCapNhatMon);
                 
                    }
                    
                }
                mon.Clear();
            }

            else if (txtCapNhatMon.Text.Equals(cmbCapNhatMon) == true)
            {
                if (MonAnDAO.Instance.UpdateMonAn(Save_maMonAn, txtCapNhatMon.Text, cmbCapNhatLoaiMon.SelectedIndex + 1, int.Parse(txtCapNhatGia.Text)) > 0)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(Application.StartupPath + "\\Mon\\" + ma_mon[0].Anh);
                    picCnAnh.Image.Save(Application.StartupPath + "\\Mon\\" + ma_mon[0].Anh);
                    MessageBox.Show("Cập nhật món thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgvMonAn.DataSource = Database.Instance.KetNoiSql("select * from monan");
                    listTenMon("select * from monan", cmbXoaMon);
                    listTenMon("select * from monan", cmbCapNhatMon);
                   
                }
            }
            ma_mon.Clear();


        }
        void listCv(ComboBox cmb, string query)
        {
            cmb.Items.Clear();

            List<ChucVu> Chuc_vu = ChucVuDAO.Instance.themDsCV(query);
            foreach (ChucVu cv in Chuc_vu)
            {
                cmb.Items.Add(cv.TenChucVu);

            }
            Chuc_vu.Clear();
            cmb.SelectedIndex = 0;
        }
        void themBan(ComboBox cmb, string query)
        {
            cmb.Items.Clear();

            List<BanAn> banAn = BanAnDAO.Instance.themDsBan(query);
            foreach (BanAn b in banAn)
            {
                cmb.Items.Add(b.TenBanAn);
                lmaban.Add(b.MaBanAn);
            }
            banAn.Clear();
            cmb.SelectedIndex = 0;
        }
        void themMaHD(ComboBox cmb, string query)
        {
            if (Database.Instance.KetNoiSql("select * from hoadon").Rows.Count > 0){ 

            cmb.Items.Clear();
            List<HoaDon> hd = HoaDonDAO.Instance.themHoaDon(query);
           
            foreach (HoaDon HD in hd)
            {
                cmb.Items.Add(HD.MaHoaDon);

            }
            hd.Clear();
            cmb.SelectedIndex = 0;
            }
        }
        void laySoGhe(string tenban)
        {


            List<BanAn> banAn = BanAnDAO.Instance.themDsBan($"select * from banan where tenbanan=N'{tenban}'");
            foreach (BanAn b in banAn)
            {
                txtCnSL.Text = b.SoKhachNgoi.ToString();
                maban = b.MaBanAn;
            }

        }
        private void btnThemBan_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox5.Visible = false;
            groupBox6.Visible = false;

        }

        private void btnCapNhatBan_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox5.Visible = true;
            groupBox6.Visible = false;
            themBan(cmbChonBan, "select * from banan");
            txtCnTenBan.Text = cmbChonBan.SelectedItem.ToString();
            laySoGhe(txtCnTenBan.Text);
            listTenManv(cmbNhanVienHd);

        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = true;
            themBan(cmbXoaBan, "select * from banan");
        }



        private void btnLThemLMon_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = true;
            groupBox8.Visible = false;
            groupBox9.Visible = false;
        }

        private void btnXoaLoaiMon_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            groupBox9.Visible = true;
            loadDsLoaiMon(cmbXoaLoaiMon);
        }

        private void btnCapNhatLMon_Click(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            groupBox8.Visible = true;
            groupBox9.Visible = false;
            loadDsLoaiMon(cmbCnLoaiMon);
            txtCnTenLMon.Text = cmbCnLoaiMon.Items[cmbCnLoaiMon.SelectedIndex].ToString();
        }
        void loaddsMon(ComboBox cmb)
        {
            cmb.Items.Clear();

            DataTable ttable = Database.Instance.KetNoiSql("select distinct MonAn.TenMonAn from chitiethoadon  as ct inner join  MonAn on MonAn.MaMonAn=ct.MaMonAn");
            cmb.Items.Add("Tất cả");
            foreach (DataRow tab in ttable.Rows)
            {

                cmb.Items.Add(tab.ItemArray.GetValue(0));

            }
            ttable.Clear();
            cmb.SelectedIndex = 0;
        }
        void loaddsMon2(ComboBox cmb, int mahoadon)
        {
            cmb.Items.Clear();
            List<MonAn> ma = MonAnDAO.Instance.themDsMon($"select monan.mamonan,MonAn.Anh,MonAn.TenMonAn,MonAn.MaLoaiMonAn,MonAn.Gia from chitiethoadon  as ct inner join  MonAn on MonAn.MaMonAn=ct.MaMonAn where ct.mahoadon={mahoadon}");

            //  DataTable ttable = Database.Instance.KetNoiSql($"select distinct MonAn.TenMonAn from chitiethoadon  as ct inner join  MonAn on MonAn.MaMonAn=ct.MaMonAn where ct.mahoadon={mahoadon}");
            foreach (MonAn m in ma)
            {

                cmb.Items.Add(m.TenMonAn);

            }
            //     cmb.SelectedIndex = 0;
            ma.Clear();

        }
        void listMaHD(ComboBox cmb, string query)
        {
            cmb.Items.Clear();
            cmb.Items.Add("Tất cả");
            List<HoaDon> hd = HoaDonDAO.Instance.themHoaDon(query);
            foreach (HoaDon HD in hd)
            {
                cmb.Items.Add(HD.MaHoaDon);

            }
            cmb.SelectedIndex = 0;
        }
        void boLocCTHD()
        {

            string order = null;
            if (cmbSxCthd.SelectedIndex == 0)
                order = " ct.MaHoaDon";
            else if (cmbSxCthd.SelectedIndex == 1)
                order = " MonAn.TenMonAn";
            else if (cmbSxCthd.SelectedIndex == 2)
                order = " SoLuongMon";
            else if (cmbSxCthd.SelectedIndex == 3)
                order = " ThanhTien";

            if (cmbTgCthd.SelectedIndex == 0)
            {
                if (cmbChonMonAn.SelectedIndex == 0 && cmbChonHd.SelectedIndex == 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + " order by " + order + " asc");

                }
                else if (cmbChonMonAn.SelectedIndex != 0 && cmbChonHd.SelectedIndex == 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $" where monan.tenmonan=N'{cmbChonMonAn.SelectedItem.ToString()}' " + "order by " + order + " asc");

                }
                else if (cmbChonMonAn.SelectedIndex == 0 && cmbChonHd.SelectedIndex != 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $" where ct.mahoadon={int.Parse(cmbChonHd.SelectedItem.ToString())}" + "order by " + order + " asc");
                }
                else if (cmbChonMonAn.SelectedIndex != 0 && cmbChonHd.SelectedIndex != 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $"where ct.mahoadon={int.Parse(cmbChonHd.SelectedItem.ToString())} and monan.tenmonan=N'{cmbChonMonAn.SelectedItem.ToString()}'" + "order by " + order + " asc");

                }
            }
            else if (cmbTgCthd.SelectedIndex == 1)
            {
                if (cmbChonMonAn.SelectedIndex == 0 && cmbChonHd.SelectedIndex == 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + " order by " + order + " desc");

                }
                else if (cmbChonMonAn.SelectedIndex != 0 && cmbChonHd.SelectedIndex == 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $" where monan.tenmonan=N'{cmbChonMonAn.SelectedItem.ToString()}' " + "order by " + order + " desc");

                }
                else if (cmbChonMonAn.SelectedIndex == 0 && cmbChonHd.SelectedIndex != 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $" where ct.mahoadon={int.Parse(cmbChonHd.SelectedItem.ToString())}" + "order by " + order + " desc");
                }
                else if (cmbChonMonAn.SelectedIndex != 0 && cmbChonHd.SelectedIndex != 0)
                {
                    dtgvCthoaDon.DataSource = Database.Instance.KetNoiSql(qry + $"where ct.mahoadon={int.Parse(cmbChonHd.SelectedItem.ToString())} and monan.tenmonan=N'{cmbChonMonAn.SelectedItem.ToString()}'" + "order by " + order + " desc");

                }
            }

        }
        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLQADataSet.HoaDon' table. You can move, or remove it, as needed.
            //  this.hoaDonTableAdapter.Fill(this.qLQADataSet.HoaDon);
            // TODO: This line of code loads data into the 'qLQADataSet.ChucVu' table. You can move, or remove it, as needed.
            //  this.chucVuTableAdapter.Fill(this.qLQADataSet.ChucVu);
            if (Database.Instance.KetNoiSql("select * from hoadon").Rows.Count <= 0)
            {
                // MessageBox.Show("Không tìm thấy dữ liệu ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (Control crt in tabPage1.Controls)
                {
                    crt.Enabled = false;
                }
               
            }

            if (Database.Instance.KetNoiSql("select * from chitiethoadon").Rows.Count <= 0)
            {

                foreach (Control crt in tabpage2.Controls)
                {
                    crt.Enabled = false;
                }


            }
            //  dtgvLichSuNew.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
            this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");

        }

        private void btnThemHd_Click(object sender, EventArgs e)
        {
            groupBox10.Visible = true;
            groupBox10.Size = new Size(299, 209);
            groupBox11.Visible = false;
            groupBox12.Visible = false;
            themBan(cmbBanAnHd, "select * from banan where sokhachngoi<4");
            listTenManv(cmbNhanVienHd);
            cmbTTHoaDon.SelectedIndex = 1;
        }

        private void btnCapNhatHd_Click(object sender, EventArgs e)
        {
            groupBox10.Visible = false;
            groupBox11.Visible = true;
            groupBox12.Visible = false;

            listTenManv(cmbCapNhatNV);
            themMaHD(cmbCapNhatHd, "select * from hoadon");
            themBan(cmbCapNhatBanan, "select * from banan where sokhachngoi<4");
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            groupBox10.Visible = false;
            groupBox11.Visible = false;
            groupBox12.Visible = true;
            cmbXoaTheo.SelectedIndex = 0;

        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            cmbChonMonAn.SelectedIndex = 0;
            cmbChonHd.SelectedIndex = 0;
            cmbSxCthd.SelectedIndex = 0;
            cmbTgCthd.SelectedIndex = 0;
        }

        private void btnThemCthd_Click(object sender, EventArgs e)
        {
            groupBox13.Visible = true;
            groupBox14.Visible = false;
            groupBox15.Visible = false;
            listTenMon("select * from monan", cmbThemMonCTHD);
            themMaHD(cmbChonHDThem, "select * from hoadon");
            groupBox13.Size = new Size(275, 200);
        }

        private void btnCapNhatCthd_Click(object sender, EventArgs e)
        {
            groupBox13.Visible = false;
            groupBox14.Visible = true;
            groupBox15.Visible = false;
            themMaHD(cmbCapNhatCTHD, "select * from hoadon");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox13.Visible = false;
            groupBox14.Visible = false;
            groupBox15.Visible = true;
            themMaHD(cmbXacNhanXoaMaHD, "select * from hoadon");
            cmbXacNhanXoaMon.SelectedIndex = 0;
        }

        private void btnXoaChucVu_Click(object sender, EventArgs e)
        {
            listCv(cmbXoaTenCv, "select * from chucvu");
            groupBox16.Visible = false;
            groupBox17.Visible = false;
            groupBox18.Visible = true;
        }

        private void btnCapNhatCv_Click(object sender, EventArgs e)
        {
            listCv(cmbChonCv, "select * from chucvu");
            groupBox16.Visible = false;
            groupBox17.Visible = true;
            groupBox18.Visible = false;
        }

        private void btnThemChucVu_Click(object sender, EventArgs e)
        {

            groupBox16.Visible = true;
            groupBox17.Visible = false;
            groupBox18.Visible = false;
        }

        private void btnThemNv_Click(object sender, EventArgs e)
        {
            groupBox19.Visible = true;
            groupBox20.Visible = false;
            groupBox21.Visible = false;
            listCv(cmbChucVuNv, "select * from chucvu");
        }

        private void btnCapNhatNv_Click(object sender, EventArgs e)
        {
            groupBox19.Visible = false;
            groupBox20.Visible = true;
            groupBox21.Visible = false;
            listCv(cmbCNCVNV, "select * from chucvu");
            listTenManv(cmbChonNhanVien);
            
        }

        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            groupBox19.Visible = false;
            groupBox20.Visible = false;
            groupBox21.Visible = true;
            listTenManv(cmbXoaNhanVien);
        }
    }
}
