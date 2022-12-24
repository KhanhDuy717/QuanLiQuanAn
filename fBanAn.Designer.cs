
namespace QuanLiQuanAn
{
    partial class fBanAn
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
            this.flpBanAn = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimKiemBan = new System.Windows.Forms.TextBox();
            this.btnTimBan = new System.Windows.Forms.Button();
            this.btnXacNhanBan = new System.Windows.Forms.Button();
            this.btnTrong = new System.Windows.Forms.Button();
            this.btnDay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flpBanAn
            // 
            this.flpBanAn.AutoScroll = true;
            this.flpBanAn.BackColor = System.Drawing.Color.Transparent;
            this.flpBanAn.Location = new System.Drawing.Point(9, 106);
            this.flpBanAn.Name = "flpBanAn";
            this.flpBanAn.Size = new System.Drawing.Size(779, 332);
            this.flpBanAn.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(307, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Danh sách bàn ăn";
            // 
            // txtTimKiemBan
            // 
            this.txtTimKiemBan.Location = new System.Drawing.Point(206, 66);
            this.txtTimKiemBan.Name = "txtTimKiemBan";
            this.txtTimKiemBan.Size = new System.Drawing.Size(385, 20);
            this.txtTimKiemBan.TabIndex = 2;
            // 
            // btnTimBan
            // 
            this.btnTimBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimBan.Location = new System.Drawing.Point(606, 63);
            this.btnTimBan.Name = "btnTimBan";
            this.btnTimBan.Size = new System.Drawing.Size(87, 25);
            this.btnTimBan.TabIndex = 3;
            this.btnTimBan.Text = "Tìm bàn ";
            this.btnTimBan.UseVisualStyleBackColor = true;
            this.btnTimBan.Click += new System.EventHandler(this.btnTimBan_Click);
            // 
            // btnXacNhanBan
            // 
            this.btnXacNhanBan.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnXacNhanBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanBan.Location = new System.Drawing.Point(701, 63);
            this.btnXacNhanBan.Name = "btnXacNhanBan";
            this.btnXacNhanBan.Size = new System.Drawing.Size(87, 25);
            this.btnXacNhanBan.TabIndex = 4;
            this.btnXacNhanBan.Text = "Xác nhận bàn";
            this.btnXacNhanBan.UseVisualStyleBackColor = false;
            // 
            // btnTrong
            // 
            this.btnTrong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrong.Location = new System.Drawing.Point(12, 61);
            this.btnTrong.Name = "btnTrong";
            this.btnTrong.Size = new System.Drawing.Size(87, 25);
            this.btnTrong.TabIndex = 5;
            this.btnTrong.Text = "Bàn trống";
            this.btnTrong.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTrong.UseVisualStyleBackColor = true;
            this.btnTrong.Click += new System.EventHandler(this.btnTrong_Click);
            // 
            // btnDay
            // 
            this.btnDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDay.Location = new System.Drawing.Point(105, 61);
            this.btnDay.Name = "btnDay";
            this.btnDay.Size = new System.Drawing.Size(87, 25);
            this.btnDay.TabIndex = 6;
            this.btnDay.Text = "Bàn đầy";
            this.btnDay.UseVisualStyleBackColor = true;
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // fBanAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::QuanLiQuanAn.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDay);
            this.Controls.Add(this.btnTrong);
            this.Controls.Add(this.btnXacNhanBan);
            this.Controls.Add(this.btnTimBan);
            this.Controls.Add(this.txtTimKiemBan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpBanAn);
            this.MaximizeBox = false;
            this.Name = "fBanAn";
            this.Text = "Bàn ăn";
            this.Load += new System.EventHandler(this.fBanAn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpBanAn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimKiemBan;
        private System.Windows.Forms.Button btnTimBan;
        private System.Windows.Forms.Button btnXacNhanBan;
        private System.Windows.Forms.Button btnTrong;
        private System.Windows.Forms.Button btnDay;
    }
}