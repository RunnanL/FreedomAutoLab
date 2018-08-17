namespace WindowsFormsApp1
{
    partial class GantryCalibration
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
            this.ManualCmdInput_tb = new System.Windows.Forms.TextBox();
            this.ManualCmd_gb = new System.Windows.Forms.GroupBox();
            this.ManualCmdSend_btn = new System.Windows.Forms.Button();
            this.ConsoleDialogue_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CalibrationMode_gb = new System.Windows.Forms.GroupBox();
            this.CalibrateHere_gb = new System.Windows.Forms.GroupBox();
            this.CaliZ_tb = new System.Windows.Forms.TextBox();
            this.CaliY_tb = new System.Windows.Forms.TextBox();
            this.CalibrateHere = new System.Windows.Forms.Button();
            this.CaliX_tb = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.DirectMove_gb = new System.Windows.Forms.GroupBox();
            this.stop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MovPlusY_btn = new System.Windows.Forms.Button();
            this.MovMinusZ_tb = new System.Windows.Forms.TextBox();
            this.MovPlusX_btn = new System.Windows.Forms.Button();
            this.MovPlusZ_tb = new System.Windows.Forms.TextBox();
            this.MovMinusY_btn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.MovMinusX_btn = new System.Windows.Forms.Button();
            this.MovMinusZ_btn = new System.Windows.Forms.Button();
            this.MovPlusX_tb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.MovPlusZ_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.MovMinusY_tb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MovPlusY_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MovMinusX_tb = new System.Windows.Forms.TextBox();
            this.En_btn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SendCmd_btn = new System.Windows.Forms.Button();
            this.GantryCmd_tb = new System.Windows.Forms.TextBox();
            this.ManualCmd_gb.SuspendLayout();
            this.CalibrationMode_gb.SuspendLayout();
            this.CalibrateHere_gb.SuspendLayout();
            this.DirectMove_gb.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ManualCmdInput_tb
            // 
            this.ManualCmdInput_tb.Location = new System.Drawing.Point(6, 19);
            this.ManualCmdInput_tb.Name = "ManualCmdInput_tb";
            this.ManualCmdInput_tb.Size = new System.Drawing.Size(204, 20);
            this.ManualCmdInput_tb.TabIndex = 0;
            // 
            // ManualCmd_gb
            // 
            this.ManualCmd_gb.Controls.Add(this.ManualCmdSend_btn);
            this.ManualCmd_gb.Controls.Add(this.ManualCmdInput_tb);
            this.ManualCmd_gb.Location = new System.Drawing.Point(12, 429);
            this.ManualCmd_gb.Name = "ManualCmd_gb";
            this.ManualCmd_gb.Size = new System.Drawing.Size(297, 50);
            this.ManualCmd_gb.TabIndex = 1;
            this.ManualCmd_gb.TabStop = false;
            this.ManualCmd_gb.Text = "Manual Command";
            // 
            // ManualCmdSend_btn
            // 
            this.ManualCmdSend_btn.Location = new System.Drawing.Point(216, 17);
            this.ManualCmdSend_btn.Name = "ManualCmdSend_btn";
            this.ManualCmdSend_btn.Size = new System.Drawing.Size(75, 23);
            this.ManualCmdSend_btn.TabIndex = 2;
            this.ManualCmdSend_btn.Text = "Send";
            this.ManualCmdSend_btn.UseVisualStyleBackColor = true;
            // 
            // ConsoleDialogue_TextBox
            // 
            this.ConsoleDialogue_TextBox.Location = new System.Drawing.Point(12, 25);
            this.ConsoleDialogue_TextBox.Multiline = true;
            this.ConsoleDialogue_TextBox.Name = "ConsoleDialogue_TextBox";
            this.ConsoleDialogue_TextBox.ReadOnly = true;
            this.ConsoleDialogue_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleDialogue_TextBox.Size = new System.Drawing.Size(571, 111);
            this.ConsoleDialogue_TextBox.TabIndex = 3;
            this.ConsoleDialogue_TextBox.Text = "* Please connect Gantry before making changes *\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Gantry Dialogue";
            // 
            // CalibrationMode_gb
            // 
            this.CalibrationMode_gb.Controls.Add(this.CalibrateHere_gb);
            this.CalibrationMode_gb.Controls.Add(this.DirectMove_gb);
            this.CalibrationMode_gb.Controls.Add(this.En_btn);
            this.CalibrationMode_gb.Location = new System.Drawing.Point(12, 143);
            this.CalibrationMode_gb.Name = "CalibrationMode_gb";
            this.CalibrationMode_gb.Size = new System.Drawing.Size(571, 336);
            this.CalibrationMode_gb.TabIndex = 5;
            this.CalibrationMode_gb.TabStop = false;
            this.CalibrationMode_gb.Text = "Calibration Mode";
            // 
            // CalibrateHere_gb
            // 
            this.CalibrateHere_gb.Controls.Add(this.CaliZ_tb);
            this.CalibrateHere_gb.Controls.Add(this.CaliY_tb);
            this.CalibrateHere_gb.Controls.Add(this.CalibrateHere);
            this.CalibrateHere_gb.Controls.Add(this.CaliX_tb);
            this.CalibrateHere_gb.Controls.Add(this.label16);
            this.CalibrateHere_gb.Controls.Add(this.label15);
            this.CalibrateHere_gb.Controls.Add(this.label14);
            this.CalibrateHere_gb.Enabled = false;
            this.CalibrateHere_gb.Location = new System.Drawing.Point(8, 247);
            this.CalibrateHere_gb.Name = "CalibrateHere_gb";
            this.CalibrateHere_gb.Size = new System.Drawing.Size(551, 78);
            this.CalibrateHere_gb.TabIndex = 22;
            this.CalibrateHere_gb.TabStop = false;
            this.CalibrateHere_gb.Text = "Calibration";
            // 
            // CaliZ_tb
            // 
            this.CaliZ_tb.Location = new System.Drawing.Point(200, 39);
            this.CaliZ_tb.Name = "CaliZ_tb";
            this.CaliZ_tb.Size = new System.Drawing.Size(75, 20);
            this.CaliZ_tb.TabIndex = 16;
            // 
            // CaliY_tb
            // 
            this.CaliY_tb.Location = new System.Drawing.Point(107, 39);
            this.CaliY_tb.Name = "CaliY_tb";
            this.CaliY_tb.Size = new System.Drawing.Size(75, 20);
            this.CaliY_tb.TabIndex = 15;
            // 
            // CalibrateHere
            // 
            this.CalibrateHere.Location = new System.Drawing.Point(295, 37);
            this.CalibrateHere.Name = "CalibrateHere";
            this.CalibrateHere.Size = new System.Drawing.Size(84, 23);
            this.CalibrateHere.TabIndex = 17;
            this.CalibrateHere.Text = "Calibrate Here";
            this.CalibrateHere.UseVisualStyleBackColor = true;
            this.CalibrateHere.Click += new System.EventHandler(this.CalibrateHere_Click);
            // 
            // CaliX_tb
            // 
            this.CaliX_tb.Location = new System.Drawing.Point(17, 39);
            this.CaliX_tb.Name = "CaliX_tb";
            this.CaliX_tb.Size = new System.Drawing.Size(75, 20);
            this.CaliX_tb.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(218, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "Z (mm)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(125, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Y (mm)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "X (mm)";
            // 
            // DirectMove_gb
            // 
            this.DirectMove_gb.Controls.Add(this.stop);
            this.DirectMove_gb.Controls.Add(this.label2);
            this.DirectMove_gb.Controls.Add(this.MovPlusY_btn);
            this.DirectMove_gb.Controls.Add(this.MovMinusZ_tb);
            this.DirectMove_gb.Controls.Add(this.MovPlusX_btn);
            this.DirectMove_gb.Controls.Add(this.MovPlusZ_tb);
            this.DirectMove_gb.Controls.Add(this.MovMinusY_btn);
            this.DirectMove_gb.Controls.Add(this.label11);
            this.DirectMove_gb.Controls.Add(this.MovMinusX_btn);
            this.DirectMove_gb.Controls.Add(this.MovMinusZ_btn);
            this.DirectMove_gb.Controls.Add(this.MovPlusX_tb);
            this.DirectMove_gb.Controls.Add(this.label10);
            this.DirectMove_gb.Controls.Add(this.label12);
            this.DirectMove_gb.Controls.Add(this.MovPlusZ_btn);
            this.DirectMove_gb.Controls.Add(this.label13);
            this.DirectMove_gb.Controls.Add(this.label9);
            this.DirectMove_gb.Controls.Add(this.MovMinusY_tb);
            this.DirectMove_gb.Controls.Add(this.label8);
            this.DirectMove_gb.Controls.Add(this.label3);
            this.DirectMove_gb.Controls.Add(this.label7);
            this.DirectMove_gb.Controls.Add(this.MovPlusY_tb);
            this.DirectMove_gb.Controls.Add(this.label6);
            this.DirectMove_gb.Controls.Add(this.label4);
            this.DirectMove_gb.Controls.Add(this.label5);
            this.DirectMove_gb.Controls.Add(this.MovMinusX_tb);
            this.DirectMove_gb.Enabled = false;
            this.DirectMove_gb.Location = new System.Drawing.Point(8, 48);
            this.DirectMove_gb.Name = "DirectMove_gb";
            this.DirectMove_gb.Size = new System.Drawing.Size(551, 192);
            this.DirectMove_gb.TabIndex = 21;
            this.DirectMove_gb.TabStop = false;
            this.DirectMove_gb.Text = "Direct Movement";
            // 
            // stop
            // 
            this.stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop.ForeColor = System.Drawing.Color.Red;
            this.stop.Location = new System.Drawing.Point(483, 140);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(62, 46);
            this.stop.TabIndex = 13;
            this.stop.Text = "STOP";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "mm";
            // 
            // MovPlusY_btn
            // 
            this.MovPlusY_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovPlusY_btn.Location = new System.Drawing.Point(51, 84);
            this.MovPlusY_btn.Name = "MovPlusY_btn";
            this.MovPlusY_btn.Size = new System.Drawing.Size(30, 30);
            this.MovPlusY_btn.TabIndex = 6;
            this.MovPlusY_btn.Text = "←";
            this.MovPlusY_btn.UseVisualStyleBackColor = true;
            this.MovPlusY_btn.Click += new System.EventHandler(this.MovPlusY_btn_Click);
            // 
            // MovMinusZ_tb
            // 
            this.MovMinusZ_tb.Location = new System.Drawing.Point(450, 79);
            this.MovMinusZ_tb.Name = "MovMinusZ_tb";
            this.MovMinusZ_tb.Size = new System.Drawing.Size(48, 20);
            this.MovMinusZ_tb.TabIndex = 11;
            this.MovMinusZ_tb.Text = "000";
            this.MovMinusZ_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MovPlusX_btn
            // 
            this.MovPlusX_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovPlusX_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MovPlusX_btn.Location = new System.Drawing.Point(152, 30);
            this.MovPlusX_btn.Name = "MovPlusX_btn";
            this.MovPlusX_btn.Size = new System.Drawing.Size(30, 30);
            this.MovPlusX_btn.TabIndex = 2;
            this.MovPlusX_btn.Text = "↑";
            this.MovPlusX_btn.UseVisualStyleBackColor = true;
            this.MovPlusX_btn.Click += new System.EventHandler(this.MovPlusX_btn_Click);
            // 
            // MovPlusZ_tb
            // 
            this.MovPlusZ_tb.Location = new System.Drawing.Point(450, 25);
            this.MovPlusZ_tb.Name = "MovPlusZ_tb";
            this.MovPlusZ_tb.Size = new System.Drawing.Size(48, 20);
            this.MovPlusZ_tb.TabIndex = 9;
            this.MovPlusZ_tb.Text = "000";
            this.MovPlusZ_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MovMinusY_btn
            // 
            this.MovMinusY_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovMinusY_btn.Location = new System.Drawing.Point(283, 84);
            this.MovMinusY_btn.Name = "MovMinusY_btn";
            this.MovMinusY_btn.Size = new System.Drawing.Size(30, 30);
            this.MovMinusY_btn.TabIndex = 8;
            this.MovMinusY_btn.Text = "→";
            this.MovMinusY_btn.UseVisualStyleBackColor = true;
            this.MovMinusY_btn.Click += new System.EventHandler(this.MovMinusY_btn_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(368, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Z down";
            // 
            // MovMinusX_btn
            // 
            this.MovMinusX_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovMinusX_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MovMinusX_btn.Location = new System.Drawing.Point(152, 142);
            this.MovMinusX_btn.Name = "MovMinusX_btn";
            this.MovMinusX_btn.Size = new System.Drawing.Size(30, 30);
            this.MovMinusX_btn.TabIndex = 4;
            this.MovMinusX_btn.Text = "↓";
            this.MovMinusX_btn.UseVisualStyleBackColor = true;
            this.MovMinusX_btn.Click += new System.EventHandler(this.MovMinusX_btn_Click);
            // 
            // MovMinusZ_btn
            // 
            this.MovMinusZ_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovMinusZ_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MovMinusZ_btn.Location = new System.Drawing.Point(414, 73);
            this.MovMinusZ_btn.Name = "MovMinusZ_btn";
            this.MovMinusZ_btn.Size = new System.Drawing.Size(30, 30);
            this.MovMinusZ_btn.TabIndex = 12;
            this.MovMinusZ_btn.Text = "↓";
            this.MovMinusZ_btn.UseVisualStyleBackColor = true;
            this.MovMinusZ_btn.Click += new System.EventHandler(this.MovMinusZ_btn_Click);
            // 
            // MovPlusX_tb
            // 
            this.MovPlusX_tb.Location = new System.Drawing.Point(144, 66);
            this.MovPlusX_tb.Name = "MovPlusX_tb";
            this.MovPlusX_tb.Size = new System.Drawing.Size(48, 20);
            this.MovPlusX_tb.TabIndex = 1;
            this.MovPlusX_tb.Text = "000";
            this.MovPlusX_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(379, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Z up";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(504, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "mm";
            // 
            // MovPlusZ_btn
            // 
            this.MovPlusZ_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovPlusZ_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MovPlusZ_btn.Location = new System.Drawing.Point(414, 19);
            this.MovPlusZ_btn.Name = "MovPlusZ_btn";
            this.MovPlusZ_btn.Size = new System.Drawing.Size(30, 30);
            this.MovPlusZ_btn.TabIndex = 10;
            this.MovPlusZ_btn.Text = "↑";
            this.MovPlusZ_btn.UseVisualStyleBackColor = true;
            this.MovPlusZ_btn.Click += new System.EventHandler(this.MovPlusZ_btn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(504, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "mm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(157, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "-X";
            // 
            // MovMinusY_tb
            // 
            this.MovMinusY_tb.Location = new System.Drawing.Point(200, 90);
            this.MovMinusY_tb.Name = "MovMinusY_tb";
            this.MovMinusY_tb.Size = new System.Drawing.Size(48, 20);
            this.MovMinusY_tb.TabIndex = 7;
            this.MovMinusY_tb.Text = "000";
            this.MovMinusY_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(319, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "-Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "+Y";
            // 
            // MovPlusY_tb
            // 
            this.MovPlusY_tb.Location = new System.Drawing.Point(87, 90);
            this.MovPlusY_tb.Name = "MovPlusY_tb";
            this.MovPlusY_tb.Size = new System.Drawing.Size(48, 20);
            this.MovPlusY_tb.TabIndex = 5;
            this.MovPlusY_tb.Text = "000";
            this.MovPlusY_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(158, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "+X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "mm";
            // 
            // MovMinusX_tb
            // 
            this.MovMinusX_tb.Location = new System.Drawing.Point(144, 116);
            this.MovMinusX_tb.Name = "MovMinusX_tb";
            this.MovMinusX_tb.Size = new System.Drawing.Size(48, 20);
            this.MovMinusX_tb.TabIndex = 3;
            this.MovMinusX_tb.Text = "000";
            this.MovMinusX_tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // En_btn
            // 
            this.En_btn.Location = new System.Drawing.Point(8, 19);
            this.En_btn.Name = "En_btn";
            this.En_btn.Size = new System.Drawing.Size(75, 23);
            this.En_btn.TabIndex = 20;
            this.En_btn.Text = "Enable";
            this.En_btn.UseVisualStyleBackColor = true;
            this.En_btn.Click += new System.EventHandler(this.En_btn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SendCmd_btn);
            this.groupBox3.Controls.Add(this.GantryCmd_tb);
            this.groupBox3.Location = new System.Drawing.Point(15, 485);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 75);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Manual Command";
            // 
            // SendCmd_btn
            // 
            this.SendCmd_btn.Location = new System.Drawing.Point(300, 34);
            this.SendCmd_btn.Name = "SendCmd_btn";
            this.SendCmd_btn.Size = new System.Drawing.Size(84, 23);
            this.SendCmd_btn.TabIndex = 19;
            this.SendCmd_btn.Text = "Send";
            this.SendCmd_btn.UseVisualStyleBackColor = true;
            this.SendCmd_btn.Click += new System.EventHandler(this.SendCmd_btn_Click);
            // 
            // GantryCmd_tb
            // 
            this.GantryCmd_tb.Location = new System.Drawing.Point(22, 36);
            this.GantryCmd_tb.Name = "GantryCmd_tb";
            this.GantryCmd_tb.Size = new System.Drawing.Size(260, 20);
            this.GantryCmd_tb.TabIndex = 18;
            // 
            // GantryCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 567);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CalibrationMode_gb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsoleDialogue_TextBox);
            this.Controls.Add(this.ManualCmd_gb);
            this.Name = "GantryCalibration";
            this.Text = "Gantry Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GantryCalibration_FormClosing);
            this.ManualCmd_gb.ResumeLayout(false);
            this.ManualCmd_gb.PerformLayout();
            this.CalibrationMode_gb.ResumeLayout(false);
            this.CalibrateHere_gb.ResumeLayout(false);
            this.CalibrateHere_gb.PerformLayout();
            this.DirectMove_gb.ResumeLayout(false);
            this.DirectMove_gb.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ManualCmdInput_tb;
        private System.Windows.Forms.GroupBox ManualCmd_gb;
        private System.Windows.Forms.Button ManualCmdSend_btn;
        private System.Windows.Forms.TextBox ConsoleDialogue_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox CalibrationMode_gb;
        private System.Windows.Forms.Button MovPlusY_btn;
        private System.Windows.Forms.Button En_btn;
        private System.Windows.Forms.TextBox MovPlusX_tb;
        private System.Windows.Forms.Button MovMinusX_btn;
        private System.Windows.Forms.Button MovMinusY_btn;
        private System.Windows.Forms.Button MovPlusX_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MovMinusX_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MovPlusY_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MovMinusY_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MovPlusZ_tb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button MovMinusZ_btn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button MovPlusZ_btn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox DirectMove_gb;
        private System.Windows.Forms.TextBox MovMinusZ_tb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox CalibrateHere_gb;
        private System.Windows.Forms.TextBox CaliZ_tb;
        private System.Windows.Forms.TextBox CaliY_tb;
        private System.Windows.Forms.Button CalibrateHere;
        private System.Windows.Forms.TextBox CaliX_tb;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SendCmd_btn;
        private System.Windows.Forms.TextBox GantryCmd_tb;
        private System.Windows.Forms.Button stop;
    }
}