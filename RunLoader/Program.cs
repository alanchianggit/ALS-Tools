﻿using System;
using System.Windows.Forms;


namespace ALSTools
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

    //public class ProgramSettings
    //{
    //    public static string DbPath = Settings.Default.DbPath.ToString();
    //}
}
