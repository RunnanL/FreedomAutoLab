using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GantryDllCs;

namespace WindowsFormsApp1
{
    public partial class GantryCalibration : Form
    {
        public Gantry GantryDll;
        public bool CalibrationMode_b = false;
        public GantryCalibration(Gantry GantryDll)
        {
            InitializeComponent();
            this.GantryDll = GantryDll;
        }

        private enum PositionMode { RelativeCalibrationMode = 1, AbsoluteMode = 2 };

        private void SendCmd_btn_Click(object sender, EventArgs e)
        {
            ConsoleDialogue_TextBox.AppendText("Sending command: " + GantryCmd_tb.Text + '\n');
            try
            {
                int Var = 0;
                Var = GantryDll.Com_W_R(GantryCmd_tb.Text);
                if (1 == Var)
                {
                    ConsoleDialogue_TextBox.AppendText("Gantry response: " + GantryDll.ReadBuffer + '\n');
                }
                if(0 == Var)
                {
                    ConsoleDialogue_TextBox.AppendText("Timeout. Gantry no response. Please check Gantry connection.\n");
                }
            }
            catch { MessageBox.Show("Cannot send command. Please check Gantry connection.", "Error", MessageBoxButtons.OK); }
        }

        private void CalibrationOn()
        {
            try
            {
                int Var = GantryDll.RelativeModeXYZ();
                if (0 == Var)
                {
                    PositionMode ValueX = (PositionMode)GantryDll.States.XPositioningMode;
                    PositionMode ValueY = (PositionMode)GantryDll.States.YPositioningMode;
                    PositionMode ValueZ = (PositionMode)GantryDll.States.ZPositioningMode;
                    MessageBox.Show("One or more motors CANNOT ENABLE Relative Movement (Direct Movement) Mode!\n" + "X motor mode: "
                      + ValueX + "\nY motor mode: " + ValueY + "\nZ motor mode: "
                      + ValueZ, "Error", MessageBoxButtons.OK); return;
                }
                if (1 == Var)
                {
                    GantryDll.PosRecali();
                    DirectMove_gb.Enabled = true; //Enable calibration buttons
                    CalibrateHere_gb.Enabled = true;
                    CalibrationMode_b = true;
                    En_btn.Text = "Disable"; //Change button text to disable
                    ConsoleDialogue_TextBox.AppendText("Start Calibration. WARNING: Gantry is in RELATIVE MODE (Direct Movement by Offset)\n");
                }
            }
            catch (Exception Err) { MessageBox.Show("CANNOT ENABLE calibration mode\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void CalibrationOff()
        {
            try
            {
                int Var = GantryDll.AbsoluteModeXYZ();
                if (0 == Var)
                {
                    PositionMode ValueX = (PositionMode)GantryDll.States.XPositioningMode;
                    PositionMode ValueY = (PositionMode)GantryDll.States.YPositioningMode;
                    PositionMode ValueZ = (PositionMode)GantryDll.States.ZPositioningMode;
                    MessageBox.Show("One or more motors CANNOT DISABLE Calibration Mode!\n" + "X motor mode: "
                      + ValueX + "\nY motor mode: " + ValueY + "\nZ motor mode: "
                      + ValueZ, "Error", MessageBoxButtons.OK); return;
                }
                if (1 == Var)
                {
                    DirectMove_gb.Enabled = false;
                    CalibrateHere_gb.Enabled = false;
                    CalibrationMode_b = false;
                    En_btn.Text = "Enable";
                    ConsoleDialogue_TextBox.AppendText("Calibration mode disabled! Gantry in ABSOLUTE MODE (Coordinate Movement).\n");
                    MessageBox.Show("Calibration mode disabled! Gantry in ABSOLUTE MODE (Coordinate Movement).", "Calibration Mode Turned Off");
                }
            }
            catch (Exception Err) { MessageBox.Show("CANNOT DISABLE calibration mode\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void En_btn_Click(object sender, EventArgs e)
        {
            if(false == CalibrationMode_b) //turn mode from Absolute to Relative(Direct)
            {
                CalibrationOn();
            }
            else if(true == CalibrationMode_b) //turn mode from Relative(Direct) to Absolulte
            {
                CalibrationOff();
            }
        }

        private void GantryCalibration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (true == CalibrationMode_b)
            {
                DialogResult DiagResult = MessageBox.Show("Calibration Mode still On, do you want to exit?", "Warning", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == DiagResult) { CalibrationOff(); };
                if (DialogResult.No == DiagResult) { e.Cancel=true; };
            }
        }

        private void MovPlusX_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeX(Convert.ToDouble(MovPlusX_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.","Error",MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Offseting towards +X: " + MovPlusX_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void MovMinusX_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeX(Convert.ToDouble('-' + MovMinusX_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.", "Error", MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Offseting towards -X: " + MovMinusX_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void MovPlusY_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeY(Convert.ToDouble(MovPlusY_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.", "Error", MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Offseting towards +Y: " + MovPlusY_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void MovMinusY_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeY(Convert.ToDouble('-' + MovMinusY_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.", "Error", MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Offseting towards -Y: " + MovMinusY_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void MovPlusZ_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeZ(Convert.ToDouble(MovPlusZ_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.", "Error", MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Raising Z: " + MovPlusZ_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void MovMinusZ_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.MoveRelativeZ(Convert.ToDouble('-' + MovMinusZ_tb.Text));
                if (0 == Code) { MessageBox.Show("Cannot move gantry, please try again.", "Error", MessageBoxButtons.OK); return; }
                ConsoleDialogue_TextBox.AppendText("Lowering Z: " + MovMinusZ_tb.Text + "mm\n");
            }
            catch (Exception Err) { MessageBox.Show("Canot move gantry\n\n" + Err, "Error", MessageBoxButtons.OK); }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            try
            {
                GantryDll.GantryStop();
                ConsoleDialogue_TextBox.AppendText("Gantry stopped\n");
                MessageBox.Show("Gantry stopped","Message");
            }
            catch { MessageBox.Show("CANNOT stop Gantry, please try again", "ERROR"); }
        }

        private void CalibrateHere_Click(object sender, EventArgs e)
        {
            try
            {
                int Code = 0;
                Code = GantryDll.Calibrate(Convert.ToDouble(CaliX_tb.Text), Convert.ToDouble(CaliY_tb.Text), Convert.ToDouble(CaliZ_tb.Text));
                if (0 == Code) { MessageBox.Show("One or more motor(s) NOT been calibrated, please try again", "Incomplete motor calibration"); return; }
                MessageBox.Show("Calibration Succeeded!", "Succeed");
                ConsoleDialogue_TextBox.AppendText("Calibration Succeeded!\n");
                CalibrationOff();
            }
            catch { MessageBox.Show("CANNOT calibrate, please try again", "ERROR"); }
        }
    }
}
