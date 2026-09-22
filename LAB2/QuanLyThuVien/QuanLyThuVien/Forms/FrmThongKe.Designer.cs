namespace QuanLyThuVien.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.lblMuon = new System.Windows.Forms.Label();
            this.lblQuaHan = new System.Windows.Forms.Label();
            this.lblMat = new System.Windows.Forms.Label();
            this.lblHuHong = new System.Windows.Forms.Label();
            this.lblPhiPhat = new System.Windows.Forms.Label();
            this.dgvPhat = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhat)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày :";
            // 
            // dtTu
            // 
            this.dtTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTu.Location = new System.Drawing.Point(120, 20);
            this.dtTu.Name = "dtTu";
            this.dtTu.Size = new System.Drawing.Size(150, 30);
            this.dtTu.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày :";
            // 
            // dtDen
            // 
            this.dtDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDen.Location = new System.Drawing.Point(400, 20);
            this.dtDen.Name = "dtDen";
            this.dtDen.Size = new System.Drawing.Size(150, 30);
            this.dtDen.TabIndex = 3;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(580, 18);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(140, 32);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // lblMuon
            // 
            this.lblMuon.AutoSize = true;
            this.lblMuon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMuon.Location = new System.Drawing.Point(20, 80);
            this.lblMuon.Name = "lblMuon";
            this.lblMuon.Size = new System.Drawing.Size(200, 25);
            this.lblMuon.TabIndex = 5;
            this.lblMuon.Text = "Lượt sách mượn: 0";
            // 
            // lblQuaHan
            // 
            this.lblQuaHan.AutoSize = true;
            this.lblQuaHan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuaHan.Location = new System.Drawing.Point(260, 80);
            this.lblQuaHan.Name = "lblQuaHan";
            this.lblQuaHan.Size = new System.Drawing.Size(180, 25);
            this.lblQuaHan.TabIndex = 6;
            this.lblQuaHan.Text = "Sách quá hạn: 0";
            // 
            // lblMat
            // 
            this.lblMat.AutoSize = true;
            this.lblMat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMat.Location = new System.Drawing.Point(480, 80);
            this.lblMat.Name = "lblMat";
            this.lblMat.Size = new System.Drawing.Size(140, 25);
            this.lblMat.TabIndex = 7;
            this.lblMat.Text = "Sách mất: 0";
            // 
            // lblHuHong
            // 
            this.lblHuHong.AutoSize = true;
            this.lblHuHong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHuHong.Location = new System.Drawing.Point(660, 80);
            this.lblHuHong.Name = "lblHuHong";
            this.lblHuHong.Size = new System.Drawing.Size(170, 25);
            this.lblHuHong.TabIndex = 8;
            this.lblHuHong.Text = "Sách hư hỏng: 0";
            // 
            // lblPhiPhat
            // 
            this.lblPhiPhat.AutoSize = true;
            this.lblPhiPhat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPhiPhat.Location = new System.Drawing.Point(20, 120);
            this.lblPhiPhat.Name = "lblPhiPhat";
            this.lblPhiPhat.Size = new System.Drawing.Size(200, 25);
            this.lblPhiPhat.TabIndex = 9;
            this.lblPhiPhat.Text = "Tổng phí phạt: 0 đ";
            // 
            // dgvPhat
            // 
            this.dgvPhat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhat.Location = new System.Drawing.Point(20, 170);
            this.dgvPhat.Name = "dgvPhat";
            this.dgvPhat.RowHeadersWidth = 51;
            this.dgvPhat.RowTemplate.Height = 24;
            this.dgvPhat.Size = new System.Drawing.Size(1000, 470);
            this.dgvPhat.TabIndex = 10;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(920, 660);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 30);
            this.btnDong.TabIndex = 11;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 700);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgvPhat);
            this.Controls.Add(this.lblPhiPhat);
            this.Controls.Add(this.lblHuHong);
            this.Controls.Add(this.lblMat);
            this.Controls.Add(this.lblQuaHan);
            this.Controls.Add(this.lblMuon);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dtDen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtTu);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.FrmThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDen;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Label lblMuon;
        private System.Windows.Forms.Label lblQuaHan;
        private System.Windows.Forms.Label lblMat;
        private System.Windows.Forms.Label lblHuHong;
        private System.Windows.Forms.Label lblPhiPhat;
        private System.Windows.Forms.DataGridView dgvPhat;
        private System.Windows.Forms.Button btnDong;
    }
}