using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelperLib;
using RaspberryPiClient.Forms;

namespace RaspberryPiClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NDTest());

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Config.ReadConfig();
                using (Form form = new Map())
                {
                    form.FormClosed += delegate (object sender, FormClosedEventArgs e)
                    {
                        Config.SaveConfig();
                        Application.Exit();
                    };
                    Application.Run(form);
                }
            }
            catch (Exception ex)
            {
                Config.SaveConfig();
                FileHelper.Write(new string[] { "ErrorLog.txt" }, ex.ToString());
            }
        }
    }
}
