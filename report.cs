using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLiQuanAnDataSet1.Menu' table. You can move, or remove it, as needed.
            this.Icon = new System.Drawing.Icon(Application.StartupPath + "\\icon\\icon.ico");
            // TODO: This line of code loads data into the 'QuanLiQuanAnDataSet.reportHoaDon' table. You can move, or remove it, as needed.
            System.Windows.Forms.Form fQ = System.Windows.Forms.Application.OpenForms["fQuanLiQuanAn"];
            int num =int.Parse(((fQuanLiQuanAn)fQ).lvcthd.Items[0].SubItems[0].Text);
            this.reportHoaDonTableAdapter.Fill(this.QuanLiQuanAnDataSet.reportHoaDon,num);

            this.reportViewer1.RefreshReport();
        }

       
    }
}
