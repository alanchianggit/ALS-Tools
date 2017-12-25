using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;


namespace RunLoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-main":
                        Application.Run(new frm_MainMenu());
                        break;
                    case "-files":
                        Application.Run(new Analysis_Management());
                        break;
                    case "-analysis":
                        Application.Run(new Analysis_Management());
                        break;
                    case "-archiver":
                        Application.Run(new Archiver.ArchiverForm());
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                Application.Run(new frm_MainMenu());
            }
            
        }
    }
}
