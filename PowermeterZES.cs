using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

namespace WindowsFormsApp1
{
    public class PowermeterZES
    {
        SerialPort ZEScomPort = new SerialPort();
        //public static NetworkStream ZESns;
        public String ZESResponseData;

        public string VerZes()
        {
            return "1.0";
        }

        public int ConnectPowerMeter(String ComNo)
        {
            try
            {
                ZEScomPort.PortName = ComNo;
                ZEScomPort.BaudRate = 38400;
                ZEScomPort.DataBits = 8;
                ZEScomPort.ReadTimeout = 400;
                ZEScomPort.WriteTimeout = 500;
                ZEScomPort.StopBits = StopBits.One;
                ZEScomPort.DtrEnable = true;
    
                ZEScomPort.Open();
                ZEScomPort.DiscardInBuffer();
                ZEScomPort.DiscardOutBuffer();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return 1;
            }
        }

        public void ZESclearbuffer()
        {
            ZEScomPort.DiscardOutBuffer();
            ZEScomPort.DiscardOutBuffer();
        }

        public int DisconnectPowerMeter()
        {
            try
            {
                ZEScomPort.DiscardInBuffer();
                ZEScomPort.DiscardOutBuffer();
                ZEScomPort.Close();

                if (ZEScomPort.IsOpen == false) { return 0; }
                else {return 1;}

            }
            catch (Exception err) { MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK); return 2; }
        }
       
        public void ZES_Send_Read(String strMSG)
        {
            try
            {
                ZESResponseData = string.Empty;

                ZEScomPort.DiscardOutBuffer();
                ZEScomPort.WriteLine(strMSG);
                //int milliseconds = 300;
                //Thread.Sleep(milliseconds);
                ZEScomPort.DiscardInBuffer();
                string DataIn;
                Int32 Timer = 0;
                while (true)
                {
                    if (Timer > 1000) { break; }
                    DataIn = ZEScomPort.ReadExisting();
                    if (DataIn.Length > 1 && DataIn != "\r")
                    {
                        ZESResponseData = DataIn;
                        ZESResponseData.Trim();
                        break;
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Timer += 100;
                    }
                }


            }
            catch (Exception ERR) { MessageBox.Show(ERR.Message, "Error"); }
        }

/*  Only valid for Datareceived Event use
    ZEScomPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            ZESResponseData = indata;
        }

        public void ZES_Send(String str)
        {
            try
            {
                ZESResponseData = string.Empty;

                ZEScomPort.WriteLine(str);
            }
            catch (Exception ERR) { MessageBox.Show(ERR.Message, "Error"); }
         }
*/
    }
}
