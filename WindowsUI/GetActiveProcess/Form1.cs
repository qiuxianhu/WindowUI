using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GetActiveProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void Form1_Load(object sender, EventArgs e)
        {            
            var timerInteractionAppCheck = new Timer { Interval = 500, Enabled = true };
            Process process = null;
            timerInteractionAppCheck.Tick += (sender1, e1) =>
            {
                string processName = ActiveProcessHelper.GetActiveProcessName(out process);
                
                switch (processName)
                {                    
                    case "Photoshop":
                        break;
                    case "ApplicationFrameHost":
                        process = ActiveProcessHelper.GetRealProcess(process);
                        if (process!=null&& process.ProcessName!=null&& process.ProcessName=="XD")
                        {

                        }                        
                        break;
                    default:

                        break;
                }
            };
        }       
    }
}
