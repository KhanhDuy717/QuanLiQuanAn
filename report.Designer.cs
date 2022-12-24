
namespace QuanLiQuanAn
{
    partial class report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportHoaDonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QuanLiQuanAnDataSet = new QuanLiQuanAn.QuanLiQuanAnDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportHoaDonTableAdapter = new QuanLiQuanAn.QuanLiQuanAnDataSetTableAdapters.reportHoaDonTableAdapter();
            this.quanLiQuanAnDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.reportHoaDonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLiQuanAnDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLiQuanAnDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportHoaDonBindingSource
            // 
            this.reportHoaDonBindingSource.DataMember = "reportHoaDon";
            this.reportHoaDonBindingSource.DataSource = this.QuanLiQuanAnDataSet;
            // 
            // QuanLiQuanAnDataSet
            // 
            this.QuanLiQuanAnDataSet.DataSetName = "QuanLiQuanAnDataSet";
            this.QuanLiQuanAnDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet";
            reportDataSource1.Value = this.reportHoaDonBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLiQuanAn.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(633, 394);
            this.reportViewer1.TabIndex = 0;
            // 
            // reportHoaDonTableAdapter
            // 
            this.reportHoaDonTableAdapter.ClearBeforeFill = true;
            // 
            // quanLiQuanAnDataSetBindingSource
            // 
            this.quanLiQuanAnDataSetBindingSource.DataSource = this.QuanLiQuanAnDataSet;
            this.quanLiQuanAnDataSetBindingSource.Position = 0;
            // 
            // report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 394);
            this.Controls.Add(this.reportViewer1);
            this.MaximizeBox = false;
            this.Name = "report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xuất hoá đơn";
            this.Load += new System.EventHandler(this.report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reportHoaDonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLiQuanAnDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLiQuanAnDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource reportHoaDonBindingSource;
        private QuanLiQuanAnDataSet QuanLiQuanAnDataSet;
        private QuanLiQuanAnDataSetTableAdapters.reportHoaDonTableAdapter reportHoaDonTableAdapter;
        private System.Windows.Forms.BindingSource quanLiQuanAnDataSetBindingSource;
    }
}