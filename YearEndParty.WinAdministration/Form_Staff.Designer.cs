namespace YearEndParty.WinAdministration
{
    partial class Form_Staff
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.group_StaffList = new System.Windows.Forms.GroupBox();
            this.gridView_Staff = new System.Windows.Forms.DataGridView();
            this.btn_AddNewStaff = new System.Windows.Forms.Button();
            this.group_StaffInformation = new System.Windows.Forms.GroupBox();
            this.group_OtherCompany = new System.Windows.Forms.GroupBox();
            this.txt_Department = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.picture_Avatar = new System.Windows.Forms.PictureBox();
            this.combo_Department = new System.Windows.Forms.ComboBox();
            this.btn_SelectImage = new System.Windows.Forms.Button();
            this.txt_NickName = new System.Windows.Forms.TextBox();
            this.txt_LastName = new System.Windows.Forms.TextBox();
            this.txt_FirstName = new System.Windows.Forms.TextBox();
            this.txt_StaffCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFile_Avatar = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.combo_DeptSearch = new System.Windows.Forms.ComboBox();
            this.openFile_Excel = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker_ImportExcel = new System.ComponentModel.BackgroundWorker();
            this.btn_ImportImages = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbb_Department = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.group_StaffList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Staff)).BeginInit();
            this.group_StaffInformation.SuspendLayout();
            this.group_OtherCompany.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Avatar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_StaffList
            // 
            this.group_StaffList.Controls.Add(this.gridView_Staff);
            this.group_StaffList.Location = new System.Drawing.Point(11, 25);
            this.group_StaffList.Name = "group_StaffList";
            this.group_StaffList.Size = new System.Drawing.Size(801, 322);
            this.group_StaffList.TabIndex = 5;
            this.group_StaffList.TabStop = false;
            this.group_StaffList.Text = "Staff List";
            // 
            // gridView_Staff
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView_Staff.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView_Staff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridView_Staff.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridView_Staff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView_Staff.Location = new System.Drawing.Point(3, 16);
            this.gridView_Staff.MultiSelect = false;
            this.gridView_Staff.Name = "gridView_Staff";
            this.gridView_Staff.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView_Staff.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridView_Staff.Size = new System.Drawing.Size(795, 303);
            this.gridView_Staff.TabIndex = 0;
            this.gridView_Staff.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_Staff_CellClick);
            // 
            // btn_AddNewStaff
            // 
            this.btn_AddNewStaff.Location = new System.Drawing.Point(12, 357);
            this.btn_AddNewStaff.Name = "btn_AddNewStaff";
            this.btn_AddNewStaff.Size = new System.Drawing.Size(109, 23);
            this.btn_AddNewStaff.TabIndex = 6;
            this.btn_AddNewStaff.Text = "Add New Staff";
            this.btn_AddNewStaff.UseVisualStyleBackColor = true;
            this.btn_AddNewStaff.Click += new System.EventHandler(this.btn_AddNewStaff_Click);
            // 
            // group_StaffInformation
            // 
            this.group_StaffInformation.Controls.Add(this.group_OtherCompany);
            this.group_StaffInformation.Controls.Add(this.btn_Cancel);
            this.group_StaffInformation.Controls.Add(this.btn_Delete);
            this.group_StaffInformation.Controls.Add(this.btn_Save);
            this.group_StaffInformation.Controls.Add(this.picture_Avatar);
            this.group_StaffInformation.Controls.Add(this.combo_Department);
            this.group_StaffInformation.Controls.Add(this.btn_SelectImage);
            this.group_StaffInformation.Controls.Add(this.txt_NickName);
            this.group_StaffInformation.Controls.Add(this.txt_LastName);
            this.group_StaffInformation.Controls.Add(this.txt_FirstName);
            this.group_StaffInformation.Controls.Add(this.txt_StaffCode);
            this.group_StaffInformation.Controls.Add(this.label6);
            this.group_StaffInformation.Controls.Add(this.label5);
            this.group_StaffInformation.Controls.Add(this.label3);
            this.group_StaffInformation.Controls.Add(this.label7);
            this.group_StaffInformation.Controls.Add(this.label2);
            this.group_StaffInformation.Controls.Add(this.label1);
            this.group_StaffInformation.Enabled = false;
            this.group_StaffInformation.Location = new System.Drawing.Point(12, 384);
            this.group_StaffInformation.Name = "group_StaffInformation";
            this.group_StaffInformation.Size = new System.Drawing.Size(800, 194);
            this.group_StaffInformation.TabIndex = 9;
            this.group_StaffInformation.TabStop = false;
            this.group_StaffInformation.Text = "Staff Information";
            // 
            // group_OtherCompany
            // 
            this.group_OtherCompany.Controls.Add(this.txt_Department);
            this.group_OtherCompany.Location = new System.Drawing.Point(218, 22);
            this.group_OtherCompany.Name = "group_OtherCompany";
            this.group_OtherCompany.Size = new System.Drawing.Size(188, 67);
            this.group_OtherCompany.TabIndex = 6;
            this.group_OtherCompany.TabStop = false;
            this.group_OtherCompany.Text = "Other Company";
            this.group_OtherCompany.Visible = false;
            // 
            // txt_Department
            // 
            this.txt_Department.Location = new System.Drawing.Point(11, 27);
            this.txt_Department.Name = "txt_Department";
            this.txt_Department.Size = new System.Drawing.Size(163, 20);
            this.txt_Department.TabIndex = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(182, 160);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Location = new System.Drawing.Point(101, 159);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 8;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(18, 159);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // picture_Avatar
            // 
            this.picture_Avatar.BackColor = System.Drawing.SystemColors.Info;
            this.picture_Avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture_Avatar.Location = new System.Drawing.Point(548, 25);
            this.picture_Avatar.Name = "picture_Avatar";
            this.picture_Avatar.Size = new System.Drawing.Size(120, 130);
            this.picture_Avatar.TabIndex = 3;
            this.picture_Avatar.TabStop = false;
            // 
            // combo_Department
            // 
            this.combo_Department.FormattingEnabled = true;
            this.combo_Department.Location = new System.Drawing.Point(101, 47);
            this.combo_Department.Name = "combo_Department";
            this.combo_Department.Size = new System.Drawing.Size(100, 21);
            this.combo_Department.TabIndex = 2;
            this.combo_Department.SelectedIndexChanged += new System.EventHandler(this.combo_Department_SelectedIndexChanged);
            // 
            // btn_SelectImage
            // 
            this.btn_SelectImage.Location = new System.Drawing.Point(548, 160);
            this.btn_SelectImage.Name = "btn_SelectImage";
            this.btn_SelectImage.Size = new System.Drawing.Size(120, 23);
            this.btn_SelectImage.TabIndex = 10;
            this.btn_SelectImage.Text = "Select Image";
            this.btn_SelectImage.UseVisualStyleBackColor = true;
            this.btn_SelectImage.Click += new System.EventHandler(this.btn_SelectImage_Click);
            // 
            // txt_NickName
            // 
            this.txt_NickName.Location = new System.Drawing.Point(101, 123);
            this.txt_NickName.Name = "txt_NickName";
            this.txt_NickName.Size = new System.Drawing.Size(100, 20);
            this.txt_NickName.TabIndex = 5;
            // 
            // txt_LastName
            // 
            this.txt_LastName.Location = new System.Drawing.Point(101, 98);
            this.txt_LastName.Name = "txt_LastName";
            this.txt_LastName.Size = new System.Drawing.Size(100, 20);
            this.txt_LastName.TabIndex = 4;
            // 
            // txt_FirstName
            // 
            this.txt_FirstName.Location = new System.Drawing.Point(101, 73);
            this.txt_FirstName.Name = "txt_FirstName";
            this.txt_FirstName.Size = new System.Drawing.Size(100, 20);
            this.txt_FirstName.TabIndex = 3;
            // 
            // txt_StaffCode
            // 
            this.txt_StaffCode.Location = new System.Drawing.Point(101, 22);
            this.txt_StaffCode.Name = "txt_StaffCode";
            this.txt_StaffCode.Size = new System.Drawing.Size(100, 20);
            this.txt_StaffCode.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Nick Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Last Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "First Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(478, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Avatar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Department:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Staff Code:";
            // 
            // openFile_Avatar
            // 
            this.openFile_Avatar.FileName = "openFile_Avatar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(196, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Staff Code:";
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(285, 6);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(111, 20);
            this.txt_Search.TabIndex = 1;
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(633, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(74, 23);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Back
            // 
            this.btn_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Back.Location = new System.Drawing.Point(722, 3);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(89, 23);
            this.btn_Back.TabIndex = 4;
            this.btn_Back.Text = "Refresh";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Enabled = false;
            this.btn_Next.Location = new System.Drawing.Point(729, 357);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 23);
            this.btn_Next.TabIndex = 8;
            this.btn_Next.Text = "NEXT";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Enabled = false;
            this.btn_Previous.Location = new System.Drawing.Point(635, 357);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(75, 23);
            this.btn_Previous.TabIndex = 7;
            this.btn_Previous.Text = "PREVIOUS";
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(412, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Department:";
            // 
            // combo_DeptSearch
            // 
            this.combo_DeptSearch.FormattingEnabled = true;
            this.combo_DeptSearch.Location = new System.Drawing.Point(494, 5);
            this.combo_DeptSearch.Name = "combo_DeptSearch";
            this.combo_DeptSearch.Size = new System.Drawing.Size(121, 21);
            this.combo_DeptSearch.TabIndex = 2;
            // 
            // openFile_Excel
            // 
            this.openFile_Excel.Multiselect = true;
            this.openFile_Excel.SupportMultiDottedExtensions = true;
            // 
            // btn_ImportImages
            // 
            this.btn_ImportImages.Location = new System.Drawing.Point(419, 21);
            this.btn_ImportImages.Name = "btn_ImportImages";
            this.btn_ImportImages.Size = new System.Drawing.Size(75, 23);
            this.btn_ImportImages.TabIndex = 10;
            this.btn_ImportImages.Text = "Import";
            this.btn_ImportImages.UseVisualStyleBackColor = true;
            this.btn_ImportImages.Click += new System.EventHandler(this.btn_ImportImages_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbb_Department);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btn_ImportImages);
            this.groupBox1.Location = new System.Drawing.Point(12, 579);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 57);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Images";
            // 
            // cbb_Department
            // 
            this.cbb_Department.FormattingEnabled = true;
            this.cbb_Department.Location = new System.Drawing.Point(101, 22);
            this.cbb_Department.Name = "cbb_Department";
            this.cbb_Department.Size = new System.Drawing.Size(305, 21);
            this.cbb_Department.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Department:";
            // 
            // Form_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(824, 687);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.combo_DeptSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.group_StaffInformation);
            this.Controls.Add(this.btn_AddNewStaff);
            this.Controls.Add(this.group_StaffList);
            this.MinimumSize = new System.Drawing.Size(840, 725);
            this.Name = "Form_Staff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Year End Party - Staff";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.group_StaffList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Staff)).EndInit();
            this.group_StaffInformation.ResumeLayout(false);
            this.group_StaffInformation.PerformLayout();
            this.group_OtherCompany.ResumeLayout(false);
            this.group_OtherCompany.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Avatar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox group_StaffList;
        private System.Windows.Forms.DataGridView gridView_Staff;
        private System.Windows.Forms.Button btn_AddNewStaff;
        private System.Windows.Forms.GroupBox group_StaffInformation;
        private System.Windows.Forms.PictureBox picture_Avatar;
        private System.Windows.Forms.ComboBox combo_Department;
        private System.Windows.Forms.Button btn_SelectImage;
        private System.Windows.Forms.TextBox txt_NickName;
        private System.Windows.Forms.TextBox txt_LastName;
        private System.Windows.Forms.TextBox txt_FirstName;
        private System.Windows.Forms.TextBox txt_Department;
        private System.Windows.Forms.TextBox txt_StaffCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.OpenFileDialog openFile_Avatar;
        private System.Windows.Forms.GroupBox group_OtherCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox combo_DeptSearch;
        private System.Windows.Forms.OpenFileDialog openFile_Excel;
        private System.ComponentModel.BackgroundWorker backgroundWorker_ImportExcel;
        private System.Windows.Forms.Button btn_ImportImages;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbb_Department;
        private System.Windows.Forms.Label label9;
    }
}