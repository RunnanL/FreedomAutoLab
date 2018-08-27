using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GantryDllCs;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        #region gantrymembers
        Color RedNo = Color.Red;
        Color GreenYes = Color.Green;
        Int32 ComGantryStatus_n;     
        Boolean Run_Pct_Flg= false; //percentage UI use
        #endregion

        #region gantrydll
        Gantry GantryDll = new Gantry();
        #endregion

        #region regatronmembers
        const string _tcioLibFileName = "tcio.dll";
        const Int32 DLL_SUCCESS = 0;
        const Int32 DLL_FAIL = -1;
        Int32 comPortStart = 1, comPortEnd = 10, comPortFound = 0;
        Int32 dllState = 0, dllError = 0;
        Boolean connected;
        UInt32 controlIn;
        Double p_vinc, p_iinc, p_pinc, p_rinc, vsinc, isinc, psinc, rsinc;
        Double voltagePhysMax, currentPhysMax, powerPhysMax, resistancePhysMax;
        Double voltagePhysMin, currentPhysMin, powerPhysMin, resistancePhysMin;
        Boolean loadRefValues;
        Boolean isVoltageRefValuePresent;
        Boolean isCurrentRefValuePresent;
        Boolean isPowerRefValuePresent;
        Color changedColor = Color.Yellow;
        Color presentStateColor;
        #endregion

        #region regatron tcio dll imports
        //init
        [DllImport(_tcioLibFileName, EntryPoint = "DllInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DllInit();
        [DllImport(_tcioLibFileName, EntryPoint = "DllClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 DllClose();
        [DllImport(_tcioLibFileName, EntryPoint = "DllGetStatus", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 DllGetStatus(ref Int32 p_dllState, ref Int32 p_dllError);
        [DllImport(_tcioLibFileName, EntryPoint = "DllReadVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 DllReadVersion(ref UInt32 p_version, ref UInt32 p_build, StringBuilder p_string);
        [DllImport(_tcioLibFileName, EntryPoint = "DllSearchDevice", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 DllSearchDevice(Int32 fromport, Int32 toport, ref Int32 p_portnrfound);
        //mandatory
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetPhysicalValuesIncrement", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetPhysicalValuesIncrement(ref Double p_vinc, ref Double p_iinc,
                                                                  ref Double p_pinc, ref Double p_rinc,
                                                                  ref Double v_sinc, ref Double isinc,
                                                                  ref Double p_sinc, ref Double rsinc);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetModuleSelector", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetModuleSelector(UInt32 selector);
        // actual values getters
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetSystemPhysicalLimitMax", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetSystemPhysicalLimitMax(ref Double voltagePhysMax, ref Double currentPhysMax,
                                                                ref Double powerPhysMax, ref Double resistancePhysMax);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetSystemPhysicalLimitMin", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetSystemPhysicalLimitMin(ref Double voltagePhysMin, ref Double currentPhysMin,
                                                                ref Double powerPhysMin, ref Double resistancePhysMin);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetVoltageAct", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetVoltageAct(ref Double p_Voltage);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetCurrentAct", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetCurrentAct(ref Double p_Current);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetPowerAct", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetPowerAct(ref Double p_Power);
        // reference values getters and setters
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetVoltageRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetVoltageRef(ref Double p_Voltage);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetCurrentRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetCurrentRef(ref Double p_Current);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetPowerRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetPowerRef(ref Double p_Power);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetVoltageRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetVoltageRef(Double Voltage);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetCurrentRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetCurrentRef(Double Current);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetPowerRef", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetPowerRef(Double Power);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetRemoteControlInput", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetRemoteControlInput(UInt32 control);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4GetControlIn", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4GetControlIn(ref UInt32 p_control);
        [DllImport(_tcioLibFileName, EntryPoint = "TC4SetControlIn", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TC4SetControlIn(UInt32 control);
        #endregion

        #region LCR init
        LCRmeter HIOKI = new LCRmeter();
        #endregion

        #region ZES power meter init
        PowermeterZES ZESzimmer = new PowermeterZES();
        #endregion

        #region Combobox matched Init
        List<string> LCRunitList = new List<string>();
        List<string> ZEScmdMatchReadList = new List<string>();
        List<string> ZEScmdMatchFetchList = new List<string>();
        List<string> ZESunitList = new List<string>();
        #endregion


        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "UI Version: V1.0.3  ";  //UI version

            #region Gantry Init
            goX.Text = "0";
            goY.Text = "0";
            goZ.Text = "0";

            string Gantry_v;
            Gantry_v =GantryDll.DllVersion();
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Gantry: " + Gantry_v.ToString();

            Xstat.BackColor = RedNo;
            Ystat.BackColor = RedNo;
            Zstat.BackColor = RedNo;
            #endregion       
            #region Regatron Init
            presentStateColor = textBox_voltageRef.BackColor;
            UInt32 version = 0;
            UInt32 build = 0;
            StringBuilder sb = new StringBuilder(256);
            DllReadVersion(ref version, ref build, sb);
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "  RegatronTCIO: V" + (version >> 16).ToString() +  "." + (version & 0xFFFF).ToString();
            #endregion
            #region version info
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "  LCR: V" + HIOKI.VerLcr().ToString();
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "  Powermeter: V" + ZESzimmer.VerZes().ToString();
            #endregion
            #region LCR combox units matchlist init
            LCRunitList.Add("Ω");
            LCRunitList.Add("S");
            LCRunitList.Add("°");
            LCRunitList.Add("F");
            LCRunitList.Add("F");
            LCRunitList.Add(" ");
            LCRunitList.Add("H");
            LCRunitList.Add("H");
            LCRunitList.Add(" ");
            LCRunitList.Add("Ω");
            LCRunitList.Add("S");
            LCRunitList.Add("Ω");
            LCRunitList.Add("Ω");
            LCRunitList.Add("S");
            LCRunitList.Add("Ω");
            LCRunitList.Add(" ");
            LCRunitList.Add(" ");
            #endregion
            #region ZES combobox matchread matchfetch list init
            ZEScmdMatchReadList.Add(":read:voltage:trms");
            ZEScmdMatchReadList.Add(":read:voltage:ac");
            ZEScmdMatchReadList.Add(":read:voltage:dc");
            ZEScmdMatchReadList.Add(":read:current:trms");
            ZEScmdMatchReadList.Add(":read:current:ac");
            ZEScmdMatchReadList.Add(":read:current:dc");
            ZEScmdMatchReadList.Add(":read:power:active");
            ZEScmdMatchReadList.Add(":read:power:reactive");
            ZEScmdMatchReadList.Add(":read:power:apparent");
            ZEScmdMatchReadList.Add(":read:power:pfactor");
            ZEScmdMatchReadList.Add("");

            ZEScmdMatchFetchList.Add(":fetch:voltage:trms");
            ZEScmdMatchFetchList.Add(":fetch:voltage:ac");
            ZEScmdMatchFetchList.Add(":fetch:voltage:dc");
            ZEScmdMatchFetchList.Add(":fetch:current:trms");
            ZEScmdMatchFetchList.Add(":fetch:current:ac");
            ZEScmdMatchFetchList.Add(":fetch:current:dc");
            ZEScmdMatchFetchList.Add(":fetch:power:active");
            ZEScmdMatchFetchList.Add(":fetch:power:reactive");
            ZEScmdMatchFetchList.Add(":fetch:power:apparent");
            ZEScmdMatchFetchList.Add(":fetch:power:pfactor");
            ZEScmdMatchFetchList.Add("");
            ZEScmdMatchFetchList.Add("");
            #endregion
            #region ZES units match list
            ZESunitList.Add("V");
            ZESunitList.Add("V");
            ZESunitList.Add("V");
            ZESunitList.Add("A");
            ZESunitList.Add("A");
            ZESunitList.Add("A");
            ZESunitList.Add("W");
            ZESunitList.Add("Var");
            ZESunitList.Add("VA");
            ZESunitList.Add("-");
            #endregion
        }

        #region Form Events, MenuStrip 
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //close Regatron DLL
            System.Windows.Forms.Application.Exit();
            TimerGantry.Stop();
            TimerRegatron.Stop();
            TimerRegatron.Enabled = false;
            connected = false;
            DllClose();
            Application.ExitThread();
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        private void Exit_Click(object sender, EventArgs e)  //Exit button click
        {
            System.Windows.Forms.Application.Exit();
            TimerGantry.Stop();
            TimerRegatron.Stop();
            TimerRegatron.Enabled = false;
            connected = false;
            DllClose();
            Application.ExitThread();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)  //Too Strip Status
        {
            System.Windows.Forms.Application.Exit();
            TimerGantry.Stop();
            TimerRegatron.Stop();
            TimerRegatron.Enabled = false;
            connected = false;
            DllClose();
            Application.ExitThread();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        #region Menu strip
        private void newScriptLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoggingForm LogParamForm = new LoggingForm(ZESzimmer, GantryDll, toolStripStatusLabel1.Text);
            LogParamForm.Show();
            auto_refresh.Checked = false;
            AutoReadLCR.Checked = false;
            AutoRdZes.Checked = false;
        }

        private void CalibrationManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (0 == ComGantryStatus_n)
            {
                MessageBox.Show("No Gantry connection detected, please connect Gantry before calibration!", "No connection");
            }
            else if (1 == ComGantryStatus_n || 2 == ComGantryStatus_n)
            {
                GantryCalibration GantryCalibrationManual = new GantryCalibration(GantryDll);
                GantryCalibrationManual.Show();
                auto_refresh.Checked = false;
                AutoReadLCR.Checked = false;
                AutoRdZes.Checked = false;
            }
        }
        #endregion

        #endregion

        #region Gantry Events & Buttons
        public int ChkMotor()
        {
            if (GantryDll.MotorCheck(1) == 1)
            {
                Xstat.BackColor = GreenYes;
            }
            else
            {
                Xstat.BackColor = RedNo;
            }
            if (GantryDll.MotorCheck(2) == 1)
            {
                Ystat.BackColor = GreenYes;
            }
            else
            {
                Ystat.BackColor = RedNo;
            }
            if (GantryDll.MotorCheck(3) == 1)
            {
                Zstat.BackColor = GreenYes;
            }
            else
            {
                Zstat.BackColor = RedNo;
            }
            if (Xstat.BackColor == RedNo | Ystat.BackColor == RedNo | Zstat.BackColor == RedNo)
            { MessageBox.Show("One or more motor not detected! Please check connection.", "Warning!", MessageBoxButtons.OK);
                return 1;
            }
            else { return 0; }
        }

        public void ChkAbs()
        { //  motor chk need separate with #1y1 load memory. TBC
            Int32 ResultX, ResultY, ResultZ;
            ResultX = GantryDll.AbsCheck(1);
            switch (ResultX)
            {
                case 1:
                    Xabs.BackColor = GreenYes; break;
                case 0:
                    Xabs.BackColor = RedNo; break;
            }
            ResultY = GantryDll.AbsCheck(2);
            switch (ResultY)
            {
                case 1:
                    Yabs.BackColor = GreenYes; break;
                case 0:
                    Yabs.BackColor = RedNo; break;
            }
            ResultZ = GantryDll.AbsCheck(3);
            switch (ResultZ)
            {
                case 1:
                    Zabs.BackColor = GreenYes; break;
                case 0:
                    Zabs.BackColor = RedNo; break;
            }
            if(0 == ResultX || 0 == ResultY || 0 == ResultZ)
            { MessageBox.Show("ERROR, one or more motors NOT in Absolute positioning mode!\n" +
                "Error motor(s) movement will be based on Direct movement NOT on X,Y,Z coordinate.\n\n"+
                "Please CALIBRATE Gantry before use or retry connection.","WARNING, Not in coordinate movement");
            }
        }

        private void TimerGantry_Tick(object sender, EventArgs e)
        {
            Int32 ReadyBit = 0;
            Double Px = 0.0;
            Double Py = 0.0;
            Double Pz = 0.0;
            Int32 LoopCounter = 0;
            Int32 Code = 0;
            try
            {
                while (true)
                {
                    if (LoopCounter > 5)
                    {
                        auto_refresh.Checked = false; Xstat.BackColor = RedNo; Ystat.BackColor = RedNo; Zstat.BackColor = RedNo;
                        Xrdy.BackColor = RedNo; Yrdy.BackColor = RedNo; Zrdy.BackColor = RedNo; return;
                    }
                    Code = GantryDll.Pos(ref Px, ref Py, ref Pz);
                    if(1 == Code) { break; }
                    else { Thread.Sleep(50); LoopCounter++; }
                }
                GantryDll.CtrlReady(ref ReadyBit);
                    if (1 == (ReadyBit & 100) / 100)
                    { Xrdy.BackColor = GreenYes; }
                    else { Xrdy.BackColor = RedNo; }
                    if (1 == (ReadyBit & 010) / 10)
                    { Yrdy.BackColor = GreenYes; }
                    else { Yrdy.BackColor = RedNo; }
                    if (1 == (ReadyBit & 001))
                    { Zrdy.BackColor = GreenYes; }
                    else { Zrdy.BackColor = RedNo; }

                    posX.Text = Px.ToString();
                    posY.Text = Py.ToString();
                    posZ.Text = Pz.ToString();
                }
                catch (Exception)
                {
                    auto_refresh.Checked = false; Xstat.BackColor = RedNo; Ystat.BackColor = RedNo; Zstat.BackColor = RedNo; 
                    Xrdy.BackColor = RedNo; Yrdy.BackColor = RedNo; Zrdy.BackColor = RedNo;return;
                } //xyz disconnection detect

                if (Run_Pct_Flg == true)
                    try
                    {
                        Double Xdivider = Convert.ToDouble(Xto.Text) - Convert.ToDouble(Xfrom.Text);
                        Double Ydivider = Convert.ToDouble(Yto.Text) - Convert.ToDouble(Yfrom.Text);
                        Double Zdivider = Convert.ToDouble(Zto.Text) - Convert.ToDouble(Zfrom.Text);

                        if (Xdivider != 0)
                        {
                            progressBar1.Value = Convert.ToInt32((Px - Convert.ToDouble(Xfrom.Text)) / Xdivider * 100);
                            perct_tag.Text = progressBar1.Value.ToString();
                        }

                        if (Ydivider != 0)
                        {
                            progressBar2.Value = Convert.ToInt32((Py - Convert.ToDouble(Yfrom.Text)) / Ydivider * 100);
                            perct_tag2.Text = progressBar2.Value.ToString();
                        }

                        if (Zdivider != 0)
                        {
                            progressBar3.Value = Convert.ToInt32((Pz - Convert.ToDouble(Zfrom.Text)) / Zdivider * 100);
                            perct_tag3.Text = progressBar3.Value.ToString();
                        }

                        if (100 == progressBar1.Value && 100 == progressBar2.Value && 100 == progressBar3.Value)
                        { Run_Pct_Flg = false; }
                    }
                    catch (Exception) { }
        }

        private void auto_refresh_CheckedChanged(object sender, EventArgs e)
        {
            if (true == auto_refresh.Checked)
            {
                TimerGantry.Enabled = true;
            }
            else
            {
                TimerGantry.Enabled = false;
            }
        }

        private void Run_Click(object sender, EventArgs e)  //run gantry
        {
            try
            {
                Double TarX = Convert.ToDouble(goX.Text);
                Double TarY = Convert.ToDouble(goY.Text);
                Double TarZ = Convert.ToDouble(goZ.Text);
                progressBar1.Value = 0;
                perct_tag.Text = "0";
                progressBar2.Value = 0;
                perct_tag2.Text = "0";
                progressBar3.Value = 0;
                perct_tag3.Text = "0";
                Xfrom.Text = posX.Text;
                Yfrom.Text = posY.Text;
                Zfrom.Text = posZ.Text;
                Xto.Text = TarX.ToString();
                Yto.Text = TarY.ToString();
                Zto.Text = TarZ.ToString();
                Run_Pct_Flg = true; //progress bar enable flag

                int RetryNoReading = 0;
                int Code = 0;
                while (true)
                {
                    Code = GantryDll.MoveGantry(TarX, TarY, TarZ);
                    if (RetryNoReading >= 5)
                    {
                        DialogResult GantryReadingWarn = MessageBox.Show("Cannot move Gantry, no response from Gantry controller, retry?", "Gantry response timeout", MessageBoxButtons.YesNo);
                        if (GantryReadingWarn == DialogResult.No) { break; }
                        if (GantryReadingWarn == DialogResult.Yes) { RetryNoReading = 0; continue; }
                    }
                    if (1 == Code) { break; }
                    else {Thread.Sleep(100); RetryNoReading++; }
                }
            }
            catch (Exception ErrMove){ MessageBox.Show("Cannot Move Gantry\n\n"+ErrMove,"Cannot move gantry now"); }
        }

        private void ConnectGantry_Click(object sender, EventArgs e) 
        {
            ComGantryStatus_n = GantryDll.GantryComConnect(ComPort.Text); //ref stat
            if (1 == ComGantryStatus_n)
            {
                GantryConnStat.BackColor = GreenYes;
            }
            else if (2 == ComGantryStatus_n)
            {
                GantryConnStat.BackColor = GreenYes;  //already connected n hit conn again
            }
            else
            {
                GantryConnStat.BackColor = RedNo;
                MessageBox.Show("Cannot connect port "+ComPort.Text, "Message", MessageBoxButtons.OK);
                return;
            }
           try
            {
                ChkMotor();
                ChkAbs();
                Double Px = 0.0;
                Double Py = 0.0;
                Double Pz = 0.0;
                GantryDll.Pos(ref Px, ref Py, ref Pz);               
                posX.Text = Px.ToString();
                goX.Text = Px.ToString();
                posY.Text = Py.ToString();
                goY.Text = Py.ToString();
                posZ.Text = Pz.ToString();
                goZ.Text = Pz.ToString();
                auto_refresh.Checked = true;
            }
            catch (Exception ex) { MessageBox.Show("Cannot initialize motor controllers. Motor controller unavailable. \n\n" + ex, "Connection Error!!", MessageBoxButtons.OK); }
            if (1 == ComGantryStatus_n) { MessageBox.Show("Gantry COM port connect successful! Status code: " + ComGantryStatus_n.ToString(), "Com Port Message", MessageBoxButtons.OK); }
            else if (2 == ComGantryStatus_n) { MessageBox.Show("Gantry COM port already connected. Status code: " + ComGantryStatus_n.ToString(), "Com Port Message", MessageBoxButtons.OK); }
            else { MessageBox.Show("Gantry COM port invalid! Please check connection and try again! Status code: " + ComGantryStatus_n.ToString(), "Com Port Message", MessageBoxButtons.OK); }
        }

        private void DisconnGantry_Click(object sender, EventArgs e)
        {
            //int Msg = GantryComDisconnect(ComPort.Text);
            int Msg = GantryDll.GantryComDisconnect();
            if (Msg == 1)
            {
                ComGantryStatus_n = 0;
                GantryConnStat.BackColor = RedNo;
                MessageBox.Show("Gantry COM port disconnected.", "Com Port Message", MessageBoxButtons.OK);
                auto_refresh.Checked = false; //cancel auto refresh free CPU usage
                //change indicators
                Xstat.BackColor = RedNo;
                Ystat.BackColor = RedNo;
                Zstat.BackColor = RedNo;
                Xabs.BackColor = RedNo;
                Yabs.BackColor = RedNo;
                Zabs.BackColor = RedNo;
                Xrdy.BackColor = RedNo;
                Yrdy.BackColor = RedNo;
                Zrdy.BackColor = RedNo;
            }
            else { MessageBox.Show("Gantry COM port UNSUCCESSFUL. Please try again", "Com Port Message", MessageBoxButtons.OK); }          
        }

        private void chkmoto_Click(object sender, EventArgs e)
        {
            ChkMotor();
        }

        private void refreshpos_Click(object sender, EventArgs e)
        {
            try
            {
                Double Px = 0.0;
                Double Py = 0.0;
                Double Pz = 0.0;
                GantryDll.Pos(ref Px, ref Py, ref Pz);
                posX.Text = Px.ToString();
                posY.Text = Py.ToString();
                posZ.Text = Pz.ToString();
            }
            catch (Exception Err) { MessageBox.Show("Cannot read position from Gantry, please try again.","",MessageBoxButtons.OK); }
        }

        private void CMD_go_Click(object sender, EventArgs e)
        {
            //StringBuilder RX = new StringBuilder(128);
            GantryDll.Com_W_R(CMD_txt.Text);
            TestLabel.Text = GantryDll.ReadBuffer.ToString();
            //TestLabel.Text = RX.ToString();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            GantryDll.GantryStop();
            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
            GantryDll.PosRecali();

            Double Px = 0.0;
            Double Py = 0.0;
            Double Pz = 0.0;
   
            posX.Text = Px.ToString();
            posY.Text = Py.ToString();
            posZ.Text = Pz.ToString();
            MessageBox.Show("User stopped Gantry", "Message", MessageBoxButtons.OK);
        }
        #endregion

        #region Regatron Buttons & Events Area 
        private void TimerRegatron_Tick(object sender, EventArgs e)
        {
            Int32 result;
            Double voltageAct = 0.0;
            Double currentAct = 0.0;
            Double powerAct = 0.0;
            if (connected)
            {
                //get values from device
                result = TC4GetVoltageAct(ref voltageAct);
                result = TC4GetCurrentAct(ref currentAct);
                result = TC4GetPowerAct(ref powerAct);
                label_voltageAct.Text = String.Format("{0:0.00}", voltageAct);
                label_currentAct.Text = String.Format("{0:0.00}", currentAct);
                label_powerAct.Text = String.Format("{0:0.00}", powerAct);
                result = TC4GetControlIn(ref controlIn);
                if (controlIn != 0)
                { label_controlState.BackColor = Color.Green; }
                else
                { label_controlState.BackColor = Color.Red; }

                result = DllGetStatus(ref dllState, ref dllError);
                if ((result != 0) || (dllState != 0))
                {
                    SetDisconectState();
                    MessageBox.Show("Error ocured while loading data.\nRegatron disconnected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                SetDisconectState();
                MessageBox.Show("Regatron disconnected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetDisconectState()
        {
            TimerRegatron.Enabled = false;
            connected = false;
            label_voltageAct.Text = "0";
            label_currentAct.Text = "0";
            label_powerAct.Text = "0";
            loadRefValues = true;
            textBox_voltageRef.Text = "0";
            textBox_currentRef.Text = "0";
            textBox_powerRef.Text = "0";
            loadRefValues = false;
            label_controlState.BackColor = Color.Transparent;
        }

        private void button_powerOn_Click(object sender, EventArgs e)
        {
            TC4SetControlIn(1);
        }


        private void button_powerOff_Click(object sender, EventArgs e)
        {
            TC4SetControlIn(0);
        }


        private void button1_Click(object sender, EventArgs e) //set remote control
        {
            TC4SetRemoteControlInput(2);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                string[] comPorts = textBox_comPort.Text.Split(new Char[] { ' ', ',', '-', ':', '/' });
                int i = 0;
                foreach (string s in comPorts)
                {
                    if (s.Trim() != "")
                    { comPorts[i++] = s; }
                }

                if (comPorts.Length > 0)
                { comPortStart = Convert.ToInt32(comPorts[0]); }
                if (comPorts.Length > 1)
                { comPortEnd = Convert.ToInt32(comPorts[1]); }
                else
                { comPortEnd = comPortStart; }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to parse COM Port(s).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Int32 result = DllSearchDevice(comPortStart, comPortEnd, ref comPortFound);
            if (0 == comPortFound)
            {
                MessageBox.Show("NO Regatron connected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegatronConnStat.BackColor = RedNo;
                return;
            }
            result = TC4GetPhysicalValuesIncrement(ref p_vinc, ref p_iinc, ref p_pinc, ref p_rinc, ref vsinc, ref isinc, ref psinc, ref rsinc);
            result = TC4GetSystemPhysicalLimitMax(ref voltagePhysMax, ref currentPhysMax, ref powerPhysMax, ref resistancePhysMax);
            result = TC4GetSystemPhysicalLimitMin(ref voltagePhysMin, ref currentPhysMin, ref powerPhysMin, ref resistancePhysMin);
            result = TC4SetModuleSelector(64);
            result = DllGetStatus(ref dllState, ref dllError);
            if ((result != 0) || (dllState != 0))
            {
                MessageBox.Show("DllInit failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegatronConnStat.BackColor = RedNo;
            }
            else
            {
                RegatronConnStat.BackColor = GreenYes;
                MessageBox.Show("Regatron connected to port: " + comPortFound.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_comPort.Text = comPortFound.ToString();
                connected = true;
                //get init values
                ReadVoltageRef();
                ReadCurrentRef();
                ReadPowerRef();
                result = DllGetStatus(ref dllState, ref dllError);
                if ((result != 0) || (dllState != 0))
                {
                    MessageBox.Show("Error ocured while loading data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RegatronConnStat.BackColor = RedNo;
                }

                TimerRegatron.Enabled = true;
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            Int32 result = DllClose();
            if (result != DLL_SUCCESS)
            { MessageBox.Show("Regatron not disconnected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            { connected = false;
              RegatronConnStat.BackColor = RedNo;
            }
        }

        private void textBox_voltageRef_TextChanged_1(object sender, EventArgs e)
        {
            if (!connected || loadRefValues) { return; }
            isVoltageRefValuePresent = false;
            textBox_voltageRef.BackColor = changedColor;
        }

        private void textBox_currentRef_TextChanged_1(object sender, EventArgs e)
        {
            if (!connected || loadRefValues) { return; }
            isCurrentRefValuePresent = false;
            textBox_currentRef.BackColor = changedColor;
        }


        private void textBox_powerRef_TextChanged_1(object sender, EventArgs e)
        {
            if (!connected || loadRefValues) { return; }
            isPowerRefValuePresent = false;
            textBox_powerRef.BackColor = changedColor;
        }

        private void textBox_voltageRef_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !isVoltageRefValuePresent && connected)
            { WriteVoltageRef(); }
        }

        private void textBox_currentRef_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !isCurrentRefValuePresent && connected)
            { WriteCurrentRef(); }
        }


        private void textBox_powerRef_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !isPowerRefValuePresent && connected)
            { WritePowerRef(); }
        }


        private void textBox_voltageRef_Leave(object sender, EventArgs e)
        {
            if (!isVoltageRefValuePresent && connected)
            { WriteVoltageRef(); }
        }


        private void textBox_currentRef_Leave(object sender, EventArgs e)
        {
            if (!isCurrentRefValuePresent && connected)
            { WriteCurrentRef(); }
        }

        private void textBox_powerRef_Leave(object sender, EventArgs e)
        {
            if (!isPowerRefValuePresent && connected)
            { WritePowerRef(); }
        }


        //Regatron self functions
        private Int32 ReadVoltageRef()
        {
            Int32 result = DLL_FAIL;
            Double voltageRef = 0.0;
            result = TC4GetVoltageRef(ref voltageRef);
            loadRefValues = true;
            try
            {
                if (DLL_SUCCESS == result)
                {
                    textBox_voltageRef.Text = String.Format("{0:0.00}", voltageRef);
                    isCurrentRefValuePresent = true;
                }
            }
            catch (Exception) { }

            loadRefValues = false;
            return result;
        }

        private Int32 ReadCurrentRef()
        {
            Int32 result = DLL_FAIL;
            Double currentRef = 0.0;
            result = TC4GetCurrentRef(ref currentRef);
            loadRefValues = true;
            try
            {
                if (DLL_SUCCESS == result)
                {
                    textBox_currentRef.Text = String.Format("{0:0.00}", currentRef);
                    isCurrentRefValuePresent = true;
                }
            }
            catch (Exception) { }

            loadRefValues = false;
            return result;
        }


        private Int32 ReadPowerRef()
        {
            Int32 result = DLL_FAIL;
            Double powerRef = 0.0;
            result = TC4GetPowerRef(ref powerRef);
            loadRefValues = true;
            try
            {
                if (DLL_SUCCESS == result)
                {
                    textBox_powerRef.Text = String.Format("{0:0.00}", powerRef);
                    isPowerRefValuePresent = true;
                }
            }
            catch (Exception) { }
            loadRefValues = false;
            return result;
        }

        private Int32 WriteVoltageRef()
        {
            Int32 result = DLL_FAIL;
            try
            {
                Double voltageRef = Convert.ToDouble(textBox_voltageRef.Text);
                voltageRef = GetValueInnerLimits(voltageRef, voltagePhysMin, voltagePhysMax);
                result = TC4SetVoltageRef(voltageRef);
                if (DLL_SUCCESS == result)
                {
                    result = ReadVoltageRef();
                    isVoltageRefValuePresent = true;
                    textBox_voltageRef.BackColor = presentStateColor;
                }
            }
            catch (Exception) { }
            return result;
        }


        private Int32 WriteCurrentRef()
        {
            Int32 result = DLL_FAIL;
            try
            {
                Double currentRef = Convert.ToDouble(textBox_currentRef.Text);
                currentRef = GetValueInnerLimits(currentRef, currentPhysMin, currentPhysMax);
                result = TC4SetCurrentRef(currentRef);
                if (DLL_SUCCESS == result)
                {
                    result = ReadCurrentRef();
                    isCurrentRefValuePresent = true;
                    textBox_currentRef.BackColor = presentStateColor;
                }
            }
            catch (Exception) { }
            return result;
        }

        private Int32 WritePowerRef()
        {
            Int32 result = DLL_FAIL;
            try
            {
                Double powerRef = Convert.ToDouble(textBox_powerRef.Text);
                powerRef = GetValueInnerLimits(powerRef, powerPhysMin, powerPhysMax);
                result = TC4SetPowerRef(powerRef);
                if (DLL_SUCCESS == result)
                {
                    result = ReadPowerRef();
                    isPowerRefValuePresent = true;
                    textBox_powerRef.BackColor = presentStateColor;
                }
            }
            catch (Exception) { }
            return result;
        }

        private Double GetValueInnerLimits(Double value, Double min, Double max)
        {
            if (value > max)
            { value = max; }
            if (value < min)
            { value = min; }
            return value;
        }






        #endregion

        #region  LCR HIOKI
        private void LCR_connect_Click(object sender, EventArgs e)
        {

            //System.Net.IPAddress.TryParse(LCRip.Text,out ip);
            Int32 Result;
            Result =HIOKI.ConnectLCR(LCRip.Text, Convert.ToInt32(LCRport.Text));
            if (Result == 0)
            { LCRConnStat.BackColor = GreenYes;

                HIOKI.LCR_Param_Change(1, LCR1Name.Text);
                LCR1unit.Text = LCRunitList[LCR1Name.SelectedIndex];
                HIOKI.LCR_Param_Change(2, LCR2Name.Text);
                LCR2unit.Text = LCRunitList[LCR2Name.SelectedIndex];
                HIOKI.LCR_Param_Change(3, LCR3Name.Text);
                LCR3unit.Text = LCRunitList[LCR3Name.SelectedIndex];
                HIOKI.LCR_Param_Change(4, LCR4Name.Text);
                LCR4unit.Text = LCRunitList[LCR4Name.SelectedIndex];
                AutoReadLCR.Checked = true;
            }
            else { LCRConnStat.BackColor = RedNo; }
        }


        private void LCR_disconnect_Click(object sender, EventArgs e)
        {
            AutoReadLCR.Checked = false;
            HIOKI.DisconnectLCR();
            LCRConnStat.BackColor = RedNo;
        }


        private void LCRcmdSd_Click(object sender, EventArgs e)
        {
            //command manual
                HIOKI.LCRwrite_read(LCRCMD.Text);
                TestLabel.Text = HIOKI.ResponseData;
        }

        private void ReadLCR_Click(object sender, EventArgs e)
        {
           try
            {
                HIOKI.Measure();
                string[] tokens = HIOKI.ResponseData.Split(',');
                LCR1Value.Text = tokens[0];//Decimal.Parse( tokens[0], System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint).ToString();
                LCR2Value.Text = tokens[1];
                LCR3Value.Text = tokens[2];
                LCR4Value.Text = tokens[3];
            }
            catch { }
        }


        private void AutoReadLCR_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoReadLCR.Checked == true)
            {
                TimerLCR.Enabled = true;
            }
            else
            {
                TimerLCR.Enabled = false;
            }
        }

        private void TimerLCR_Tick(object sender, EventArgs e)
        {
            try
            {
                try { HIOKI.Measure();}
                catch (Exception) { AutoReadLCR.Checked = false; TimerLCR.Enabled = false; return; }
                string[] tokens = HIOKI.ResponseData.Split(',');
                LCR1Value.Text = tokens[0];//Decimal.Parse( tokens[0], System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint).ToString();
                LCR2Value.Text = tokens[1];
                LCR3Value.Text = tokens[2];
                LCR4Value.Text = tokens[3];
                
            }
            catch (Exception ex) { AutoReadLCR.Checked = false; TimerLCR.Enabled = false; return; }
        }

        private void LCR1Name_TextChanged(object sender, EventArgs e)
        {
            HIOKI.LCR_Param_Change(1,LCR1Name.Text);
        }

        private void LCR1Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCR1unit.Text = LCRunitList[LCR1Name.SelectedIndex];
        }

        private void LCR2Name_TextChanged(object sender, EventArgs e)
        {
            HIOKI.LCR_Param_Change(2, LCR2Name.Text);
        }

        private void LCR2Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCR2unit.Text = LCRunitList[LCR2Name.SelectedIndex];
        }

        private void LCR3Name_TextChanged(object sender, EventArgs e)
        {
            HIOKI.LCR_Param_Change(3, LCR3Name.Text);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lumen Freedom AutoLab\n"+toolStripStatusLabel1.Text,"About",MessageBoxButtons.OK);
        }

        private void LCR3Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCR3unit.Text = LCRunitList[LCR3Name.SelectedIndex];
        }

        private void LCR4Name_TextChanged(object sender, EventArgs e)
        {
            HIOKI.LCR_Param_Change(4, LCR4Name.Text);
        }

        private void LCR4Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCR4unit.Text = LCRunitList[LCR4Name.SelectedIndex];
        }

        private void LCR5Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HIOKI.LCR_Setting_Change(LCR5Input.Text);
                LCR5Value.Text = HIOKI.ResponseData;
            }
        }

        #endregion

        #region Power Meter
        private void ZESconnect_Click(object sender, EventArgs e)
        {
            Int32 ConStat=3;
            ConStat = ZESzimmer.ConnectPowerMeter(ZEScomNo.Text);

            if (ConStat == 0)
            {
                ZESconnstat.BackColor = GreenYes;
                ZES1unit.Text = ZESunitList[ZESvalue1.SelectedIndex];
                ZES2unit.Text = ZESunitList[ZESvalue2.SelectedIndex];
                ZES2unit.Text = ZESunitList[ZESvalue3.SelectedIndex];
                ZES4unit.Text = ZESunitList[ZESvalue4.SelectedIndex];
            }
            else { ZESconnstat.BackColor = RedNo; }

            AutoRdZes.Checked = true;
        }

        private void ZESdisconnect_Click(object sender, EventArgs e)
        {
            int Constat = 3;
            Constat = ZESzimmer.DisconnectPowerMeter();
            if (Constat == 0) { ZESconnstat.BackColor = RedNo; }
            AutoRdZes.Checked = false;
        }

        private void ZEScmd_Click(object sender, EventArgs e)
        {
            ZESzimmer.ZES_Send_Read(ZEScmdTXT.Text);
            MessageBox.Show("Command reading from Power Analyzer is:\n"+ZESzimmer.ZESResponseData, "Power Meter Reading", MessageBoxButtons.OK);
        }

        private void ZesReadAct_Click(object sender, EventArgs e)
        {
            Int32 ReadNoExp = 0;
            ReadNoExp = SendZESCommand();
            ReadZESCommandUI(ReadNoExp);
            //MessageBox.Show(ZESzimmer.ZESResponseData, "sssssTest Display Window", MessageBoxButtons.OK);

        }

        public int SendZESCommand()
        {
            try
            {
                String DataSend = "";
                Int32 RequestNo = 0;
                if (!ZESvalue1.Items.Contains(ZESvalue1.Text.Trim()))
                { DataSend = ZESvalue1.Text + Chbox1.Text + ";"; DataSend.Trim(); RequestNo++; }
                else
                { DataSend = ZEScmdMatchReadList[ZESvalue1.SelectedIndex] + Chbox1.Text + ";"; DataSend.Trim(); RequestNo++; }
                if (ZESvalue2EN.Checked)
                { DataSend += ZEScmdMatchFetchList[ZESvalue2.SelectedIndex] + Chbox2.Text + ";"; DataSend.Trim(); RequestNo++; }
                if (ZESvalue3EN.Checked)
                { DataSend += ZEScmdMatchFetchList[ZESvalue3.SelectedIndex] + Chbox3.Text + ";"; DataSend.Trim(); RequestNo++; }
                if (ZESvalue4EN.Checked)
                { DataSend += ZEScmdMatchFetchList[ZESvalue4.SelectedIndex] + Chbox4.Text + ";"; DataSend.Trim(); RequestNo++; }
                if (ZESvalueCustEN.Checked)
                { DataSend += ZESvalueCust.Text.Trim() + Chbox2.Text + ";"; DataSend.Trim(); RequestNo++; }

                ZESzimmer.ZES_Send_Read(DataSend);
                return RequestNo;
            }
            catch (Exception) { return 0; }// MessageBox.Show(ErrZes.ToString(), "Error ZES Send", MessageBoxButtons.OK); }
        }

        public void ReadZESCommandUI(Int32 No2Read)
        {
            try
            {
                TestLabel.Text = ZESzimmer.ZESResponseData;
                String[] tokens = ZESzimmer.ZESResponseData.Split(';');
                if (tokens.Length == No2Read)
                {
                    Int32 TokNo = 0;
                    ZesValue1Act.Text = tokens[TokNo++];
                    if (ZESvalue2EN.Checked)
                    { ZesValue2Act.Text = tokens[TokNo++]; }
                    if (ZESvalue3EN.Checked)
                    { ZesValue3Act.Text = tokens[TokNo++]; }
                    if (ZESvalue2EN.Checked)
                    { ZesValue4Act.Text = tokens[TokNo++]; }
                    if (ZESvalueCustEN.Checked)
                    { ZesValueCustAct.Text = tokens[TokNo++]; }
                }
               
            }
            catch (Exception ErrZesrd) { //MessageBox.Show(ErrZesrd.ToString(), "Error ZES Read", MessageBoxButtons.OK); 
            }
        }

        private void AutoRdZes_CheckedChanged(object sender, EventArgs e)
        {
            if(AutoRdZes.Checked==true)
            { TimerPowerMeter.Enabled = true; }
            if(AutoRdZes.Checked==false)
            { TimerPowerMeter.Enabled = false; }
        }

        private void TimerPowerMeter_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    String DataSend = "";
                    Int32 RequestNo = 0;
                    if (!ZESvalue1.Items.Contains(ZESvalue1.Text.Trim()))
                    { DataSend = ZESvalue1.Text + Chbox1.Text + ";"; DataSend.Trim(); RequestNo++; }
                    else
                    { DataSend = ZEScmdMatchReadList[ZESvalue1.SelectedIndex] + Chbox1.Text + ";"; DataSend.Trim(); RequestNo++; }
                    if (ZESvalue2EN.Checked)
                    { DataSend += ZEScmdMatchFetchList[ZESvalue2.SelectedIndex] + Chbox2.Text + ";"; DataSend.Trim(); RequestNo++; }
                    if (ZESvalue3EN.Checked)
                    { DataSend += ZEScmdMatchFetchList[ZESvalue3.SelectedIndex] + Chbox3.Text + ";"; DataSend.Trim(); RequestNo++; }
                    if (ZESvalue4EN.Checked)
                    { DataSend += ZEScmdMatchFetchList[ZESvalue4.SelectedIndex] + Chbox4.Text + ";"; DataSend.Trim(); RequestNo++; }
                    if (ZESvalueCustEN.Checked)
                    { DataSend += ZESvalueCust.Text.Trim() + Chbox2.Text + ";"; DataSend.Trim(); RequestNo++; }
                    ZESzimmer.ZES_Send_Read(DataSend); //timer send read com
                    //TestLabel.Text = ZESzimmer.ZESResponseData;
                    String[] tokens = ZESzimmer.ZESResponseData.Split(';');
                    if (tokens.Length == RequestNo)
                     {
                         Int32 TokNo = 0;
                         ZesValue1Act.Text = tokens[TokNo++];
                         if (ZESvalue2EN.Checked)
                            { ZesValue2Act.Text = tokens[TokNo++]; }
                         if (ZESvalue3EN.Checked)
                            { ZesValue3Act.Text = tokens[TokNo++]; }
                         if (ZESvalue2EN.Checked)
                            { ZesValue4Act.Text = tokens[TokNo++]; }
                         if (ZESvalueCustEN.Checked)
                            { ZesValueCustAct.Text = tokens[TokNo++]; }
                     }
                }
               catch (Exception) { AutoRdZes.Checked = false; TimerPowerMeter.Enabled = false; return; }
            }
            catch (Exception) { AutoRdZes.Checked = false; TimerPowerMeter.Enabled = false; return; }
        }

        #region PowerMeter Unit change
        private void ZESvalue1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZES1unit.Text = ZESunitList[ZESvalue1.SelectedIndex];
        }
        private void ZESvalue2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZES2unit.Text = ZESunitList[ZESvalue2.SelectedIndex];
        }
        private void ZESvalue3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZES3unit.Text = ZESunitList[ZESvalue3.SelectedIndex];
        }
        private void ZESvalue4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZES4unit.Text = ZESunitList[ZESvalue4.SelectedIndex];
        }
        #endregion

        #endregion


    }


}