using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Shutter_Control_CSharp;

namespace stageControl_CSharp
{
    public partial class Form1 : Form
    {
        private SerialPort com;
        private String shutterStatus = "Closed";
        private bool statusChanged = false;
        
        public Form1()
        {
            InitializeComponent();

        }

        private void sendSerial(string command)
        {
            try
            {
                com.WriteLine(command);
                statusChanged = true;
            }
            catch (Exception ex)
            {
                statusChanged = false;
                //writeLog(ex.Message);
            }
        }
        //private void writeLog(string log)
        //{
        //    txtbxLog.Text = txtbxLog.Text + log + Environment.NewLine;
        //}

        private void connectCOM()
        {
            try
            {
                com = new SerialPort(comboBox1.Text, 9600, Parity.None, 8, StopBits.One);
                com.Open();
                btnDiscon.Enabled = true;
                //writeLog("Stage Connected!");
                pnlIndicator.BackColor = Color.Lime;
            }
            catch (Exception ex)
            {
                //writeLog("Stage not found! " + ex.Message);
            }
        }

        private void closeCOM()
        {
            try
            {
                com.Close();
                btnDiscon.Enabled = false;
                //writeLog("Stage Disconnected!");
                pnlIndicator.BackColor = Color.Gray;
            }
            catch (Exception ex)
            {
                //writeLog(ex.Message);
            }
        }

        private void openShutter()
        {
            statusChanged = false;
            sendSerial(Shutter_Control_CSharp.Properties.Settings.Default.Open);
            if (statusChanged){
                shutterStatus = "Open";
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = shutterStatus;
            }
        }
        private void closeShutter()
        {
            statusChanged = false;
            sendSerial(Shutter_Control_CSharp.Properties.Settings.Default.Close);
            if (statusChanged){
                shutterStatus = "Closed";
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = shutterStatus;
            }
            
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            closeCOM();
            connectCOM();
        }


        private void btnDiscon_Click(object sender, EventArgs e)
        {
            closeCOM();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeCOM();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void btnOpen_MouseClick(object sender, MouseEventArgs e)
        {
            openShutter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            closeShutter();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            settingsPage page = new settingsPage();
            page.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }
    }
}
