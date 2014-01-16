namespace YearEndParty.Presentation
{
    partial class XAdhocPrize
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
            this.labelDept = new System.Windows.Forms.Label();
            this.labelNick = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.pictureBoxWinner = new System.Windows.Forms.PictureBox();
            this.AdHocList = new System.Windows.Forms.ComboBox();
            this.lableStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWinner)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDept
            // 
            this.labelDept.BackColor = System.Drawing.Color.Transparent;
            this.labelDept.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDept.ForeColor = System.Drawing.Color.White;
            this.labelDept.Location = new System.Drawing.Point(171, 690);
            this.labelDept.Name = "labelDept";
            this.labelDept.Size = new System.Drawing.Size(240, 28);
            this.labelDept.TabIndex = 21;
            // 
            // labelNick
            // 
            this.labelNick.BackColor = System.Drawing.Color.Transparent;
            this.labelNick.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNick.ForeColor = System.Drawing.Color.White;
            this.labelNick.Location = new System.Drawing.Point(171, 631);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(240, 27);
            this.labelNick.TabIndex = 20;
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelName.Location = new System.Drawing.Point(168, 540);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(240, 81);
            this.labelName.TabIndex = 19;
            // 
            // pictureBoxWinner
            // 
            this.pictureBoxWinner.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxWinner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxWinner.Location = new System.Drawing.Point(168, 267);
            this.pictureBoxWinner.Name = "pictureBoxWinner";
            this.pictureBoxWinner.Size = new System.Drawing.Size(240, 260);
            this.pictureBoxWinner.TabIndex = 18;
            this.pictureBoxWinner.TabStop = false;
            // 
            // AdHocList
            // 
            this.AdHocList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdHocList.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdHocList.FormattingEnabled = true;
            this.AdHocList.Location = new System.Drawing.Point(637, 231);
            this.AdHocList.Name = "AdHocList";
            this.AdHocList.Size = new System.Drawing.Size(198, 28);
            this.AdHocList.TabIndex = 0;
            this.AdHocList.SelectedIndexChanged += new System.EventHandler(this.AdHocList_SelectedIndexChanged);
            // 
            // lableStatus
            // 
            this.lableStatus.Font = new System.Drawing.Font("XXII ARMY", 37F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableStatus.ForeColor = System.Drawing.Color.White;
            this.lableStatus.Location = new System.Drawing.Point(197, 641);
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(180, 64);
            this.lableStatus.TabIndex = 23;
            this.lableStatus.Text = "00/00";
            this.lableStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // XAdhocPrize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;

            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lableStatus);
            this.Controls.Add(this.AdHocList);
            this.Controls.Add(this.labelDept);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.pictureBoxWinner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "XAdhocPrize";
            this.Text = "AdhocPrize";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdhocPrize_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AdhocPrize_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDept;
        private System.Windows.Forms.Label labelNick;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PictureBox pictureBoxWinner;
        private System.Windows.Forms.ComboBox AdHocList;
        private System.Windows.Forms.Label lableStatus;
    }
}