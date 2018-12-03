namespace HandwritingDigitRecognition
{
    partial class KmeansTrainedDataView
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.Trained_Picturebox = new System.Windows.Forms.PictureBox();
            this.Distance_Label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Tag_Label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trained_Picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Trained_Picturebox);
            this.panel4.Controls.Add(this.Distance_Label);
            this.panel4.Controls.Add(this.Tag_Label);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(250, 136);
            this.panel4.TabIndex = 13;
            // 
            // Trained_Picturebox
            // 
            this.Trained_Picturebox.BackColor = System.Drawing.Color.White;
            this.Trained_Picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Trained_Picturebox.Location = new System.Drawing.Point(13, 10);
            this.Trained_Picturebox.Name = "Trained_Picturebox";
            this.Trained_Picturebox.Size = new System.Drawing.Size(112, 112);
            this.Trained_Picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Trained_Picturebox.TabIndex = 6;
            this.Trained_Picturebox.TabStop = false;
            // 
            // Distance_Label
            // 
            this.Distance_Label.AutoSize = true;
            this.Distance_Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Distance_Label.Location = new System.Drawing.Point(127, 85);
            this.Distance_Label.Name = "Distance_Label";
            this.Distance_Label.Size = new System.Drawing.Size(113, 27);
            this.Distance_Label.TabIndex = 11;
            this.Distance_Label.Text = "0.1234567";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(126, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tag :";
            // 
            // Tag_Label
            // 
            this.Tag_Label.AutoSize = true;
            this.Tag_Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Tag_Label.Location = new System.Drawing.Point(180, 20);
            this.Tag_Label.Name = "Tag_Label";
            this.Tag_Label.Size = new System.Drawing.Size(24, 27);
            this.Tag_Label.TabIndex = 10;
            this.Tag_Label.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(126, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 26);
            this.label6.TabIndex = 9;
            this.label6.Text = "Distance :";
            // 
            // KmeansTrainedDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Name = "KmeansTrainedDataView";
            this.Size = new System.Drawing.Size(250, 136);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trained_Picturebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox Trained_Picturebox;
        private System.Windows.Forms.Label Distance_Label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Tag_Label;
        private System.Windows.Forms.Label label6;
    }
}
