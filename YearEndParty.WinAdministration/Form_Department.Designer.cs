namespace YearEndParty.WinAdministration
{
    partial class Form_Department
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Department));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridView_Department = new System.Windows.Forms.DataGridView();
            this.btn_AddDepartment = new System.Windows.Forms.Button();
            this.group_Information = new System.Windows.Forms.GroupBox();
            this.txt_DescDepartment = new System.Windows.Forms.TextBox();
            this.txt_NameDeparment = new System.Windows.Forms.TextBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SaveDepartment = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Department)).BeginInit();
            this.group_Information.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridView_Department);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // gridView_Department
            // 
            this.gridView_Department.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridView_Department, "gridView_Department");
            this.gridView_Department.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridView_Department.MultiSelect = false;
            this.gridView_Department.Name = "gridView_Department";
            this.gridView_Department.ReadOnly = true;
            this.gridView_Department.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_Department_CellContentClick);
            // 
            // btn_AddDepartment
            // 
            resources.ApplyResources(this.btn_AddDepartment, "btn_AddDepartment");
            this.btn_AddDepartment.Name = "btn_AddDepartment";
            this.btn_AddDepartment.UseVisualStyleBackColor = true;
            this.btn_AddDepartment.Click += new System.EventHandler(this.btn_AddDepartment_Click);
            // 
            // group_Information
            // 
            this.group_Information.Controls.Add(this.txt_DescDepartment);
            this.group_Information.Controls.Add(this.txt_NameDeparment);
            this.group_Information.Controls.Add(this.btn_Delete);
            this.group_Information.Controls.Add(this.btn_Cancel);
            this.group_Information.Controls.Add(this.btn_SaveDepartment);
            this.group_Information.Controls.Add(this.label2);
            this.group_Information.Controls.Add(this.label1);
            resources.ApplyResources(this.group_Information, "group_Information");
            this.group_Information.Name = "group_Information";
            this.group_Information.TabStop = false;
            // 
            // txt_DescDepartment
            // 
            resources.ApplyResources(this.txt_DescDepartment, "txt_DescDepartment");
            this.txt_DescDepartment.Name = "txt_DescDepartment";
            // 
            // txt_NameDeparment
            // 
            resources.ApplyResources(this.txt_NameDeparment, "txt_NameDeparment");
            this.txt_NameDeparment.Name = "txt_NameDeparment";
            // 
            // btn_Delete
            // 
            resources.ApplyResources(this.btn_Delete, "btn_Delete");
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SaveDepartment
            // 
            resources.ApplyResources(this.btn_SaveDepartment, "btn_SaveDepartment");
            this.btn_SaveDepartment.Name = "btn_SaveDepartment";
            this.btn_SaveDepartment.UseVisualStyleBackColor = true;
            this.btn_SaveDepartment.Click += new System.EventHandler(this.btn_SaveDepartment_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Form_Department
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ControlBox = false;
            this.Controls.Add(this.group_Information);
            this.Controls.Add(this.btn_AddDepartment);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Department";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormDepartment_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Department)).EndInit();
            this.group_Information.ResumeLayout(false);
            this.group_Information.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AddDepartment;
        private System.Windows.Forms.GroupBox group_Information;
        private System.Windows.Forms.TextBox txt_DescDepartment;
        private System.Windows.Forms.TextBox txt_NameDeparment;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_SaveDepartment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridView_Department;
        private System.Windows.Forms.Button btn_Delete;

    }
}