namespace YearEndParty.WinAdministration
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_ExitMain = new System.Windows.Forms.Button();
            this.btn_Winner = new System.Windows.Forms.Button();
            this.btn_Staff = new System.Windows.Forms.Button();
            this.btn_PrizeItem = new System.Windows.Forms.Button();
            this.btn_PrizeType = new System.Windows.Forms.Button();
            this.btn_Department = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainer1.Panel1.Controls.Add(this.btn_ExitMain);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Winner);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Staff);
            this.splitContainer1.Panel1.Controls.Add(this.btn_PrizeItem);
            this.splitContainer1.Panel1.Controls.Add(this.btn_PrizeType);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Department);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(100, 10, 10, 10);
            this.splitContainer1.Size = new System.Drawing.Size(974, 662);
            this.splitContainer1.SplitterDistance = 158;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_ExitMain
            // 
            this.btn_ExitMain.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_ExitMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExitMain.Location = new System.Drawing.Point(0, 593);
            this.btn_ExitMain.Name = "btn_ExitMain";
            this.btn_ExitMain.Size = new System.Drawing.Size(155, 57);
            this.btn_ExitMain.TabIndex = 4;
            this.btn_ExitMain.Text = "Exit";
            this.btn_ExitMain.UseVisualStyleBackColor = false;
            this.btn_ExitMain.Click += new System.EventHandler(this.btn_ExitMain_Click);
            // 
            // btn_Winner
            // 
            this.btn_Winner.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Winner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Winner.Location = new System.Drawing.Point(3, 264);
            this.btn_Winner.Name = "btn_Winner";
            this.btn_Winner.Size = new System.Drawing.Size(155, 57);
            this.btn_Winner.TabIndex = 4;
            this.btn_Winner.Text = "Winner of LuckyDraw";
            this.btn_Winner.UseVisualStyleBackColor = false;
            this.btn_Winner.Click += new System.EventHandler(this.btn_Winner_Click);
            // 
            // btn_Staff
            // 
            this.btn_Staff.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Staff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Staff.Location = new System.Drawing.Point(3, 201);
            this.btn_Staff.Name = "btn_Staff";
            this.btn_Staff.Size = new System.Drawing.Size(155, 57);
            this.btn_Staff.TabIndex = 3;
            this.btn_Staff.Text = "Staff Management";
            this.btn_Staff.UseVisualStyleBackColor = false;
            this.btn_Staff.Click += new System.EventHandler(this.btn_Staff_Click);
            // 
            // btn_PrizeItem
            // 
            this.btn_PrizeItem.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_PrizeItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PrizeItem.Location = new System.Drawing.Point(3, 138);
            this.btn_PrizeItem.Name = "btn_PrizeItem";
            this.btn_PrizeItem.Size = new System.Drawing.Size(155, 57);
            this.btn_PrizeItem.TabIndex = 2;
            this.btn_PrizeItem.Text = "Prize Management";
            this.btn_PrizeItem.UseVisualStyleBackColor = false;
            this.btn_PrizeItem.Click += new System.EventHandler(this.btn_PrizeItem_Click);
            // 
            // btn_PrizeType
            // 
            this.btn_PrizeType.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_PrizeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PrizeType.Location = new System.Drawing.Point(3, 75);
            this.btn_PrizeType.Name = "btn_PrizeType";
            this.btn_PrizeType.Size = new System.Drawing.Size(155, 57);
            this.btn_PrizeType.TabIndex = 1;
            this.btn_PrizeType.Text = "Prize Type Setting";
            this.btn_PrizeType.UseVisualStyleBackColor = false;
            this.btn_PrizeType.Click += new System.EventHandler(this.btn_PrizeType_Click);
            // 
            // btn_Department
            // 
            this.btn_Department.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Department.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Department.Location = new System.Drawing.Point(3, 12);
            this.btn_Department.Name = "btn_Department";
            this.btn_Department.Size = new System.Drawing.Size(155, 57);
            this.btn_Department.TabIndex = 0;
            this.btn_Department.Text = "Department Management";
            this.btn_Department.UseVisualStyleBackColor = false;
            this.btn_Department.Click += new System.EventHandler(this.btn_Department_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 662);
            this.Controls.Add(this.splitContainer1);
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(990, 700);
            this.MinimumSize = new System.Drawing.Size(990, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Year End Party - Administration";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Department;
        private System.Windows.Forms.Button btn_Winner;
        private System.Windows.Forms.Button btn_Staff;
        private System.Windows.Forms.Button btn_PrizeItem;
        private System.Windows.Forms.Button btn_PrizeType;
        private System.Windows.Forms.Button btn_ExitMain;
    }
}

