using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    class LCRmeter
    {
        public static System.Net.Sockets.TcpClient LCRsocket;
        public static NetworkStream ns;
        public String ResponseData;

        public string VerLcr()
        {
            return "1.0.1";
        }

        public int ConnectLCR(String ip, Int32 port)
        {
            try
            {
                LCRsocket = new System.Net.Sockets.TcpClient();
                LCRsocket.Connect(ip, port);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return 1;
            }
        }

        public void DisconnectLCR()
        {
            try
            {
                ns.Close();
                LCRsocket.Close();
            }
            catch (Exception err) { MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK); }
        }

        public void LCRwrite_read(String strMsg)
        {
            if (true == LCRsocket.Connected)
            {
                try
                {
                    ResponseData = String.Empty;
                    strMsg = strMsg + "\r\n";
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(strMsg);
                    ns = LCRsocket.GetStream();
                    ns.ReadTimeout = 900;
                    ns.Write(data, 0, data.Length);

                    int milliseconds = 50;
                    Thread.Sleep(milliseconds);
                    Byte[] DAT = new Byte[1024];

                    if (ns.DataAvailable)
                    {
                        Int32 bytes = ns.Read(DAT, 0, DAT.Length);
                        ResponseData = System.Text.Encoding.ASCII.GetString(DAT, 0, bytes);
                        ResponseData.Trim();
                    }
                    else
                    {
                        milliseconds = 500;
                        Thread.Sleep(milliseconds);
                        // await Task.Delay(milliseconds);
                        if (ns.DataAvailable)
                        {
                            Int32 bytes = ns.Read(DAT, 0, DAT.Length);
                            ResponseData = System.Text.Encoding.ASCII.GetString(DAT, 0, bytes);
                            ResponseData.Trim();
                        }
                    }

                }
                catch (Exception ERR) { MessageBox.Show(ERR.Message, "Error"); }
            }
        }

        public void LCR_Param_Change(int BoxNo, string ChangeTo)
        {
            String cmd;
            cmd = ":PARameter" + BoxNo + " " + ChangeTo;
            LCRwrite_read(cmd);
        }

        public void LCR_Setting_Change(String ChangeTo)
        {
            String cmd = ":FREQuency " + ChangeTo;
            LCRwrite_read(cmd);
            cmd = ":FREQuency?";
            LCRwrite_read(cmd);
        }

        public int Measure()
        {
            try
            {
                LCRwrite_read(":MEASure?");

                int Counter_LCR_retry = 0;
                string[] tokens = ResponseData.Split(',');
                while (4 != tokens.Length)
                {
                    if (Counter_LCR_retry > 49) //retry if read data not showing LCR1/LCR2/LCR3/LCR4 four data. Limit try 50 times
                    { break; }
                    else
                    {
                        LCRwrite_read(":MEASure?");
                        tokens = ResponseData.Split(',');
                        Counter_LCR_retry++;
                    }
                }
                return 1;
            }
            catch { return 0; }
        }
    }
}
