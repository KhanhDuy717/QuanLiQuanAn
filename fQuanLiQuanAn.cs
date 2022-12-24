using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using CheckBox = System.Windows.Forms.CheckBox;
using Point = System.Drawing.Point;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    public partial class fQuanLiQuanAn : Form
    {
        public int gia_mon = 0;
        public int thanhtien = 0;
        public fQuanLiQuanAn()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            btnBan.Click += (o, e) =>
            {
                fBanAn B_a = new fBanAn(this);
                B_a.StartPosition = FormStartPosition.CenterScreen;
                B_a.ShowDialog();

            };

            btnTimKiem.Click += (o, e) =>
            {

                flpMonAn.Controls.Clear();
                themMonAn($"select * from monan where tenmonan like N'%{txtTimKiem.Text}%'");
            };

            txtTtien.Text = "0";
            btnDat.Click += (o, e) =>
            {
                if (txtBanAn.Text.Equals("")&& lvmenu.Items.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn bàn và món", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                } else if (lvmenu.Items.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn món", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else if (txtBanAn.Text.Equals(""))
                {
                    MessageBox.Show("Vui lòng chọn bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn đặt món", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        BanAnDAO.Instance.updateSoKhach(int.Parse(nupSLNguoi.Value.ToString()), int.Parse(txtBanAn.Text));
                        if (HoaDonDAO.Instance.InsertHoaDon(DateTime.UtcNow.Date, int.Parse(txtBanAn.Text), int.Parse(txtTtien.Text), int.Parse(txtMaNv.Text), "Chưa", ((int)nupSLNguoi.Value)) > 0)
                        {

                            MessageBox.Show("Lưu hoá đơn thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            boloc();
                            boLocHd();
                            txtTtien.Text = "0";
                            ThenBanAn("select * from banan", flpBanAn);
                        }

                        foreach (ListViewItem lvi in lvmenu.Items)
                        {
                            Database.Instance.InsertCTHD(int.Parse(lvi.SubItems[0].Text), int.Parse(lvi.SubItems[2].Text));
                        }
                        foreach (CheckBox ckb in flpMonAn.Controls)
                        {
                            ckb.Checked = false;
                        }
                        txtBanAn.Text = " ";
                        nupSLNguoi.Value = 1;
                        lvmenu.Items.Clear();
                    }

                }
               

            };

            dtgvLichSu.DataSource = Database.Instance.KetNoiSql("select * from hoadon");
            cmbSx.SelectedIndex = cmbSx.Items.IndexOf("Mã hoá đơn");
            cmbLocThanhToan.SelectedIndex = 0;
            cmbSxTg.SelectedIndex = 0;
            listBan("select * from banan");
            listManv("select * from nhanvien");
            dtpLichSu.ValueChanged += (o, e) =>
            {
                boloc();
            };

            cmbBan.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbManv.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            ckbAll.CheckedChanged += (o, e) =>
            {
                boloc();
            };
            cmbSx.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbSxTg.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };
            cmbLocThanhToan.SelectedIndexChanged += (o, e) =>
            {
                boloc();
            };


            dtpHoaDon.ValueChanged += (o, e) =>
            {
                boLocHd();
            };
            lvhd.SelectedIndexChanged += (o, e) =>
            {
               
                    menu(int.Parse(lvhd.Items[lvhd.FocusedItem.Index].SubItems[0].Text));
                    txtTongTien.Text = lvhd.Items[lvhd.FocusedItem.Index].SubItems[4].Text;
           
                
            };

            txtBanAn.TextChanged += (o, e) =>
            {
                nupSLNguoi.Maximum = 4;
            };
            nupSLNguoi.Click += (o, e) =>
             {

                 if (txtBanAn.Text == " ")
                 {
                     nupSLNguoi.Maximum = 1;
                     nupSLNguoi.Value = 1;
                     MessageBox.Show("Vui lòng chọn bàn trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }



             };
            nupSLNguoi.ValueChanged += (o, e) =>
             {
                 if (txtBanAn.Text != " ")
                 {
                     List<BanAn> banAn = BanAnDAO.Instance.themDsBan($"select * from banan where mabanan={int.Parse(txtBanAn.Text)}");
                     foreach (BanAn B_a in banAn)
                     {
                         if (nupSLNguoi.Value > (4 - B_a.SoKhachNgoi))
                         {
                             MessageBox.Show("Đã vuọt quá số ghế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             if (nupSLNguoi.Value > 1)
                             {
                                 nupSLNguoi.Value = nupSLNguoi.Value - 1;
                             }
                         }
                     }
                 }
                 else nupSLNguoi.Value = 1;

             };
            btnThanhToan.Click += (o, e) =>
             {
                 if (lvcthd.Items.Count <= 0)
                 {
                     MessageBox.Show("Vui lòng chọn hoá đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 else
                 {
                     if (MessageBox.Show("Xác nhận thanh toán số tiền " + txtTongTien.Text + "đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                     {
                         int maban = int.Parse(lvhd.Items[lvhd.FocusedItem.Index].SubItems[2].Text);
                         int soKhach = int.Parse(lvhd.Items[lvhd.FocusedItem.Index].SubItems[6].Text);
                         if (HoaDonDAO.Instance.updateHoaDon(int.Parse(lvhd.Items[lvhd.FocusedItem.Index].SubItems[0].Text)) > 0 && BanAnDAO.Instance.updateSoKhachRaVe(soKhach, maban) > 0)
                         {
                             MessageBox.Show("Hoá đơn đã được thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         }
                         report r = new report();
                         r.Show();
                     }
                     ThenBanAn("select * from banan", flpBanAn);
                 }
              
             };

            for (int i = 2020; i <= DateTime.Today.Year; i++)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedIndex = (cmbYear.Items.Count) - 1;
            cmbMonth.SelectedIndex = int.Parse(DateTime.Today.Month.ToString()) - 1;
            loadData();
            loadChart();
            cmbMonth.SelectedIndexChanged += (o, e) =>
            {
                List<MonThinhHanh> mon = MonThinhHanhDAO.Instance.DsMonThinhHanh($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");

                if (mon.Count <= 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbYear.SelectedIndex = (cmbYear.Items.Count) - 1;
                    cmbMonth.SelectedIndex = int.Parse(DateTime.Today.Month.ToString()) - 1;
                }
                mon.Clear();
                loadData();
                loadChart();
            };
            cmbYear.SelectedIndexChanged += (o, e) =>
            {
                List<MonThinhHanh> mon = MonThinhHanhDAO.Instance.DsMonThinhHanh($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");

                if (mon.Count <= 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbYear.SelectedIndex = (cmbYear.Items.Count) - 1;
                    cmbMonth.SelectedIndex = int.Parse(DateTime.Today.Month.ToString()) - 1;
                }
                mon.Clear();
                loadData();
                loadChart();
            };
            ckbTatCa.CheckedChanged += (o, e) =>
            {
                loadData();
                if (ckbTatCa.CheckState == CheckState.Checked)
                {
                    chtMonAn.Series["Tiền"].Points.Clear();
                    List<MonThinhHanh> mon = MonThinhHanhDAO.Instance.DsMonThinhHanh("exec dbo.monThinhHanh2");
                    foreach (MonThinhHanh MonT_h in mon)
                    {
                        chtMonAn.Series["Tiền"].Points.AddXY(MonT_h.TenMonAn, MonT_h.PhanTram);
                    }
                }
                else
                {
                    cmbYear.SelectedIndex = (cmbYear.Items.Count) - 1;
                    cmbMonth.SelectedIndex = int.Parse(DateTime.Today.Month.ToString()) - 1;
                    loadChart();
                }

            };
            btnXuat.Click += (o, e) =>
            {
                fexport exp = new fexport();
                List<MonThinhHanh> mon = MonThinhHanhDAO.Instance.DsMonThinhHanh($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");
                save.Filter = "Excel Workbook|*.xlsx";
                save.FileName = "MonThinhHanh" + cmbMonth.SelectedItem.ToString() + "-" + cmbYear.SelectedItem.ToString();
              
                if (save.ShowDialog() == DialogResult.OK)
                {   
                    Microsoft.Office.Interop.Excel.Application exc = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wB = exc.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet wS = (Worksheet)exc.ActiveSheet;
                    wS.Range[wS.Cells[1, 1], wS.Cells[1, 4]].Merge();
                    exc.Cells[1, 1] = "Thống kê tháng " + cmbMonth.SelectedItem.ToString() + "-" + cmbYear.SelectedItem.ToString(); ;
                    exc.Cells[3, 1] = "Tên món";
                    exc.Cells[3, 2] = "Số lượng";
                    exc.Cells[3, 3] = "Tiền";
                    exc.Cells[3, 4] = "Phần trăm";
                    int cell = 4;
                   
                    foreach (MonThinhHanh m in mon)
                    {
                        exc.Cells[cell, 1] = m.TenMonAn.ToString();
                        exc.Cells[cell, 2] = m.SoLuong.ToString();
                        exc.Cells[cell, 3] = m.Tien.ToString();
                        exc.Cells[cell, 4] = m.PhanTram.ToString();

                        wS.get_Range("A" + (cell - 1).ToString(), "D" + (cell - 1).ToString()).Cells.BorderAround(Type.Missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));
                        cell++;
                    }


                    exc.Cells[3, 1].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGrey;
                    exc.Cells[3, 2].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGrey;
                    exc.Cells[3, 3].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGrey;
                    exc.Cells[3, 4].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGrey;
                    wS.Range["A1"].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    wS.get_Range("C3", "C" + (cell - 1).ToString()).Cells.BorderAround(Type.Missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

                    wS.get_Range("B3", "B" + (cell - 1).ToString()).Cells.BorderAround(Type.Missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

                    wS.get_Range("A3", "A" + (cell - 1).ToString()).Cells.BorderAround(Type.Missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

                    wS.get_Range("A" + (cell - 1).ToString(), "D" + (cell - 1).ToString()).Cells.BorderAround(Type.Missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));
                    int check = 0;
                    
                    try
                    {
                         wB.SaveAs(save.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                      
                    }
                    catch (Exception)
                    {
                       
                       
                        MessageBox.Show("Vui lòng đóng file hiện hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        check = 1;
                       
                    }

                    exp.Show();
               
                    if (check != 1)
                    {
                        Thread.Sleep(2000);
                        MessageBox.Show("Đã xuât file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        exp.Hide();
                
                    }
                    wB.Close(null,null,null);
                    exc.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exc);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wB);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wS);
                    wB = null;
                    exc = null;
                }
            
                
            };
        }
        void boLocHd()
        {
            string nDay = dtpHoaDon.Value.Day.ToString();
            string nMonth = dtpHoaDon.Value.Month.ToString();
            string nYear = dtpHoaDon.Value.Year.ToString();
            string date = nMonth + "/" + nDay + "/" + nYear;
            dtgvhd.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien={txtMaNv.Text} and ngaythanhtoan='{date}'");
            txtSLhd.Text = (dtgvhd.Rows.Count - 1).ToString();
            ckbTatCaHd.Checked = false;
        }
        public string Txt
        {
            get { return txtBanAn.Text; }
            set { txtBanAn.Text = value; }
        }
        public string sluong;
        public string soLuong
        {
            get { return sluong; }
            set { sluong = value; }

        }
        void menu(int maHd)
        {
            List<Menu> Menu = MenuDAO.Instance.themMenu($"exec dbo.Menu {maHd}");
            lvcthd.Items.Clear();
            foreach (Menu M_n in Menu)
            {
                ListViewItem lvi = new ListViewItem(M_n.MaHoaDon.ToString());
                lvi.SubItems.Add(M_n.Tien.ToString());
                lvi.SubItems.Add(M_n.TeMonAn);
                lvcthd.Items.Add(lvi);
            }
        }
        void boloc()
        {
            string nDay = dtpLichSu.Value.Day.ToString();
            string nMonth = dtpLichSu.Value.Month.ToString();
            string nYear = dtpLichSu.Value.Year.ToString();
            string date = nMonth + "/" + nDay + "/" + nYear;
            string name = null;
            string thanhtoan = null;
            if (cmbSx.SelectedIndex == 0)
            {
                name = "mahoadon";
            }
            else if (cmbSx.SelectedIndex == 1)
            {
                name = "ngaythanhtoan";
            }
            else if (cmbSx.SelectedIndex == 2)
            {
                name = "mabanan";
            }
            else if (cmbSx.SelectedIndex == 3)
            {
                name = "thanhtien";
            }


            if (cmbLocThanhToan.SelectedIndex == 0)
            {
                thanhtoan = "Tất cả";
            }
            else if (cmbLocThanhToan.SelectedIndex == 2)
            {
                thanhtoan = "Chưa";
            }
            else if (cmbLocThanhToan.SelectedIndex == 1)
            {
                thanhtoan = "Đã thanh toán";
            }


            if (ckbAll.CheckState == CheckState.Unchecked)
            {
                if (cmbSxTg.SelectedIndex == 0)
                {
                    if (cmbLocThanhToan.SelectedIndex == 0)
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' order by '{name}' asc");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where ngaythanhtoan= '{date}' and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan  where ngaythanhtoan= '{date}' and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                    else
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' order by '{name}' asc");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                }


                else if (cmbSxTg.SelectedIndex == 1)
                {
                    if (cmbLocThanhToan.SelectedIndex == 0)
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' order by '{name}' desc");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' order by '{name}' desc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                    else
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' order by '{name}' desc");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}' and banan.tenbanan='{cmbBan.SelectedItem.ToString()}' order by '{name}' desc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where ngaythanhtoan= '{date}' and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                }
            }
            else
            {
                if (cmbSxTg.SelectedIndex == 0)
                {
                    if (cmbLocThanhToan.SelectedIndex == 0)
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon order by '{name}' asc  ");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}' order by '{name}' asc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' asc");
                        }
                    }
                    else
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where thanhtoan=N'{thanhtoan}' order by '{name}' asc  ");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}'and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManv.SelectedIndex.ToString()}'  order by '{name}' asc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}'and thanhtoan=N'{thanhtoan}' order by '{name}' asc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManv.SelectedIndex.ToString()}'and thanhtoan=N'{thanhtoan}'  order by '{name}' asc");
                        }
                    }
                }
                else
                {
                    if (cmbLocThanhToan.SelectedIndex == 0)
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon order by '{name}' desc  ");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}'and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}'  order by '{name}' desc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");
                        }
                    }
                    else
                    {
                        if (cmbBan.SelectedIndex == 0 && cmbManv.SelectedIndex == 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where thanhtoan=N'{thanhtoan}' order by '{name}' desc  ");
                        }

                        else if (cmbBan.SelectedIndex != 0 && cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}'and thanhtoan=N'{thanhtoan}'  and manhanvien='{cmbManv.SelectedIndex.ToString()}' order by '{name}' desc");

                        }
                        else if (cmbBan.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon inner join banan on banan.mabanan=hoadon.mabanan where banan.tenbanan='{cmbBan.SelectedItem.ToString()}' and thanhtoan=N'{thanhtoan}'  order by '{name}' desc");
                        }
                        else if (cmbManv.SelectedIndex != 0)
                        {
                            dtgvLichSu.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{cmbManv.SelectedIndex.ToString()}' and thanhtoan=N'{thanhtoan}'  order by '{name}' desc");
                        }
                    }
                }
            }
        }
        void boLocMonAn()
        {

            List<LoaiMonAn> LMon = LoaiMonAnDAO.Instance.themDsLoaiMon("Select * from loaimonan");
            foreach (LoaiMonAn lm in LMon)
            {
                System.Windows.Forms.Button btn = new System.Windows.Forms.Button() { Height = 22, Width = 72 };
                btn.Text = lm.TenLoaiMonAn;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.White;
                flpBoLocMon.Controls.Add(btn);

            }
            foreach (System.Windows.Forms.Button btnn in flpBoLocMon.Controls)
            {


                btnn.Click += (o, e) =>
                {
                    flpMonAn.Visible = false;
                    Thread.Sleep(200);
                    btnAd.Cursor = Cursors.WaitCursor;
                    if (btnn.Text.Equals("Tất cả"))
                    {
                        flpMonAn.Controls.Clear();
                        themMonAn($"select * from monan");
                    }
                    else
                    {
                        flpMonAn.Controls.Clear();

                        themMonAn($"select monan.* from monan inner join loaimonan on loaimonan.maloaimonan=monan.maloaimonan where loaimonan.tenloaimonan=N'{btnn.Text}'");

                    }
                    flpMonAn.Visible = true;
                };

            }
        }
        void listBan(string query)
        {
            List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan(query);
            cmbBan.Items.Add("Tất cả");
            foreach (BanAn B_a in Ban_an)
            {
                cmbBan.Items.Add(B_a.TenBanAn);
            }
            cmbBan.SelectedIndex = cmbBan.Items.IndexOf("Tất cả");
        }
        void listManv(string query)
        {
            List<NhanVien> MaNv = NhanVienDAO.Instance.themTTNhanVien(query);
            cmbManv.Items.Add("Tất cả");
            foreach (NhanVien MNV in MaNv)
            {
                cmbManv.Items.Add(MNV.MaNhanVien);
            }
            cmbManv.SelectedIndex = cmbManv.Items.IndexOf("Tất cả");
        }
        void themMonAn(string query)
        {
            List<MonAn> mon_an = new List<MonAn>();
            mon_an = MonAnDAO.Instance.themDsMon(query);
            flpMonAn.Visible = false;
            foreach (MonAn M_a in mon_an)
            {
                System.Windows.Forms.CheckBox ckb = new System.Windows.Forms.CheckBox() { Width = 175, Height = 145 };
                Bitmap image=new Bitmap(Application.StartupPath + "\\Mon\\"+M_a.Anh);
                ckb.BackgroundImage= new Bitmap(image,new Size(175,115));
                ckb.BackgroundImageLayout = ImageLayout.None;
                ckb.Margin = new Padding(4, 10, 4, 10);
                ckb.BackColor = Color.White;
                ckb.Text = M_a.TenMonAn + "-" + M_a.Gia.ToString() + "đ";
                ckb.TextAlign = ContentAlignment.BottomLeft;
                ckb.CheckAlign = ContentAlignment.BottomLeft;
                ckb.BackColor = Color.Transparent;
                ckb.ForeColor = Color.White;
                ckb.Font = new System.Drawing.Font("TimeNewRoman", 9);
                
                flpMonAn.Controls.Add(ckb);

                ckb.CheckedChanged += (o, e) =>
                {
                    int index = 0;

                    if (ckb.CheckState == CheckState.Checked)
                    {
                        SoLuong S_l = new SoLuong(this);
                        S_l.ShowDialog();
                        ListViewItem lvi = new ListViewItem(M_a.MaMonAn.ToString());
                        lvi.SubItems.Add(M_a.TenMonAn.ToString());
                        lvi.SubItems.Add(soLuong);
                        lvi.SubItems.Add((M_a.Gia * int.Parse(soLuong)).ToString());
                        lvmenu.Items.Add(lvi);
                        thanhtien = 0;
                        foreach (ListViewItem lv_item in lvmenu.Items)
                        {
                            gia_mon = int.Parse(lv_item.SubItems[3].Text);
                            thanhtien += gia_mon;
                            txtTtien.Text = thanhtien.ToString();
                        }
                        thanhtien = 0;
                    }
                    else if (ckb.CheckState == CheckState.Unchecked)
                    {
                        string[] name = ckb.Text.Split('-');
                        foreach (ListViewItem lv_item in lvmenu.Items)
                        {
                            if (String.Equals(lv_item.SubItems[1].Text, name[0]) == true)
                            {
                                index = lv_item.Index;
                                if (txtTtien.Text.Equals("0")==false)
                                {
                                    txtTtien.Text = (int.Parse(txtTtien.Text.Remove(txtTtien.Text.Length - 1)) - int.Parse(lvmenu.Items[index].SubItems[3].Text)).ToString();
                                    lvmenu.Items[index].Remove();
                                }
                              

                            }
                        }

                    }
                };
            }

            mon_an.Clear();
            Thread.Sleep(200);
            flpMonAn.Visible = true;
        }
        public void ThenBanAn(string query, FlowLayoutPanel flp)
        {
            flp.Controls.Clear();
            List<BanAn> Ban_an = BanAnDAO.Instance.themDsBan(query);
            foreach (BanAn B_a in Ban_an)
            {
                RadioButton rdb = new RadioButton() { Width = 160, Height = 165 };
                rdb.BackgroundImage = new Bitmap(Application.StartupPath + "\\BanAn\\BanAn.jpg");
                rdb.Text = B_a.TenBanAn + " - " + B_a.SoKhachNgoi + "/4 người";
                rdb.BackgroundImageLayout = ImageLayout.Zoom;
                rdb.ForeColor = Color.White;
                rdb.TextAlign = ContentAlignment.BottomLeft;
                rdb.CheckAlign = ContentAlignment.BottomLeft;
                flp.Controls.Add(rdb);
                rdb.CheckedChanged += (o,e) =>
                {
                    txtTongTien.Clear();
                    lvcthd.Items.Clear();

                    lvhd.Items.Clear();
                    if (rdb.Checked == true)
                    {
                        ListViewItem lvi = new ListViewItem();
                        List<HoaDon> hoaDon = HoaDonDAO.Instance.themHoaDon($"select * from hoadon where mabanan={B_a.MaBanAn.ToString()} and thanhtoan=N'Chưa'");
                        foreach (HoaDon hd in hoaDon)
                        {
                            string[] date = hd.NgayThanhToan.ToString().Split(' ');
                            lvi = new ListViewItem(hd.MaHoaDon.ToString());
                            lvi.SubItems.Add(date[0]);
                            lvi.SubItems.Add(hd.MaBanAn.ToString());
                            lvi.SubItems.Add(hd.ThanhToan);
                            lvi.SubItems.Add(hd.ThanhTien.ToString());
                            lvi.SubItems.Add(hd.MaNhanVien.ToString());
                            lvi.SubItems.Add(hd.SlNguoi.ToString());
                            lvhd.Items.Add(lvi);
                          
                        }
                         
                          //menu(int.Parse(lvhd.Items[lvhd.FocusedItem.Index].SubItems[0].Text));
                         // txtTongTien.Text = lvhd.Items[lvhd.FocusedItem.Index].SubItems[4].Text;
                    }

                };              
            }
            Ban_an.Clear();
        }

        private void fQuanLiQuanAn_Load(object sender, EventArgs e)
        {
            boLocMonAn();
            this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["DangNhap"];
            txtTenNv.Text = ((DangNhap)f).txtTenDN.Text;
            txtMk.Text = ((DangNhap)f).txtMK.Text;
            if (NhanVienDAO.Instance.KtAdmin(txtTenNv.Text, txtMk.Text) >= 1)
            {
                txtCv.Text = "Admin";
            }
            else
            {
                btnAd.Visible = false;
                txtCv.Text = "Nhân viên";
            }

            flpMonAn.Controls.Clear();
            themMonAn("select * from monan");
            themTTNv("select * from nhanvien where tennhanvien='" + txtTenNv.Text + "' and matkhau='" + txtMk.Text + "'");
            dtgvhd.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{txtMaNv.Text}' ");
            txtSLhd.Text = (dtgvhd.Rows.Count - 1).ToString();
            ThenBanAn("select * from banan", flpBanAn);
            List<NhanVien> Nv = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien where tennhanvien='" + txtTenNv.Text + "' and matkhau='" + txtMk.Text + "'");
           // if (File.Exists(Application.StartupPath + "\\user\\user" + txtMaNv.Text + ".jpg") == true)
             //   ptbAnh.Image = new Bitmap(Application.StartupPath + "\\user\\user" + txtMaNv.Text + ".jpg");
           // else
                ptbAnh.Image = new Bitmap(Application.StartupPath + "\\user\\"+Nv[0].AnhNhanVien);
            //List<MonThinhHanh> mon1 = MonThinhHanhDAO.Instance.DsMonThinhHanh($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");
            //if (mon1.Count <= 0)
            //{
            //    System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox() { Width = 200, Height = 50 };
            //    txt.Text = "Hiện không có dữ liệu";
            //    txt.Location = new Point(280, 275);
            //    tabPage5.Controls.Add(txt);
            //    chtMonAn.Visible = false;
            //}
            Nv.Clear();
        }
        public void themTTNv(string query)
        {
            List<NhanVien> Nv = NhanVienDAO.Instance.themTTNhanVien(query);

            foreach (NhanVien N_v in Nv)
            {
                txtMaNv.Text = N_v.MaNhanVien.ToString();
            }


        }

        private void fQuanLiQuanAn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng ứng dụng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<NhanVien> Nv = NhanVienDAO.Instance.themTTNhanVien("select * from nhanvien where tennhanvien='" + txtTenNv.Text + "' and matkhau='" + txtMk.Text + "'");
            ptbAnh.Image = new Bitmap(Application.StartupPath + "\\user\\" + Nv[0].AnhNhanVien);
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp;*.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.Visible = false;
                ptbAnh.Image.Dispose();

                PictureBox ptb = new PictureBox() { Height = 128, Width = 120 };
                ptb.Location = new Point(8, 8);
                ptb.SizeMode = PictureBoxSizeMode.Zoom;
                ptb.BorderStyle = BorderStyle.FixedSingle;
                ptb.Image = new Bitmap(open.FileName);
                pnlThongTin.Controls.Add(ptb);
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(Application.StartupPath + "\\user\\" + Nv[0].AnhNhanVien);
                ptb.Image.Save(Application.StartupPath + "\\user\\" + Nv[0].AnhNhanVien);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            txtMk.UseSystemPasswordChar = false;

        }

        private void btnXem_MouseLeave(object sender, EventArgs e)
        {
            txtMk.UseSystemPasswordChar = true;
        }

        private void btnDatlai_Click(object sender, EventArgs e)
        {
            ckbAll.Checked = true;
            cmbManv.SelectedIndex = cmbManv.Items.IndexOf("Tất cả");
            cmbBan.SelectedIndex = cmbBan.Items.IndexOf("Tất cả");
            cmbSx.SelectedIndex = cmbSx.Items.IndexOf("Mã hoá đơn");
            cmbLocThanhToan.SelectedIndex = 0;
            cmbSxTg.SelectedIndex = 0;

        }

        private void ckbTatCaHd_CheckedChanged(object sender, EventArgs e)
        {
            string nDay = dtpHoaDon.Value.Day.ToString();
            string nMonth = dtpHoaDon.Value.Month.ToString();
            string nYear = dtpHoaDon.Value.Year.ToString();
            string date = nMonth + "/" + nDay + "/" + nYear;
            if (ckbTatCaHd.CheckState == CheckState.Checked)
            {

                dtgvhd.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{txtMaNv.Text}' ");
                txtSLhd.Text = (dtgvhd.Rows.Count - 1).ToString();
            }
            else
            {
                dtgvhd.DataSource = Database.Instance.KetNoiSql($"select * from hoadon where manhanvien='{txtMaNv.Text}' and ngaythanhtoan='{date}'");
                txtSLhd.Text = (dtgvhd.Rows.Count - 1).ToString();
            }
        }

        private void btnAd_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap dn = new DangNhap();
            dn.ShowDialog();
        }
        void loadData()
        {
            dtgvMonAnTh.Columns.Clear();
            dtgvMonAnTh.DataSource = Database.Instance.KetNoiSql($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");
        }
        void loadChart()
        {
            chtMonAn.Series["Tiền"].Points.Clear();
            List<MonThinhHanh> mon = MonThinhHanhDAO.Instance.DsMonThinhHanh($"exec dbo.monThinhHanh {int.Parse(cmbMonth.SelectedItem.ToString())},{int.Parse(cmbYear.SelectedItem.ToString())}");
            chtMonAn.Visible = true;
            foreach (MonThinhHanh MonT_h in mon)
            {
                chtMonAn.Series["Tiền"].Points.AddXY(MonT_h.TenMonAn, MonT_h.PhanTram);
            }
            mon.Clear();
        }
    }
}





