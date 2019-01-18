namespace ML_ObjectDetection
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
            this.Webcam_Picturebox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Snap_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Conn_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SamplePicturebox = new System.Windows.Forms.PictureBox();
            this.LoadImg_btn = new System.Windows.Forms.Button();
            this.Recognize_btn = new System.Windows.Forms.Button();
            this.RecongnizePath_btn = new System.Windows.Forms.Button();
            this.FolderPath_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Connstr_ComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Webcam_Picturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplePicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // Webcam_Picturebox
            // 
            this.Webcam_Picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Webcam_Picturebox.Location = new System.Drawing.Point(12, 52);
            this.Webcam_Picturebox.Name = "Webcam_Picturebox";
            this.Webcam_Picturebox.Size = new System.Drawing.Size(640, 480);
            this.Webcam_Picturebox.TabIndex = 0;
            this.Webcam_Picturebox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(250, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera";
            // 
            // Snap_btn
            // 
            this.Snap_btn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Snap_btn.Location = new System.Drawing.Point(571, 538);
            this.Snap_btn.Name = "Snap_btn";
            this.Snap_btn.Size = new System.Drawing.Size(81, 29);
            this.Snap_btn.TabIndex = 2;
            this.Snap_btn.Text = "Snap";
            this.Snap_btn.UseVisualStyleBackColor = true;
            this.Snap_btn.Click += new System.EventHandler(this.Snap_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 542);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connstr :";
            // 
            // Conn_btn
            // 
            this.Conn_btn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Conn_btn.Location = new System.Drawing.Point(484, 538);
            this.Conn_btn.Name = "Conn_btn";
            this.Conn_btn.Size = new System.Drawing.Size(81, 29);
            this.Conn_btn.TabIndex = 5;
            this.Conn_btn.Text = "Conn";
            this.Conn_btn.UseVisualStyleBackColor = true;
            this.Conn_btn.Click += new System.EventHandler(this.Conn_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(896, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 40);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sample";
            // 
            // SamplePicturebox
            // 
            this.SamplePicturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SamplePicturebox.Location = new System.Drawing.Point(658, 52);
            this.SamplePicturebox.Name = "SamplePicturebox";
            this.SamplePicturebox.Size = new System.Drawing.Size(640, 480);
            this.SamplePicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SamplePicturebox.TabIndex = 6;
            this.SamplePicturebox.TabStop = false;
            // 
            // LoadImg_btn
            // 
            this.LoadImg_btn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LoadImg_btn.Location = new System.Drawing.Point(1105, 538);
            this.LoadImg_btn.Name = "LoadImg_btn";
            this.LoadImg_btn.Size = new System.Drawing.Size(87, 29);
            this.LoadImg_btn.TabIndex = 9;
            this.LoadImg_btn.Text = "LoadImg";
            this.LoadImg_btn.UseVisualStyleBackColor = true;
            this.LoadImg_btn.Click += new System.EventHandler(this.LoadImg_btn_Click);
            // 
            // Recognize_btn
            // 
            this.Recognize_btn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Recognize_btn.Location = new System.Drawing.Point(1198, 538);
            this.Recognize_btn.Name = "Recognize_btn";
            this.Recognize_btn.Size = new System.Drawing.Size(100, 29);
            this.Recognize_btn.TabIndex = 8;
            this.Recognize_btn.Text = "Recognize";
            this.Recognize_btn.UseVisualStyleBackColor = true;
            this.Recognize_btn.Click += new System.EventHandler(this.Recognize_btn_Click);
            // 
            // RecongnizePath_btn
            // 
            this.RecongnizePath_btn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RecongnizePath_btn.Location = new System.Drawing.Point(1006, 538);
            this.RecongnizePath_btn.Name = "RecongnizePath_btn";
            this.RecongnizePath_btn.Size = new System.Drawing.Size(93, 29);
            this.RecongnizePath_btn.TabIndex = 10;
            this.RecongnizePath_btn.Text = "RPath";
            this.RecongnizePath_btn.UseVisualStyleBackColor = true;
            this.RecongnizePath_btn.Click += new System.EventHandler(this.LoadPath_btn_Click);
            // 
            // FolderPath_TextBox
            // 
            this.FolderPath_TextBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FolderPath_TextBox.Location = new System.Drawing.Point(716, 538);
            this.FolderPath_TextBox.Name = "FolderPath_TextBox";
            this.FolderPath_TextBox.Size = new System.Drawing.Size(284, 29);
            this.FolderPath_TextBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(661, 543);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Path :";
            // 
            // Connstr_ComboBox
            // 
            this.Connstr_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Connstr_ComboBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Connstr_ComboBox.FormattingEnabled = true;
            this.Connstr_ComboBox.Location = new System.Drawing.Point(95, 539);
            this.Connstr_ComboBox.Name = "Connstr_ComboBox";
            this.Connstr_ComboBox.Size = new System.Drawing.Size(383, 28);
            this.Connstr_ComboBox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 581);
            this.Controls.Add(this.Connstr_ComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FolderPath_TextBox);
            this.Controls.Add(this.RecongnizePath_btn);
            this.Controls.Add(this.LoadImg_btn);
            this.Controls.Add(this.Recognize_btn);
            this.Controls.Add(this.SamplePicturebox);
            this.Controls.Add(this.Conn_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Snap_btn);
            this.Controls.Add(this.Webcam_Picturebox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Webcam_Picturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplePicturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Webcam_Picturebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Snap_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Conn_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox SamplePicturebox;
        private System.Windows.Forms.Button LoadImg_btn;
        private System.Windows.Forms.Button Recognize_btn;
        private System.Windows.Forms.Button RecongnizePath_btn;
        private System.Windows.Forms.TextBox FolderPath_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Connstr_ComboBox;
    }
}