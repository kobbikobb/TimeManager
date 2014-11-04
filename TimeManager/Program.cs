using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PatternLib;
using TimeManager.Properties;
using TimeManagerLib.Data;
using TimeManagerLib.Helpers;
using TimeManagerLib.Model;
using TimeManagerLib.View;
using TimeManagerLib.ViewModel;
using TimeManagerLib.ViewModel.Extension;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TimeManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                var repository = new TimeManagerRepositoryFake();

                DependencyResolver.Register(repository);
  
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Tray());
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteException(ex);
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
