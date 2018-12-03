namespace HandwritingDigitRecognition
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
            this.InputPictureBox = new System.Windows.Forms.PictureBox();
            this.Clear_btn = new System.Windows.Forms.Button();
            this.Classify_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.KnnResultPictureBox = new System.Windows.Forms.PictureBox();
            this.KnnResultNum_Label = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.KmeansResultPictureBox = new System.Windows.Forms.PictureBox();
            this.KmeansResultNum_Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.ClosestKCount_RichTextbox = new System.Windows.Forms.RichTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.KmeansTrainedData_flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Load_KemansTrainedData_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Knn_KValue_Textbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.InputPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KnnResultPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KmeansResultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InputPictureBox
            // 
            this.InputPictureBox.BackColor = System.Drawing.Color.White;
            this.InputPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputPictureBox.Location = new System.Drawing.Point(25, 55);
            this.InputPictureBox.Name = "InputPictureBox";
            this.InputPictureBox.Size = new System.Drawing.Size(112, 112);
            this.InputPictureBox.TabIndex = 0;
            this.InputPictureBox.TabStop = false;
            this.InputPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InuptPictureBox_MouseDown);
            this.InputPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InuptPictureBox_MouseMove);
            this.InputPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InuptPictureBox_MouseUp);
            // 
            // Clear_btn
            // 
            this.Clear_btn.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Clear_btn.Location = new System.Drawing.Point(143, 55);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(89, 39);
            this.Clear_btn.TabIndex = 1;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // Classify_btn
            // 
            this.Classify_btn.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Classify_btn.Location = new System.Drawing.Point(143, 128);
            this.Classify_btn.Name = "Classify_btn";
            this.Classify_btn.Size = new System.Drawing.Size(89, 39);
            this.Classify_btn.TabIndex = 2;
            this.Classify_btn.Text = "Classify";
            this.Classify_btn.UseVisualStyleBackColor = true;
            this.Classify_btn.Click += new System.EventHandler(this.Classify_btn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.InputPictureBox);
            this.panel1.Controls.Add(this.Classify_btn);
            this.panel1.Controls.Add(this.Clear_btn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 188);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Knn_KValue_Textbox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.ClosestKCount_RichTextbox);
            this.panel2.Controls.Add(this.KnnResultPictureBox);
            this.panel2.Controls.Add(this.KnnResultNum_Label);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label33);
            this.panel2.Location = new System.Drawing.Point(12, 416);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 371);
            this.panel2.TabIndex = 6;
            // 
            // KnnResultPictureBox
            // 
            this.KnnResultPictureBox.BackColor = System.Drawing.Color.White;
            this.KnnResultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KnnResultPictureBox.Location = new System.Drawing.Point(16, 87);
            this.KnnResultPictureBox.Name = "KnnResultPictureBox";
            this.KnnResultPictureBox.Size = new System.Drawing.Size(112, 112);
            this.KnnResultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.KnnResultPictureBox.TabIndex = 0;
            this.KnnResultPictureBox.TabStop = false;
            // 
            // KnnResultNum_Label
            // 
            this.KnnResultNum_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KnnResultNum_Label.Font = new System.Drawing.Font("微軟正黑體", 63.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.KnnResultNum_Label.Location = new System.Drawing.Point(133, 87);
            this.KnnResultNum_Label.Name = "KnnResultNum_Label";
            this.KnnResultNum_Label.Size = new System.Drawing.Size(112, 112);
            this.KnnResultNum_Label.TabIndex = 5;
            this.KnnResultNum_Label.Text = "0";
            this.KnnResultNum_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.Load_KemansTrainedData_btn);
            this.panel3.Controls.Add(this.KmeansTrainedData_flowPanel);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(280, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(553, 775);
            this.panel3.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(107, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(338, 40);
            this.label3.TabIndex = 7;
            this.label3.Text = "Kmeans Trained Data";
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.KmeansResultPictureBox);
            this.panel10.Controls.Add(this.KmeansResultNum_Label);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Controls.Add(this.label30);
            this.panel10.Controls.Add(this.label29);
            this.panel10.Location = new System.Drawing.Point(12, 206);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(262, 204);
            this.panel10.TabIndex = 7;
            // 
            // KmeansResultPictureBox
            // 
            this.KmeansResultPictureBox.BackColor = System.Drawing.Color.White;
            this.KmeansResultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KmeansResultPictureBox.Location = new System.Drawing.Point(16, 80);
            this.KmeansResultPictureBox.Name = "KmeansResultPictureBox";
            this.KmeansResultPictureBox.Size = new System.Drawing.Size(112, 112);
            this.KmeansResultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.KmeansResultPictureBox.TabIndex = 6;
            this.KmeansResultPictureBox.TabStop = false;
            // 
            // KmeansResultNum_Label
            // 
            this.KmeansResultNum_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KmeansResultNum_Label.Font = new System.Drawing.Font("微軟正黑體", 63.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.KmeansResultNum_Label.Location = new System.Drawing.Point(134, 80);
            this.KmeansResultNum_Label.Name = "KmeansResultNum_Label";
            this.KmeansResultNum_Label.Size = new System.Drawing.Size(112, 112);
            this.KmeansResultNum_Label.TabIndex = 8;
            this.KmeansResultNum_Label.Text = "0";
            this.KmeansResultNum_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(87, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "K-nn";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label29.Location = new System.Drawing.Point(148, 48);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(83, 30);
            this.label29.TabIndex = 9;
            this.label29.Text = "Result";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(87, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label30.Location = new System.Drawing.Point(62, 6);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(137, 40);
            this.label30.TabIndex = 10;
            this.label30.Text = "Kmenas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(26, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "Closest";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label31.Location = new System.Drawing.Point(146, 55);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 30);
            this.label31.TabIndex = 12;
            this.label31.Text = "Result";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label32.Location = new System.Drawing.Point(25, 54);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(95, 30);
            this.label32.TabIndex = 12;
            this.label32.Text = "Closest";
            // 
            // ClosestKCount_RichTextbox
            // 
            this.ClosestKCount_RichTextbox.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ClosestKCount_RichTextbox.Location = new System.Drawing.Point(16, 277);
            this.ClosestKCount_RichTextbox.Name = "ClosestKCount_RichTextbox";
            this.ClosestKCount_RichTextbox.ReadOnly = true;
            this.ClosestKCount_RichTextbox.Size = new System.Drawing.Size(230, 78);
            this.ClosestKCount_RichTextbox.TabIndex = 13;
            this.ClosestKCount_RichTextbox.Text = "";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label33.Location = new System.Drawing.Point(35, 244);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(192, 30);
            this.label33.TabIndex = 14;
            this.label33.Text = "Closest K Count";
            // 
            // KmeansTrainedData_flowPanel
            // 
            this.KmeansTrainedData_flowPanel.AutoScroll = true;
            this.KmeansTrainedData_flowPanel.Location = new System.Drawing.Point(20, 52);
            this.KmeansTrainedData_flowPanel.Name = "KmeansTrainedData_flowPanel";
            this.KmeansTrainedData_flowPanel.Size = new System.Drawing.Size(519, 710);
            this.KmeansTrainedData_flowPanel.TabIndex = 8;
            // 
            // Load_KemansTrainedData_btn
            // 
            this.Load_KemansTrainedData_btn.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Load_KemansTrainedData_btn.Location = new System.Drawing.Point(464, 21);
            this.Load_KemansTrainedData_btn.Name = "Load_KemansTrainedData_btn";
            this.Load_KemansTrainedData_btn.Size = new System.Drawing.Size(75, 28);
            this.Load_KemansTrainedData_btn.TabIndex = 9;
            this.Load_KemansTrainedData_btn.Text = "Load";
            this.Load_KemansTrainedData_btn.UseVisualStyleBackColor = true;
            this.Load_KemansTrainedData_btn.Click += new System.EventHandler(this.Load_KemansTrainedData_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(22, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 30);
            this.label5.TabIndex = 15;
            this.label5.Text = "K Value:";
            // 
            // Knn_KValue_Textbox
            // 
            this.Knn_KValue_Textbox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Knn_KValue_Textbox.Location = new System.Drawing.Point(134, 207);
            this.Knn_KValue_Textbox.Name = "Knn_KValue_Textbox";
            this.Knn_KValue_Textbox.Size = new System.Drawing.Size(111, 29);
            this.Knn_KValue_Textbox.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 799);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InputPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KnnResultPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KmeansResultPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox InputPictureBox;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.Button Classify_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox KnnResultPictureBox;
        private System.Windows.Forms.Label KnnResultNum_Label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.PictureBox KmeansResultPictureBox;
        private System.Windows.Forms.Label KmeansResultNum_Label;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.RichTextBox ClosestKCount_RichTextbox;
        private System.Windows.Forms.FlowLayoutPanel KmeansTrainedData_flowPanel;
        private System.Windows.Forms.Button Load_KemansTrainedData_btn;
        private System.Windows.Forms.TextBox Knn_KValue_Textbox;
        private System.Windows.Forms.Label label5;
    }
}