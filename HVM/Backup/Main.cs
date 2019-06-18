using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HVM
{
    public partial class Main : Form
    {
   
        public Main()
        {
            InitializeComponent();
        }

        private void btnSpeedDetectionForm_Click(object sender, EventArgs e)
        {
            //this.Hide();
            SpeedDetection sd = new SpeedDetection();
            sd.Show();
        }

        private void btnVehicleIdentificationForm_Click(object sender, EventArgs e)
        {
            //this.Hide();
            VehicleIdentification vi = new VehicleIdentification();
            vi.Show();
        }

        private void btnNumberPlateRecognitionForm_Click(object sender, EventArgs e)
        {
            //this.Hide();
            NumberPlateRecognition npr = new NumberPlateRecognition();
            npr.Show();
        }

        private void btnVehicleMonitoringForm_Click(object sender, EventArgs e)
        {
            //this.Hide();
            txtDate vm = new txtDate();
            vm.Show();
        }

        private void btnEmailSystem_Click(object sender, EventArgs e)
        {
            EmailSystem es = new EmailSystem();
            es.Show();
        }

        private void btnHistoryRecords_Click(object sender, EventArgs e)
        {
            HistoryRecords hr = new HistoryRecords();
            hr.Show();
        }

        private void btnOwnerHistory_Click(object sender, EventArgs e)
        {
            OwnerHistory oh = new OwnerHistory();
            oh.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SystemSettings ss = new SystemSettings();
            ss.Show();
        }
    }
}
