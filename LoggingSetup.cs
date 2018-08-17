using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using GantryDllCs;// new

namespace WindowsFormsApp1
{

    public partial class LoggingForm : Form
    {   //Form 
        //DataLogging LoggingData = new DataLogging();
        List<string> LinetypeList = new List<string>();
        List<string> ControlList = new List<string>();
        List<string> AddressList = new List<string>();
        List<string> CommandList = new List<string>();
        //Script logging
        public List<string> CSVcontents = new List<string>();  //stores entire formatted csv
        public List<string> ReadList = new List<string>();  //stores script output
        public List<string> OutList = new List<string>(); //stores output data to csv file
        public List<string> TestList = new List<string>();
        string ControlSearch = "$";
        string AddressSearch = "&";
        string CmdSearch = "#";
        string ReadSearch = "|";

        bool StopFlag = false;  //Stop scripting indicator. True to trigget stop

        public PowermeterZES ZES_ZIMMER;
        public Gantry GantryDll; // new 
        public string UiVersion = "";

        public string LogVer()  //Logging service version number
        {
            return "1.0.2";
        }

        public LoggingForm(PowermeterZES ZES_ZIMMER, Gantry GantryDll, string UiVersionString)
        {
            InitializeComponent();
            LogVerDisp.Text = "Logging module: V"+LogVer(); // Version of logging module

            this.ZES_ZIMMER = ZES_ZIMMER;
            this.GantryDll = GantryDll;
            LinetypeList.Add("$, ");
            LinetypeList.Add("&, ");
            LinetypeList.Add("#, ");
            LinetypeList.Add("|, ");
            LinetypeList.Add(",; ");

            ControlList.Add("X, Y, Z, ");
            ControlList.Add("Intv, ");
            ControlList.Add("HIOKI(), ");
            ControlList.Add("Freq, ");

            CommandList.Add("X, Y, Z, ");
            CommandList.Add("LCR1, ");
            CommandList.Add("LCR2, ");
            CommandList.Add("LCR3, ");
            CommandList.Add("LCR4, ");
            CommandList.Add("LCRmeasureall, ");
            CommandList.Add("Freq, ");
            CommandList.Add("Regatron_P, ");
            CommandList.Add("Regatron_V, ");
            CommandList.Add("Regatron_A, ");
            CommandList.Add("ZES(), ");

            UiVersion = UiVersionString;
        }

        #region ScriptLogging
        public async void ExecuteScript()
        {
            StopFlag = false; // clear stopflag from running program
            //init Execute script param
            List<string> CtrlOrder = new List<string>();
            //link equipment class
            LCRmeter HIOKI = new LCRmeter();

            //PowermeterZES ZES_ZIMMER = new PowermeterZES();
            //Clear read buffer used before
            ReadList.Clear();
            int LineNo = 1;

            //read each line of code
            foreach (string LineItem in CSVcontents)
            {
                if (StopFlag == true) { break; } //Stop Flag Set, break loop
                ExecuteMsg.AppendText("----Executing : " + LineItem + '\n' + "   Progress...: "+ LineNo + "/" + CSVcontents.Count+'\n');
                //Console.WriteLine("Executing line " + LineNo);

                if (LineItem.StartsWith(ControlSearch, StringComparison.InvariantCultureIgnoreCase))  //if script command = control $
                {
                    CtrlOrder.Clear();
                    var FragmentCtrl = LineItem.Split(',');
                    int walk = 0;
                    while (walk < (FragmentCtrl.Length))
                    {
                        switch (FragmentCtrl[walk].Trim().ToUpper())
                        {
                            case "X":  //if find x execute as x y z altogether
                                CtrlOrder.Add("Gantry");
                                walk += 3;
                                break;
                            case "FREQ":
                                CtrlOrder.Add("LCR");
                                walk++;
                                break;
                            case "INTV":
                                CtrlOrder.Add("INTV");
                                walk++;
                                break;
                            case "HIOKI()":
                                CtrlOrder.Add("HIOKI");
                                walk++;
                                break;
                            default:
                                walk++; break;
                        }

                    }
                }
                if (LineItem.StartsWith(AddressSearch, StringComparison.InvariantCultureIgnoreCase)) //if script command = address 
                {
                }
                if (LineItem.StartsWith(CmdSearch, StringComparison.InvariantCultureIgnoreCase))  //if script command = cmd 
                {
                    var FragmentCmd = LineItem.Split(',');
                    int WalkCtrl = 0;
                    int WalkCmd = 1;//skip first # in Cmd
                    while (WalkCmd < (FragmentCmd.Length ))   //walk through cmd line
                    {
                        if (StopFlag == true) { break; } //Stop Flag Set, break loop
                        switch (CtrlOrder[WalkCtrl].ToUpper())
                        {
                            case "GANTRY":
                                try
                                {
                                    Double x; Double y; Double z;
                                    x = Convert.ToDouble(FragmentCmd[WalkCmd]);
                                    y = Convert.ToDouble(FragmentCmd[++WalkCmd]);
                                    z = Convert.ToDouble(FragmentCmd[++WalkCmd]);
                                    ExecuteMsg.AppendText("Moving to : " + x + "," + y + "," + z + '\n');
                                    WalkCtrl++;
                                    WalkCmd++;
                                    GantryDll.MoveGantry(x, y, z);

                                    while (true)
                                    { if (StopFlag == true)
                                        {
                                            try
                                            {
                                                GantryDll.GantryStop();
                                                Thread.Sleep(1500);
                                                GantryDll.PosRecali();
                                            }
                                            catch (Exception ErrStopScript) { }
                                            break;
                                         }
                                        Int32 Readybit = 0;
                                        try
                                        {
                                            GantryDll.CtrlReady(ref Readybit);
                                            if (111 == Readybit)
                                            { break; }
                                            //Thread.Sleep(500);
                                            await Task.Delay(500);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                                catch (Exception GanMovErr) { }
                                break;
                            case "LCR":
                                try
                                {
                                    ExecuteMsg.AppendText("LCR changing to " + FragmentCmd[WalkCmd] + '\n');
                                    HIOKI.LCR_Setting_Change(FragmentCmd[WalkCmd++]);
                                    WalkCtrl++;
                                }
                                catch (Exception LCRwriteErr) { }
                                break;
                            case "INTV":
                                try
                                {
                                    double miliseconds = Convert.ToDouble(FragmentCmd[WalkCmd++]) * 1000;
                                    WalkCtrl++;
                                    ExecuteMsg.AppendText("Waiting " + miliseconds / 1000 + " s" + '\n');
                                    //Thread.Sleep(miliseconds);
                                    await Task.Delay(Convert.ToInt32(miliseconds));
                                    WalkCtrl++;
                                }
                                catch (Exception IntvErr) { }
                                break;
                            case "HIOKI":
                                try
                                {
                                    if (Regex.IsMatch(FragmentCmd[WalkCmd].Trim().ToUpper().ToString(), @"i*[H][I][O][K][I]\((.*)\)"))  //find command like HIOKI(xxx)
                                    {
                                        string CMDstr = Regex.Match(FragmentCmd[WalkCmd].Trim().ToUpper().ToString(), @"(?<=\()(.*)(?=\))").Value;
                                        HIOKI.LCRwrite_read(CMDstr);
                                    }
                                    else { MessageBox.Show("HIOKI Command input is invalid!", "WARNING!"); }
                                    WalkCmd++;
                                    WalkCtrl++;
                                }
                                catch(Exception LcrCmdErr) { }
                                break;
                            default:
                                WalkCtrl++; break;
                        }

                    }
                    //ExecuteMsg.AppendText(" ");

                }
                if (LineItem.StartsWith(ReadSearch, StringComparison.InvariantCultureIgnoreCase)) //if script cmd = read 
                {
                    var FragmentRead = LineItem.Split(',');
                    int WalkRead = 1;//skip first | in Cmd
                    string LineContents = "";
                    string ReadBuffer = "";
                    //string FragmentJudgement = "";
                    while (WalkRead < FragmentRead.Length)
                    {
                        if (StopFlag == true) { break; } //Stop Flag Set, break loop
                        switch (FragmentRead[WalkRead].Trim().ToUpper())
                        {
                            //case "XYZ":
                            case "X":
                                try
                                {
                                    Double x = 0.0, y = 0.0, z = 0.0;
                                    int RetryNoReading = 0;
                                    int Code = 0;
                                    while (RetryNoReading < 5)
                                    {
                                        Code = GantryDll.Pos(ref x, ref y, ref z);
                                        if (RetryNoReading >= 5)
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("Cannot read gantry position", "Error", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryNoReading = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if(1 == Code) { break; }
                                        else { RetryNoReading++; }
                                    }
                                    ReadBuffer = Convert.ToString(x) + "," + Convert.ToString(y) + "," + Convert.ToString(z) + ",";
                                    //gantry data check 
                                    int RetryGantryCount = 0;
                                    while (true) // retry loop if Gantry read is not correct
                                    { 
                                        string[] tokens = ReadBuffer.Split(',');
                                        if(RetryGantryCount>4) //Retry read gantry times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("One or more gantry position reading is EMPTY, please check gantry connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryGantryCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if(""==tokens[0].Trim()||""==tokens[1].Trim()||""==tokens[2].Trim())
                                        {
                                            await Task.Delay(250);
                                            GantryDll.Pos(ref x, ref y, ref z);
                                            ReadBuffer = Convert.ToString(x) + "," + Convert.ToString(y) + "," + Convert.ToString(z) + ",";
                                            RetryGantryCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass check, store data
                                    LineContents = LineContents + ReadBuffer;
                                    ExecuteMsg.AppendText("Gantry position read;\n");
                                }
                                catch (Exception GanErr) { }
                                WalkRead += 3;
                                break;
                           case "LCRREADALL": // when receive LCRMEASUREALL, measure all four channels of LCR
                                try
                                {
                                    HIOKI.Measure();
                                    string[] tokenslcr;
                                    //LCR read data check
                                    int RetryLCRCount = 0;
                                    while (true) // retry loop if LCR read is not correct
                                    {
                                        tokenslcr = HIOKI.ResponseData.Split(',');
                                        if (RetryLCRCount > 4) //Retry read LCR times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("One or more LCR measure is EMPTY, please check LCR connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryLCRCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if (" " == tokenslcr[0].Trim() || " " == tokenslcr[1].Trim() || " " == tokenslcr[2].Trim() || " "==tokenslcr[3].Trim())
                                        {
                                            await Task.Delay(200);
                                            HIOKI.Measure();
                                            RetryLCRCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass check, store data
                                    LineContents = LineContents + tokenslcr[0].Trim() + "," + tokenslcr[1].Trim() + "," + tokenslcr[2].Trim() + "," + tokenslcr[3].Trim() + ",";
                                    ExecuteMsg.AppendText("LCR all parameters read\n");
                                }
                                catch (Exception LCRerr) { LineContents = LineContents + " ErrReading, ErrReading, ErrReading, ErrReading,"; }
                                WalkRead +=4;
                                break;
                            case "LCR1":
                                try
                                {
                                    HIOKI.Measure();
                                    string[] tokens1;
                                    //data check
                                    int RetryLCRCount = 0;
                                    while (true) // retry loop if LCR read is not correct
                                    {
                                        tokens1 = HIOKI.ResponseData.Split(',');
                                        //ExecuteMsg.AppendText("LCR1:"+tokens1[0].Trim() + '/' + tokens1[1].Trim() + '/' + tokens1[2].Trim() + '/' + tokens1[3].Trim()); //TEST ONLY
                                        if (RetryLCRCount > 4) //Retry read LCR1 times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("LCR1 reading is EMPTY, please check LCR connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryLCRCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if (" " == tokens1[0].Trim()|| "" == tokens1[0].Trim())
                                        {
                                            await Task.Delay(200);
                                            HIOKI.Measure();
                                            RetryLCRCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass LCR1 check
                                    LineContents = LineContents + tokens1[0].Trim() + ",";
                                    ExecuteMsg.AppendText("LCR1 read;\n");
                                }
                                catch (Exception LCRerr) { LineContents = LineContents + "ErrReading,"; }
                                WalkRead++;
                                break;
                            case "LCR2":
                                try
                                {
                                    HIOKI.Measure();
                                    string[] tokens2;
                                    //data check
                                    int RetryLCRCount = 0;
                                    while (true) // retry loop if LCR read is not correct
                                    {
                                        tokens2 = HIOKI.ResponseData.Split(',');
                                        //ExecuteMsg.AppendText("LCR2:"+tokens2[0].Trim() + '/' + tokens2[1].Trim() + '/' + tokens2[2].Trim() + '/' + tokens2[3].Trim()); //TEST ONLY
                                        if (RetryLCRCount > 4) //Retry read LCR1 times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("LCR2 reading is EMPTY, please check LCR connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryLCRCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if (" " == tokens2[1].Trim()|| "" == tokens2[1].Trim())
                                        {
                                            await Task.Delay(200);
                                            HIOKI.Measure();
                                            RetryLCRCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass LCR1 check
                                    LineContents = LineContents + tokens2[1].Trim() + ",";
                                    ExecuteMsg.AppendText("LCR2 read;\n");
                                }
                                catch (Exception LCRerr) { LineContents = LineContents + "ErrReading,"; }
                                WalkRead++;
                                break;
                            case "LCR3":
                                try
                                {
                                    HIOKI.Measure();
                                    string[] tokens3;
                                    //data check
                                    int RetryLCRCount = 0;
                                    while (true) // retry loop if LCR read is not correct
                                    {
                                        tokens3 = HIOKI.ResponseData.Split(',');
                                        //ExecuteMsg.AppendText("LCR3:"+tokens3[0].Trim() + '/' + tokens3[1].Trim() + '/' + tokens3[2].Trim() + '/' + tokens3[3].Trim()); //TEST ONLY
                                        if (RetryLCRCount > 4) //Retry read LCR1 times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("LCR3 reading is EMPTY, please check LCR connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryLCRCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if (" " == tokens3[2].Trim()|| "" == tokens3[2].Trim())
                                        {
                                            await Task.Delay(200);
                                            HIOKI.Measure();
                                            RetryLCRCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass LCR1 check
                                    LineContents = LineContents + tokens3[2].Trim() + ",";
                                    ExecuteMsg.AppendText("LCR3 read;\n");
                                }
                                catch (Exception LCRerr) { LineContents = LineContents + "ErrReading,"; }
                                WalkRead++;
                                break;
                            case "LCR4":
                                try
                                {
                                    HIOKI.Measure();
                                    string[] tokens4;
                                    //data check
                                    int RetryLCRCount = 0;
                                    while (true) // retry loop if LCR read is not correct
                                    {
                                        tokens4 = HIOKI.ResponseData.Split(',');
                                        //ExecuteMsg.AppendText("LCR4:"+tokens4[0].Trim() + '/' + tokens4[1].Trim() + '/' + tokens4[2].Trim() + '/' + tokens4[3].Trim()); //TEST ONLY
                                        if (RetryLCRCount > 4) //Retry read LCR1 times > 4, pop out dialogue for user to decide
                                        {
                                            DialogResult GantryReadingWarn = MessageBox.Show("LCR3 reading is EMPTY, please check LCR connection", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                            if (GantryReadingWarn == DialogResult.Abort) { StopFlag = true; break; }
                                            if (GantryReadingWarn == DialogResult.Retry) { RetryLCRCount = 0; continue; }
                                            if (GantryReadingWarn == DialogResult.Ignore) { break; }
                                        }
                                        if (" " == tokens4[3].Trim()|| "" == tokens4[3].Trim())
                                        {
                                            await Task.Delay(200);
                                            HIOKI.Measure();
                                            RetryLCRCount++;
                                        }
                                        else { break; }
                                    }
                                    //pass LCR1 check
                                    LineContents = LineContents + tokens4[3].Trim() + ",";
                                    ExecuteMsg.AppendText("LCR4 read;\n");
                                }
                                catch (Exception LCRerr) { LineContents = LineContents + "ErrReading,"; }
                                WalkRead++;
                                break;

                            case "Regatron_P":
                                try
                                {
                                    Double powerRef = 0.0;
                                    Form1.TC4GetPowerAct(ref powerRef);
                                    LineContents = LineContents + String.Format("{0:0.00}", powerRef) + ",";
                                    ExecuteMsg.AppendText("Regatron Power Read;\n");
                                }
                                catch (Exception RegatronErr) { }
                                WalkRead++;
                                break;

                            case "Regatron_V":
                                try
                                {
                                    Double voltageRef = 0.0;
                                    Form1.TC4GetVoltageAct(ref voltageRef);
                                    LineContents = LineContents + String.Format("{0:0.00}", voltageRef) + ","; 
                                    ExecuteMsg.AppendText("Regatron Voltage Read;\n");
                                }
                                catch (Exception RegatronErr) { }
                                WalkRead++;
                                break;

                            case "Regatron_A":
                                try
                                {
                                    Double currentRef = 0.0;
                                    Form1.TC4GetCurrentAct(ref currentRef);
                                    LineContents = LineContents + String.Format("{0:0.00}", currentRef) + ","; 
                                    ExecuteMsg.AppendText("Regatron Current Read; " + '\n');                                  
                                }
                                catch (Exception RegatronErr) { }
                                WalkRead++;
                                break;
                            default:  //will run when not matching script commands: ZES()
                                if (Regex.IsMatch(FragmentRead[WalkRead].Trim().ToUpper().ToString(), @"i*[Z][E][S]\((.*)\)"))  //find command like ZES(xxx)
                                {
                                    string CMDstr = Regex.Match(FragmentRead[WalkRead].Trim().ToUpper().ToString(), @"(?<=\()(.*)(?=\))").Value;
                                    ZES_ZIMMER.ZES_Send_Read(CMDstr);
                                    string[] tokensCMD = CMDstr.Split(';');
                                    string[] tokens = ZES_ZIMMER.ZESResponseData.Split(';');

                                    int RetryCount = 0;
                                    while (true)  //retry loop if no data or number of data read was incorrect
                                    {
                                        if (tokensCMD.Length == tokens.Length) { break; }
                                        else if (tokensCMD.Length != tokens.Length)
                                        {
                                            if (RetryCount > 4) // Retry read 5 times, ask user decide next
                                            {
                                                DialogResult ZESreadingWarn = MessageBox.Show("ZES() reading does't match it's script!", "Warning", MessageBoxButtons.AbortRetryIgnore);
                                                if (ZESreadingWarn == DialogResult.Abort) { StopFlag = true; break; }     
                                                if (ZESreadingWarn == DialogResult.Retry) { RetryCount = 0; continue; }
                                                if (ZESreadingWarn == DialogResult.Ignore) { break; }
                                            }
                                            else // Retry read <5 times, try again
                                            {
                                                await Task.Delay(500);
                                                ZES_ZIMMER.ZES_Send_Read(CMDstr);
                                                tokensCMD = CMDstr.Split(';');
                                                tokens = ZES_ZIMMER.ZESResponseData.Split(';');
                                                RetryCount++;
                                                continue;
                                            }
                                        }
                                    }

                                    int tokenread = 0;
                                    while (tokenread < tokens.Length)  //split read data, add into Linecontents
                                    {
                                        LineContents = LineContents + tokens[tokenread].Trim() + ",";
                                        ExecuteMsg.AppendText("PowerMeter: " + tokensCMD[tokenread] + " value read;\n");
                                        tokenread++;
                                    }
                                }
                                WalkRead++;
                                break;
                        }
                    }
                    ReadList.Add(LineContents);
                    ExecuteMsg.AppendText("\n"); // CR after end
                }
                LineNo++;
            }


            if (StopFlag == false)
            { ExecuteMsg.AppendText("____________Script Execution Finished____________" + '\n'); }
            else if (StopFlag == true)
            { ExecuteMsg.AppendText("____________User Stopped Script Execution____________" + '\n'); }
        }

        private void test2_Click(object sender, EventArgs e)
        {
            string ZES = "x,y,z,ZeS(:read:voltage:trms4?;:fetch:power:active4?;:fetch:variable?(9:9);:fetch:current:trms4?), LCR1,;";
            if (Regex.IsMatch(ZES, @"i*[ZES]\((.*)\)"))
            {
                MessageBox.Show("Found ZES Regex", "FOUND!!");
                //string resultstr = Regex.Match(ZES, @"(.*)(?<=\()(.*)(?=\))(.*)").Value;
                //MessageBox.Show(resultstr, "Extract result");

                //MessageBox.Show(resultstr.Replace("ZES(",""), "Replace result 1

                string pattern = @"i*[ZES]\(";
                Regex Rgx = new Regex(pattern);
                string replacement = "";
                
                ZES = Rgx.Replace(ZES, replacement);
                MessageBox.Show(ZES, "Replace result ZES( --> blank");

                ZES = ZES.Replace(';', ',');
                MessageBox.Show(ZES, "Replace result ;-->,");
            };

        }

        public void SaveRead()
        {
            OutList.Clear();
            int ReadListCurser = 0;
            //int Out_test_curser = 0;
            string CurrentTitle = "";
            OutList.Add("Scripting Version: "+ LogVer() + " " + UiVersion);
            OutList.Add(DateTime.Now.ToString());
            foreach (string ScriptlineIterator in CSVcontents)
            {
                try
                {
                    string Scriptline = ScriptlineIterator.ToUpper(); //Unify format for easy checking
                    if(Regex.IsMatch(ScriptlineIterator, @".*[LCRREADALL].*")) //Check if there's LCRmeasureall command, replace with 4 channel titles
                    {
                        Scriptline = Scriptline.Replace("LCRREADALL", " LCRALL_1, LCRALL_2, LCRALL_3, LCRALL_4");
                    }

                    if (Regex.IsMatch(ScriptlineIterator, @"i*[ZES]\((.*)\)")) //Check if line title has ZES(xx?;:xx?;:xx?(x:x))
                    {
                        Scriptline = Scriptline.Replace("ZES", "");
                        Scriptline = Scriptline.Replace(';', ',') + ',';
                    }

                    if (Scriptline.StartsWith("|") && (Scriptline != CurrentTitle))
                    {

                        CurrentTitle = Scriptline.ToUpper();
                        OutList.Add(CurrentTitle.Substring(2));  //skip |,
                                                                 //Console.WriteLine(OutList[Out_test_curser++]);
                        OutList.Add(ReadList[ReadListCurser++]);
                        //Console.WriteLine(OutList[Out_test_curser++]);
                        continue;
                    }
                    if (Scriptline.StartsWith("|") && (Scriptline == CurrentTitle))
                    {
                        OutList.Add(ReadList[ReadListCurser++]);
                        //Console.WriteLine(OutList[Out_test_curser++] + "tst");
                        continue;
                    }
                }
                catch (Exception ReadErr) { }
            }

            //Console.WriteLine("ReadList is: ");
            //int i = 0;
           // while (i < ReadList.Count)
            //{
            //    Console.WriteLine(ReadList[i]);
            //    i++;
            //}
        }
        #endregion

        public void UpdateMsg(string text)
        {
            TestLabel.Text = text;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            DialogResult StartConfirm = MessageBox.Show("Warning! If click YES, ALL previous records will be cleared. Continue?","Warning",MessageBoxButtons.YesNo);
            if(StartConfirm == DialogResult.No)
            { return; }
            //LoggingData.ReadScript();
            CSVcontents.Clear();
            ExecuteMsg.Clear();
            foreach (string line in Script.Lines)
            {
                line.Trim();
                line.Replace(" ", string.Empty);
                //var values = line.Split(';');

                //matches ,xx; pattern. prevent ZES ; in parentheses ()
                var values = Regex.Split(line,@",\s*;",RegexOptions.None);

                Int32 ct = 0;
                while (ct < (values.Length - 1))
                {
                    CSVcontents.Add(values[ct].Trim());  //sep same line code with ; separator
                    ct++;
                }
            }
            // Test print out read
            ExecuteMsg.AppendText("____________Executing Script____________\n");
            ExecuteMsg.AppendText("----Reading Script");
            int i = 0;
              while (i < CSVcontents.Count)
              {
                ExecuteMsg.AppendText(CSVcontents[i]+'\n');
                  i++;
              }
            //ExecuteMsg.AppendText("----Script read done");

            //ThreadStart ExeThread = new ThreadStart(ExecuteScript);
            //Thread ExecuteThread = new Thread(ExeThread);
            //ExecuteThread.Start();
            //Monitor.Wait(ExecuteThread);
            ExecuteScript();
        }


        private void test_Click(object sender, EventArgs e)
        {
            ThreadStart TestThread = new ThreadStart(ExecuteScript);
            Thread childThread = new Thread(TestThread);
            childThread.Start();
            //Monitor.Wait(childThread);
            //Monitor.Pulse(childThread);
            //childThread.Suspend();
            //childThread.Resume();
        }

        private void View_Click(object sender, EventArgs e)
        {
            ExecuteMsg.AppendText("--------View Result--------\n");
            SaveRead();
            int i = 0;
            while (i < OutList.Count)
            {
                ExecuteMsg.AppendText(OutList[i]+'\n');
                i++;
            }
            ExecuteMsg.AppendText("--------View Result End--------\n");
        }

        private void SaveResult_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            SFD.FilterIndex = 1;
            SFD.RestoreDirectory = true;

            if (SFD.ShowDialog() == DialogResult.OK)
            {               
                SaveRead();
                File.WriteAllLines(SFD.FileName,OutList);
                MessageBox.Show("Data save done");
            }
        }

        private void LoadScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();
            OF.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            OF.FilterIndex = 1;
            OF.RestoreDirectory = true;

            if (OF.ShowDialog() == DialogResult.OK)
            {
                string Filename = OF.FileName;
                // Empty past list first;
                Script.Clear();
                using (StreamReader CSV = new StreamReader(Filename))
                {
                    
                    while (!CSV.EndOfStream)
                    {
                        var line = CSV.ReadLine();
                        Script.AppendText(line+'\n');
                    }

                }
            }
        }

        private void SaveScript_Click(object sender, EventArgs e)
        {

        }

        private void AddinputMeth_Click(object sender, EventArgs e)
        {
            try { Script.AppendText(LinetypeList[Linetype.SelectedIndex]); }
            catch { MessageBox.Show("Please select a 'Method' from box","Opps",MessageBoxButtons.OK); }
        }

        private void AddinputCtrl_Click(object sender, EventArgs e)
        {
            try { Script.AppendText(ControlList[Control.SelectedIndex]); }
            catch { MessageBox.Show("Please select a 'Control' from box", "Opps", MessageBoxButtons.OK); }
        }

        private void AddinputRead_Click(object sender, EventArgs e)
        {
            try { Script.AppendText(CommandList[Command.SelectedIndex]); }
            catch { MessageBox.Show("Please select a 'Read' from above box", "Opps", MessageBoxButtons.OK); }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                GantryDll.GantryStop();
                GantryDll.PosRecali();
            }
            catch (Exception) { };

            LoggingForm.ActiveForm.Close();
        }

        private void StopScripting_Click(object sender, EventArgs e)
        {
            StopFlag = true;
        }
    }
}
