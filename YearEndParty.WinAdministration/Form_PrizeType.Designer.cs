namespace YearEndParty.WinAdministration
{
    partial class Form_PrizeType
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
            this.group_ListSetting = new System.Windows.Forms.GroupBox();
            this.gridView_PrizeType = new System.Windows.Forms.DataGridView();
            this.btn_AddPrizeType = new System.Windows.Forms.Button();
            this.group_Information = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxDuplicateFromAdHoc = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDuplicateAllow = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_SavePrizeType = new System.Windows.Forms.Button();
            this.txt_PrizeQuantity = new System.Windows.Forms.TextBox();
            this.txt_PrizeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.group_ListSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PrizeType)).BeginInit();
            this.group_Information.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_ListSetting
            // 
            this.group_ListSetting.Controls.Add(this.gridView_PrizeType);
            this.group_ListSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_ListSetting.Location = new System.Drawing.Point(12, 8);
            this.group_ListSetting.Name = "group_ListSetting";
            this.group_ListSetting.Size = new System.Drawing.Size(800, 356);
            this.group_ListSetting.TabIndex = 1;
            this.group_ListSetting.TabStop = false;
            this.group_ListSetting.Text = "Prize Type List";
            // 
            // gridView_PrizeType
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView_PrizeType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView_PrizeType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridView_PrizeType.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridView_PrizeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView_PrizeType.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridView_PrizeType.Location = new System.Drawing.Point(3, 16);
            this.gridView_PrizeType.MultiSelect = false;
            this.gridView_PrizeType.Name = "gridView_PrizeType";
            this.gridView_PrizeType.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView_PrizeType.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridView_PrizeType.Size = new System.Drawing.Size(794, 337);
            this.gridView_PrizeType.TabIndex = 0;
            this.gridView_PrizeType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellClick);
            // 
            // btn_AddPrizeType
            // 
            this.btn_AddPrizeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddPrizeType.Location = new System.Drawing.Point(12, 370);
            this.btn_AddPrizeType.Name = "btn_AddPrizeType";
            this.btn_AddPrizeType.Size = new System.Drawing.Size(108, 23);
            this.btn_AddPrizeType.TabIndex = 2;
            this.btn_AddPrizeType.Text = "Add Prize Type";
            this.btn_AddPrizeType.UseVisualStyleBackColor = true;
            this.btn_AddPrizeType.Click += new System.EventHandler(this.btn_AddPrizeType_Click);
            // 
            // group_Information
            // 
            this.group_Information.Controls.Add(this.label5);
            this.group_Information.Controls.Add(this.checkBoxDuplicateFromAdHoc);
            this.group_Information.Controls.Add(this.label4);
            this.group_Information.Controls.Add(this.checkBoxDuplicateAllow);
            this.group_Information.Controls.Add(this.label3);
            this.group_Information.Controls.Add(this.btn_Cancel);
            this.group_Information.Controls.Add(this.btn_Delete);
            this.group_Information.Controls.Add(this.btn_SavePrizeType);
            this.group_Information.Controls.Add(this.txt_PrizeQuantity);
            this.group_Information.Controls.Add(this.txt_PrizeName);
            this.group_Information.Controls.Add(this.label2);
            this.group_Information.Controls.Add(this.label1);
            this.group_Information.Enabled = false;
            this.group_Information.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_Information.Location = new System.Drawing.Point(12, 398);
            this.group_Information.Name = "group_Information";
            this.group_Information.Size = new System.Drawing.Size(797, 204);
            this.group_Information.TabIndex = 2;
            this.group_Information.TabStop = false;
            this.group_Information.Text = "Prize Type Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "(with super grand/grand/cash prizes)";
            // 
            // checkBoxDuplicateFromAdHoc
            // 
            this.checkBoxDuplicateFromAdHoc.AutoSize = true;
            this.checkBoxDuplicateFromAdHoc.Location = new System.Drawing.Point(146, 144);
            this.checkBoxDuplicateFromAdHoc.Name = "checkBoxDuplicateFromAdHoc";
            this.checkBoxDuplicateFromAdHoc.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDuplicateFromAdHoc.TabIndex = 10;
            this.checkBoxDuplicateFromAdHoc.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Duplicate with Ad Hoc";
            // 
            // checkBoxDuplicateAllow
            // 
            this.checkBoxDuplicateAllow.AutoSize = true;
            this.checkBoxDuplicateAllow.Location = new System.Drawing.Point(146, 119);
            this.checkBoxDuplicateAllow.Name = "checkBoxDuplicateAllow";
            this.checkBoxDuplicateAllow.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDuplicateAllow.TabIndex = 8;
            this.checkBoxDuplicateAllow.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Allow Duplicate";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(200, 171);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Location = new System.Drawing.Point(109, 171);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 5;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_SavePrizeType
            // 
            this.btn_SavePrizeType.Location = new System.Drawing.Point(23, 171);
            this.btn_SavePrizeType.Name = "btn_SavePrizeType";
            this.btn_SavePrizeType.Size = new System.Drawing.Size(75, 23);
            this.btn_SavePrizeType.TabIndex = 4;
            this.btn_SavePrizeType.Text = "Save";
            this.btn_SavePrizeType.UseVisualStyleBackColor = true;
            this.btn_SavePrizeType.Click += new System.EventHandler(this.btn_SavePrizeType_Click);
            // 
            // txt_PrizeQuantity
            // 
            this.txt_PrizeQuantity.Location = new System.Drawing.Point(146, 84);
            this.txt_PrizeQuantity.Name = "txt_PrizeQuantity";
            this.txt_PrizeQuantity.Size = new System.Drawing.Size(86, 20);
            this.txt_PrizeQuantity.TabIndex = 3;
            // 
            // txt_PrizeName
            // 
            this.txt_PrizeName.Location = new System.Drawing.Point(146, 54);
            this.txt_PrizeName.Name = "txt_PrizeName";
            this.txt_PrizeName.Size = new System.Drawing.Size(194, 20);
            this.txt_PrizeName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quantity:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Prize Name:";
            // 
            // Form_PrizeType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(824, 664);
            this.ControlBox = false;
            this.Controls.Add(this.btn_AddPrizeType);
            this.Controls.Add(this.group_Information);
            this.Controls.Add(this.group_ListSetting);
            this.MinimumSize = new System.Drawing.Size(840, 680);
            this.Name = "Form_PrizeType";
            this.Text = "Year End Party - Prizes Setting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.group_ListSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PrizeType)).EndInit();
            this.group_Information.ResumeLayout(false);
            this.group_Information.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group_ListSetting;
        private System.Windows.Forms.Button btn_AddPrizeType;
        private System.Windows.Forms.GroupBox group_Information;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridView_PrizeType;
        private System.Windows.Forms.TextBox txt_PrizeQuantity;
        private System.Windows.Forms.TextBox txt_PrizeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_SavePrizeType;
        private System.Windows.Forms.CheckBox checkBoxDuplicateFromAdHoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxDuplicateAllow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}