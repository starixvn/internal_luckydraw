namespace YearEndParty.Client
{
    partial class AdvancedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedForm));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.imgStaff = new System.Windows.Forms.PictureBox();
            this.gvStaff = new System.Windows.Forms.DataGridView();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblDepartmentDetails = new System.Windows.Forms.Label();
            this.txtFullname = new System.Windows.Forms.TextBox();
            this.lblNickDetails = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.lblFullnameDetails = new System.Windows.Forms.Label();
            this.lblNickname = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblFullname = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStaff)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(832, 590);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 40);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "SUBMIT";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // imgStaff
            // 
            this.imgStaff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgStaff.Location = new System.Drawing.Point(789, 11);
            this.imgStaff.Name = "imgStaff";
            this.imgStaff.Size = new System.Drawing.Size(143, 167);
            this.imgStaff.TabIndex = 8;
            this.imgStaff.TabStop = false;
            // 
            // gvStaff
            // 
            this.gvStaff.AllowUserToAddRows = false;
            this.gvStaff.AllowUserToDeleteRows = false;
            this.gvStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStaff.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fullname,
            this.Nick,
            this.Barcode});
            this.gvStaff.Location = new System.Drawing.Point(12, 89);
            this.gvStaff.Name = "gvStaff";
            this.gvStaff.ReadOnly = true;
            this.gvStaff.Size = new System.Drawing.Size(630, 541);
            this.gvStaff.TabIndex = 10;
            this.gvStaff.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvStaff_CellMouseClick);
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "Fullname";
            this.Fullname.HeaderText = "Fullname";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            this.Fullname.Width = 240;
            // 
            // Nick
            // 
            this.Nick.DataPropertyName = "Nick";
            this.Nick.HeaderText = "Nick";
            this.Nick.Name = "Nick";
            this.Nick.ReadOnly = true;
            this.Nick.Width = 180;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "StaffCode";
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Width = 165;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(430, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 45);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblDepartmentDetails
            // 
            this.lblDepartmentDetails.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentDetails.ForeColor = System.Drawing.Color.Black;
            this.lblDepartmentDetails.Location = new System.Drawing.Point(577, 263);
            this.lblDepartmentDetails.Name = "lblDepartmentDetails";
            this.lblDepartmentDetails.Size = new System.Drawing.Size(355, 20);
            this.lblDepartmentDetails.TabIndex = 5;
            this.lblDepartmentDetails.Text = "Department";
            this.lblDepartmentDetails.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFullname
            // 
            this.txtFullname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullname.Location = new System.Drawing.Point(124, 11);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Size = new System.Drawing.Size(300, 20);
            this.txtFullname.TabIndex = 1;
            this.txtFullname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_Enter);
            // 
            // lblNickDetails
            // 
            this.lblNickDetails.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNickDetails.ForeColor = System.Drawing.Color.Black;
            this.lblNickDetails.Location = new System.Drawing.Point(577, 227);
            this.lblNickDetails.Name = "lblNickDetails";
            this.lblNickDetails.Size = new System.Drawing.Size(355, 20);
            this.lblNickDetails.TabIndex = 4;
            this.lblNickDetails.Text = "Nick";
            this.lblNickDetails.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNickname
            // 
            this.txtNickname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNickname.Location = new System.Drawing.Point(124, 38);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(300, 20);
            this.txtNickname.TabIndex = 2;
            this.txtNickname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_Enter);
            // 
            // lblFullnameDetails
            // 
            this.lblFullnameDetails.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullnameDetails.ForeColor = System.Drawing.Color.Black;
            this.lblFullnameDetails.Location = new System.Drawing.Point(577, 191);
            this.lblFullnameDetails.Name = "lblFullnameDetails";
            this.lblFullnameDetails.Size = new System.Drawing.Size(355, 20);
            this.lblFullnameDetails.TabIndex = 3;
            this.lblFullnameDetails.Text = "Fullname";
            this.lblFullnameDetails.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNickname
            // 
            this.lblNickname.AutoSize = true;
            this.lblNickname.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblNickname.Location = new System.Drawing.Point(13, 39);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(89, 19);
            this.lblNickname.TabIndex = 6;
            this.lblNickname.Text = "Nickname";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Location = new System.Drawing.Point(124, 64);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(300, 20);
            this.txtBarcode.TabIndex = 3;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_Enter);
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.Location = new System.Drawing.Point(27, 65);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(75, 19);
            this.lblBarcode.TabIndex = 6;
            this.lblBarcode.Text = "Barcode";
            // 
            // lblFullname
            // 
            this.lblFullname.AutoSize = true;
            this.lblFullname.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblFullname.Location = new System.Drawing.Point(14, 12);
            this.lblFullname.Name = "lblFullname";
            this.lblFullname.Size = new System.Drawing.Size(88, 19);
            this.lblFullname.TabIndex = 0;
            this.lblFullname.Text = "Full name";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(539, 39);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(103, 45);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // AdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 642);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.imgStaff);
            this.Controls.Add(this.gvStaff);
            this.Controls.Add(this.lblFullname);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.lblDepartmentDetails);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.txtFullname);
            this.Controls.Add(this.lblNickname);
            this.Controls.Add(this.lblNickDetails);
            this.Controls.Add(this.lblFullnameDetails);
            this.Controls.Add(this.txtNickname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdvancedForm";
            this.Text = "Advanced Zone";
            this.Load += new System.EventHandler(this.AdvancedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStaff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.PictureBox imgStaff;
        private System.Windows.Forms.DataGridView gvStaff;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblDepartmentDetails;
        private System.Windows.Forms.TextBox txtFullname;
        private System.Windows.Forms.Label lblNickDetails;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.Label lblFullnameDetails;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblFullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nick;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.Button btnReset;
    }
}