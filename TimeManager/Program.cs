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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var timeManagerRepository = new TimeManagerRepositoryFake();

                DependencyResolver.Register(timeManagerRepository);

                var startTaskAction = new StartTaskAction();
                var viewWorkbookAction = new ViewWorkbookAction();
                var tray = new Tray(startTaskAction, viewWorkbookAction);

                var taskAutomation = new TaskAutomation(timeManagerRepository, startTaskAction);
                var machineStatusTaskAutomation = new MachineStatusTaskAutomation(taskAutomation);
                var timeoutTaskAutomation = new TimeoutTaskAutomation(taskAutomation);

                machineStatusTaskAutomation.StartAutomation();
                timeoutTaskAutomation.StartAutomation();
                Application.Run(tray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
