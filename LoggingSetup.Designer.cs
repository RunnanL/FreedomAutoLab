namespace WindowsFormsApp1
{
    partial class LoggingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingForm));
            this.Control = new System.Windows.Forms.ListBox();
            this.Script = new System.Windows.Forms.TextBox();
            this.Address = new System.Windows.Forms.ListBox();
            this.Command = new System.Windows.Forms.ListBox();
            this.AddinputMeth = new System.Windows.Forms.Button();
            this.AddinputCtrl = new System.Windows.Forms.Button();
            this.Linetype = new System.Windows.Forms.ListBox();
            this.Start = new System.Windows.Forms.Button();
            this.TestLabel = new System.Windows.Forms.Label();
            this.test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LoadScript = new System.Windows.Forms.Button();
            this.SaveScript = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.View = new System.Windows.Forms.Button();
            this.SaveResult = new System.Windows.Forms.Button();
            this.AddinputAdd = new System.Windows.Forms.Button();
            this.AddinputRead = new System.Windows.Forms.Button();
            this.ExecuteMsg = new System.Windows.Forms.TextBox();
            this.Stop = new System.Windows.Forms.Button();
            this.test2 = new System.Windows.Forms.Button();
            this.StopScripting = new System.Windows.Forms.Button();
            this.LogVerDisp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Control
            // 
            this.Control.FormattingEnabled = true;
            this.Control.HorizontalScrollbar = true;
            this.Control.Items.AddRange(new object[] {
            "X, Y, Z,",
            "Intv,",
            "Freq,"});
            this.Control.Location = new System.Drawing.Point(126, 28);
            this.Control.Name = "Control";
            this.Control.ScrollAlwaysVisible = true;
            this.Control.Size = new System.Drawing.Size(97, 95);
            this.Control.TabIndex = 0;
            // 
            // Script
            // 
            this.Script.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Script.Location = new System.Drawing.Point(229, 28);
            this.Script.Multiline = true;
            this.Script.Name = "Script";
            this.Script.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Script.Size = new System.Drawing.Size(592, 217);
            this.Script.TabIndex = 1;
            this.Script.Text = "$, X, Y, Z, Freq, Intv, ;\r\n#, 20, 20, 175, 85000, 2, ;   |, X, Y, Z, LCR1, LCR2, " +
    "LCR3, LCR4,;\r\n#, 15, 15, 175, 84000, 2, ;   |, X, Y, Z, LCR1, LCR2, LCR3, LCR4,;" +
    "";
            this.Script.WordWrap = false;
            // 
            // Address
            // 
            this.Address.Enabled = false;
            this.Address.FormattingEnabled = true;
            this.Address.HorizontalScrollbar = true;
            this.Address.Items.AddRange(new object[] {
            "COM?",
            "IP:Port"});
            this.Address.Location = new System.Drawing.Point(24, 183);
            this.Address.Name = "Address";
            this.Address.ScrollAlwaysVisible = true;
            this.Address.Size = new System.Drawing.Size(96, 95);
            this.Address.TabIndex = 2;
            // 
            // Command
            // 
            this.Command.FormattingEnabled = true;
            this.Command.HorizontalScrollbar = true;
            this.Command.Items.AddRange(new object[] {
            "X,Y,Z,",
            "LCR1,",
            "LCR2,",
            "LCR3,",
            "LCR4,",
            "Freq,",
            "Regatron_P,",
            "Regatron_V,",
            "Regatron_A,",
            "ZES()"});
            this.Command.Location = new System.Drawing.Point(127, 183);
            this.Command.Name = "Command";
            this.Command.ScrollAlwaysVisible = true;
            this.Command.Size = new System.Drawing.Size(96, 95);
            this.Command.TabIndex = 3;
            // 
            // AddinputMeth
            // 
            this.AddinputMeth.Location = new System.Drawing.Point(24, 129);
            this.AddinputMeth.Name = "AddinputMeth";
            this.AddinputMeth.Size = new System.Drawing.Size(40, 23);
            this.AddinputMeth.TabIndex = 5;
            this.AddinputMeth.Text = ">>";
            this.AddinputMeth.UseVisualStyleBackColor = true;
            this.AddinputMeth.Click += new System.EventHandler(this.AddinputMeth_Click);
            // 
            // AddinputCtrl
            // 
            this.AddinputCtrl.Location = new System.Drawing.Point(126, 129);
            this.AddinputCtrl.Name = "AddinputCtrl";
            this.AddinputCtrl.Size = new System.Drawing.Size(40, 23);
            this.AddinputCtrl.TabIndex = 6;
            this.AddinputCtrl.Text = ">>";
            this.AddinputCtrl.UseVisualStyleBackColor = true;
            this.AddinputCtrl.Click += new System.EventHandler(this.AddinputCtrl_Click);
            // 
            // Linetype
            // 
            this.Linetype.FormattingEnabled = true;
            this.Linetype.HorizontalScrollbar = true;
            this.Linetype.Items.AddRange(new object[] {
            "$, (Control Start)",
            "&, (Address Start)",
            "#, (cmd Start)",
            "|, (Read Start)",
            "; (Line End)"});
            this.Linetype.Location = new System.Drawing.Point(24, 28);
            this.Linetype.Name = "Linetype";
            this.Linetype.ScrollAlwaysVisible = true;
            this.Linetype.Size = new System.Drawing.Size(96, 95);
            this.Linetype.TabIndex = 7;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(170, 546);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(87, 23);
            this.Start.TabIndex = 8;
            this.Start.Text = "Start Scripting";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.Location = new System.Drawing.Point(603, 289);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(53, 13);
            this.TestLabel.TabIndex = 9;
            this.TestLabel.Text = "Test Area";
            this.TestLabel.Visible = false;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(662, 284);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 10;
            this.test.Text = "Test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Visible = false;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Method Identifier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Control Params";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Address Params";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(124, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Read Params";
            // 
            // LoadScript
            // 
            this.LoadScript.Location = new System.Drawing.Point(308, 251);
            this.LoadScript.Name = "LoadScript";
            this.LoadScript.Size = new System.Drawing.Size(75, 23);
            this.LoadScript.TabIndex = 15;
            this.LoadScript.Text = "Load From";
            this.LoadScript.UseVisualStyleBackColor = true;
            this.LoadScript.Click += new System.EventHandler(this.LoadScript_Click);
            // 
            // SaveScript
            // 
            this.SaveScript.Location = new System.Drawing.Point(392, 251);
            this.SaveScript.Name = "SaveScript";
            this.SaveScript.Size = new System.Drawing.Size(75, 23);
            this.SaveScript.TabIndex = 16;
            this.SaveScript.Text = "Save Script";
            this.SaveScript.UseVisualStyleBackColor = true;
            this.SaveScript.Visible = false;
            this.SaveScript.Click += new System.EventHandler(this.SaveScript_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Script";
            // 
            // View
            // 
            this.View.Location = new System.Drawing.Point(287, 546);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(75, 23);
            this.View.TabIndex = 19;
            this.View.Text = "View Result";
            this.View.UseVisualStyleBackColor = true;
            this.View.Click += new System.EventHandler(this.View_Click);
            // 
            // SaveResult
            // 
            this.SaveResult.Location = new System.Drawing.Point(392, 546);
            this.SaveResult.Name = "SaveResult";
            this.SaveResult.Size = new System.Drawing.Size(75, 23);
            this.SaveResult.TabIndex = 20;
            this.SaveResult.Text = "Save Result";
            this.SaveResult.UseVisualStyleBackColor = true;
            this.SaveResult.Click += new System.EventHandler(this.SaveResult_Click);
            // 
            // AddinputAdd
            // 
            this.AddinputAdd.Location = new System.Drawing.Point(24, 284);
            this.AddinputAdd.Name = "AddinputAdd";
            this.AddinputAdd.Size = new System.Drawing.Size(40, 23);
            this.AddinputAdd.TabIndex = 21;
            this.AddinputAdd.Text = ">>";
            this.AddinputAdd.UseVisualStyleBackColor = true;
            // 
            // AddinputRead
            // 
            this.AddinputRead.Location = new System.Drawing.Point(126, 284);
            this.AddinputRead.Name = "AddinputRead";
            this.AddinputRead.Size = new System.Drawing.Size(40, 23);
            this.AddinputRead.TabIndex = 21;
            this.AddinputRead.Text = ">>";
            this.AddinputRead.UseVisualStyleBackColor = true;
            this.AddinputRead.Click += new System.EventHandler(this.AddinputRead_Click);
            // 
            // ExecuteMsg
            // 
            this.ExecuteMsg.Location = new System.Drawing.Point(24, 313);
            this.ExecuteMsg.Multiline = true;
            this.ExecuteMsg.Name = "ExecuteMsg";
            this.ExecuteMsg.ReadOnly = true;
            this.ExecuteMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ExecuteMsg.Size = new System.Drawing.Size(797, 217);
            this.ExecuteMsg.TabIndex = 22;
            // 
            // Stop
            // 
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stop.Location = new System.Drawing.Point(620, 544);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(87, 27);
            this.Stop.TabIndex = 23;
            this.Stop.Text = "Force Stop";
            this.Stop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // test2
            // 
            this.test2.Location = new System.Drawing.Point(743, 284);
            this.test2.Name = "test2";
            this.test2.Size = new System.Drawing.Size(75, 23);
            this.test2.TabIndex = 24;
            this.test2.Text = "Test2";
            this.test2.UseVisualStyleBackColor = true;
            this.test2.Visible = false;
            this.test2.Click += new System.EventHandler(this.test2_Click);
            // 
            // StopScripting
            // 
            this.StopScripting.Image = ((System.Drawing.Image)(resources.GetObject("StopScripting.Image")));
            this.StopScripting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StopScripting.Location = new System.Drawing.Point(528, 539);
            this.StopScripting.Name = "StopScripting";
            this.StopScripting.Size = new System.Drawing.Size(54, 36);
            this.StopScripting.TabIndex = 25;
            this.StopScripting.Text = "Stop";
            this.StopScripting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StopScripting.UseVisualStyleBackColor = true;
            this.StopScripting.Click += new System.EventHandler(this.StopScripting_Click);
            // 
            // LogVerDisp
            // 
            this.LogVerDisp.AutoSize = true;
            this.LogVerDisp.Location = new System.Drawing.Point(21, 551);
            this.LogVerDisp.Name = "LogVerDisp";
            this.LogVerDisp.Size = new System.Drawing.Size(62, 13);
            this.LogVerDisp.TabIndex = 26;
            this.LogVerDisp.Text = "Version No.";
            // 
            // LoggingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 581);
            this.Controls.Add(this.LogVerDisp);
            this.Controls.Add(this.StopScripting);
            this.Controls.Add(this.test2);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.ExecuteMsg);
            this.Controls.Add(this.AddinputRead);
            this.Controls.Add(this.AddinputAdd);
            this.Controls.Add(this.SaveResult);
            this.Controls.Add(this.View);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SaveScript);
            this.Controls.Add(this.LoadScript);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.test);
            this.Controls.Add(this.TestLabel);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Linetype);
            this.Controls.Add(this.AddinputCtrl);
            this.Controls.Add(this.AddinputMeth);
            this.Controls.Add(this.Command);
            this.Controls.Add(this.Address);
            this.Controls.Add(this.Script);
            this.Controls.Add(this.Control);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "LoggingForm";
            this.Text = "Logging Script Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Control;
        private System.Windows.Forms.ListBox Address;
        private System.Windows.Forms.ListBox Command;
        private System.Windows.Forms.Button AddinputMeth;
        private System.Windows.Forms.Button AddinputCtrl;
        private System.Windows.Forms.ListBox Linetype;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label TestLabel;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LoadScript;
        private System.Windows.Forms.Button SaveScript;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button View;
        private System.Windows.Forms.Button SaveResult;
        private System.Windows.Forms.Button AddinputAdd;
        private System.Windows.Forms.Button AddinputRead;
        private System.Windows.Forms.TextBox Script;
        private System.Windows.Forms.TextBox ExecuteMsg;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button test2;
        private System.Windows.Forms.Button StopScripting;
        private System.Windows.Forms.Label LogVerDisp;
    }
}