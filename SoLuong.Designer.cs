
namespace QuanLiQuanAn
{
    partial class SoLuong
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
            this.txtSl = new System.Windows.Forms.TextBox();
            this.btnXn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSl
            // 
            this.txtSl.Location = new System.Drawing.Point(42, 43);
            this.txtSl.Name = "txtSl";
            this.txtSl.Size = new System.Drawing.Size(192, 20);
            this.txtSl.TabIndex = 0;
            this.txtSl.Text = "1";
            // 
            // btnXn
            // 
            this.btnXn.Location = new System.Drawing.Point(103, 90);
            this.btnXn.Name = "btnXn";
            this.btnXn.Size = new System.Drawing.Size(71, 24);
            this.btnXn.TabIndex = 1;
            this.btnXn.Text = "Xác nhận";
            this.btnXn.UseVisualStyleBackColor = true;
            this.btnXn.Click += new System.EventHandler(this.btnXn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Số lượng ";
            // 
            // SoLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(286, 138);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnXn);
            this.Controls.Add(this.txtSl);
            this.MaximizeBox = false;
            this.Name = "SoLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Số lượng";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SoLuong_FormClosed);
            this.Load += new System.EventHandler(this.SoLuong_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSl;
        private System.Windows.Forms.Button btnXn;
        private System.Windows.Forms.Label label1;
    }
}