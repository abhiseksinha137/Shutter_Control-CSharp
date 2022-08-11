using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutter_Control_CSharp
{
    public partial class settingsPage : Form
    {
        public settingsPage()
        {
            InitializeComponent();
        }

        private void settingsPage_Load(object sender, EventArgs e)
        {
            txtbxOpen.Text = Properties.Settings.Default.Open;
            txtbxClose.Text = Properties.Settings.Default.Close;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Open = txtbxOpen.Text;
            Properties.Settings.Default.Close=txtbxClose.Text;
            Properties.Settings.Default.Save();
        }


    }
}
