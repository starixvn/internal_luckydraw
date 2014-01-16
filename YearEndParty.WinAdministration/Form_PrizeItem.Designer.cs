namespace YearEndParty.WinAdministration
{
    partial class Form_PrizeItem
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
            this.group_PrizeList = new System.Windows.Forms.GroupBox();
            this.gridView_PrizeList = new System.Windows.Forms.DataGridView();
            this.btn_AddNewPrize = new System.Windows.Forms.Button();
            this.group_PrizeInformation = new System.Windows.Forms.GroupBox();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.picture_Prize = new System.Windows.Forms.PictureBox();
            this.txt_PrizeIndex = new System.Windows.Forms.TextBox();
            this.txt_PrizeName = new System.Windows.Forms.TextBox();
            this.combo_PrizeType = new System.Windows.Forms.ComboBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_DeletePrize = new System.Windows.Forms.Button();
            this.btn_SavePrize = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFile_Image = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Show = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.combo_SelType = new System.Windows.Forms.ComboBox();
            this.group_PrizeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PrizeList)).BeginInit();
            this.group_PrizeInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Prize)).BeginInit();
            this.SuspendLayout();
            // 
            // group_PrizeList
            // 
            this.group_PrizeList.Controls.Add(this.gridView_PrizeList);
            this.group_PrizeList.Location = new System.Drawing.Point(12, 27);
            this.group_PrizeList.Name = "group_PrizeList";
            this.group_PrizeList.Size = new System.Drawing.Size(800, 336);
            this.group_PrizeList.TabIndex = 1;
            this.group_PrizeList.TabStop = false;
            this.group_PrizeList.Text = "Prize List";
            // 
            // gridView_PrizeList
            // 
            this.gridView_PrizeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView_PrizeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView_PrizeList.Location = new System.Drawing.Point(3, 16);
            this.gridView_PrizeList.MultiSelect = false;
            this.gridView_PrizeList.Name = "gridView_PrizeList";
            this.gridView_PrizeList.ReadOnly = true;
            this.gridView_PrizeList.Size = new System.Drawing.Size(794, 317);
            this.gridView_PrizeList.TabIndex = 0;
            this.gridView_PrizeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_PrizeList_CellClick);
            // 
            // btn_AddNewPrize
            // 
            this.btn_AddNewPrize.Location = new System.Drawing.Point(15, 369);
            this.btn_AddNewPrize.Name = "btn_AddNewPrize";
            this.btn_AddNewPrize.Size = new System.Drawing.Size(114, 23);
            this.btn_AddNewPrize.TabIndex = 2;
            this.btn_AddNewPrize.Text = "Add New Prize";
            this.btn_AddNewPrize.UseVisualStyleBackColor = true;
            this.btn_AddNewPrize.Click += new System.EventHandler(this.btn_AddNewPrize_Click);
            // 
            // group_PrizeInformation
            // 
            this.group_PrizeInformation.Controls.Add(this.btn_SelectFile);
            this.group_PrizeInformation.Controls.Add(this.picture_Prize);
            this.group_PrizeInformation.Controls.Add(this.txt_PrizeIndex);
            this.group_PrizeInformation.Controls.Add(this.txt_PrizeName);
            this.group_PrizeInformation.Controls.Add(this.combo_PrizeType);
            this.group_PrizeInformation.Controls.Add(this.btn_Cancel);
            this.group_PrizeInformation.Controls.Add(this.btn_DeletePrize);
            this.group_PrizeInformation.Controls.Add(this.btn_SavePrize);
            this.group_PrizeInformation.Controls.Add(this.label6);
            this.group_PrizeInformation.Controls.Add(this.label3);
            this.group_PrizeInformation.Controls.Add(this.label2);
            this.group_PrizeInformation.Controls.Add(this.label1);
            this.group_PrizeInformation.Enabled = false;
            this.group_PrizeInformation.Location = new System.Drawing.Point(15, 408);
            this.group_PrizeInformation.Name = "group_PrizeInformation";
            this.group_PrizeInformation.Size = new System.Drawing.Size(794, 194);
            this.group_PrizeInformation.TabIndex = 2;
            this.group_PrizeInformation.TabStop = false;
            this.group_PrizeInformation.Text = "Prize Information";
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(100, 117);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(102, 23);
            this.btn_SelectFile.TabIndex = 8;
            this.btn_SelectFile.Text = "Select Image";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // picture_Prize
            // 
            this.picture_Prize.BackColor = System.Drawing.SystemColors.Info;
            this.picture_Prize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picture_Prize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture_Prize.Location = new System.Drawing.Point(604, 23);
            this.picture_Prize.Name = "picture_Prize";
            this.picture_Prize.Size = new System.Drawing.Size(165, 135);
            this.picture_Prize.TabIndex = 4;
            this.picture_Prize.TabStop = false;
            // 
            // txt_PrizeIndex
            // 
            this.txt_PrizeIndex.Location = new System.Drawing.Point(100, 85);
            this.txt_PrizeIndex.Name = "txt_PrizeIndex";
            this.txt_PrizeIndex.Size = new System.Drawing.Size(102, 20);
            this.txt_PrizeIndex.TabIndex = 3;
            // 
            // txt_PrizeName
            // 
            this.txt_PrizeName.Location = new System.Drawing.Point(100, 53);
            this.txt_PrizeName.Name = "txt_PrizeName";
            this.txt_PrizeName.Size = new System.Drawing.Size(466, 20);
            this.txt_PrizeName.TabIndex = 2;
            // 
            // combo_PrizeType
            // 
            this.combo_PrizeType.FormattingEnabled = true;
            this.combo_PrizeType.Location = new System.Drawing.Point(100, 20);
            this.combo_PrizeType.Name = "combo_PrizeType";
            this.combo_PrizeType.Size = new System.Drawing.Size(102, 21);
            this.combo_PrizeType.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(181, 155);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_DeletePrize
            // 
            this.btn_DeletePrize.Enabled = false;
            this.btn_DeletePrize.Location = new System.Drawing.Point(100, 155);
            this.btn_DeletePrize.Name = "btn_DeletePrize";
            this.btn_DeletePrize.Size = new System.Drawing.Size(75, 23);
            this.btn_DeletePrize.TabIndex = 6;
            this.btn_DeletePrize.Text = "Delete";
            this.btn_DeletePrize.UseVisualStyleBackColor = true;
            this.btn_DeletePrize.Click += new System.EventHandler(this.btn_DeletePrize_Click);
            // 
            // btn_SavePrize
            // 
            this.btn_SavePrize.Location = new System.Drawing.Point(19, 155);
            this.btn_SavePrize.Name = "btn_SavePrize";
            this.btn_SavePrize.Size = new System.Drawing.Size(75, 23);
            this.btn_SavePrize.TabIndex = 5;
            this.btn_SavePrize.Text = "Save";
            this.btn_SavePrize.UseVisualStyleBackColor = true;
            this.btn_SavePrize.Click += new System.EventHandler(this.btn_SavePrize_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Image:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Prize Index:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Prize Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Prize Type:";
            // 
            // openFile_Image
            // 
            this.openFile_Image.FileName = "openFile_Image";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Prize Type:";
            // 
            // btn_Show
            // 
            this.btn_Show.Location = new System.Drawing.Point(649, 6);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(75, 23);
            this.btn_Show.TabIndex = 5;
            this.btn_Show.Text = "Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(734, 6);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 5;
            this.btn_Back.Text = "Refresh";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // combo_SelType
            // 
            this.combo_SelType.FormattingEnabled = true;
            this.combo_SelType.Location = new System.Drawing.Point(503, 6);
            this.combo_SelType.Name = "combo_SelType";
            this.combo_SelType.Size = new System.Drawing.Size(140, 21);
            this.combo_SelType.TabIndex = 6;
            // 
            // Form_PrizeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(824, 664);
            this.ControlBox = false;
            this.Controls.Add(this.combo_SelType);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.group_PrizeInformation);
            this.Controls.Add(this.btn_AddNewPrize);
            this.Controls.Add(this.group_PrizeList);
            this.MinimumSize = new System.Drawing.Size(840, 680);
            this.Name = "Form_PrizeItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Year End Party - Prize Item";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.group_PrizeList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_PrizeList)).EndInit();
            this.group_PrizeInformation.ResumeLayout(false);
            this.group_PrizeInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Prize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox group_PrizeList;
        private System.Windows.Forms.DataGridView gridView_PrizeList;
        private System.Windows.Forms.Button btn_AddNewPrize;
        private System.Windows.Forms.GroupBox group_PrizeInformation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_PrizeIndex;
        private System.Windows.Forms.TextBox txt_PrizeName;
        private System.Windows.Forms.ComboBox combo_PrizeType;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_DeletePrize;
        private System.Windows.Forms.Button btn_SavePrize;
        private System.Windows.Forms.PictureBox picture_Prize;
        private System.Windows.Forms.OpenFileDialog openFile_Image;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.ComboBox combo_SelType;
        private System.Windows.Forms.Label label6;
    }
}