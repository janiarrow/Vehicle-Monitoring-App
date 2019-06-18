using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HVM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SpeedDetection());
            try
            {
                Application.Run(new Main());
            }
            catch (Exception ex1)
            {
              //  MessageBox.Show(ex1.Message);
            }
        }
    }
}
