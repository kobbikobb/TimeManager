using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TimeManager.Container;
using TimeManager.Core.Repositories;
using TimeManager.NHibernate;
using TimeManager.Presentation.ViewModels;
using Application = System.Windows.Forms.Application;

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

            //Set the default format for wpf controls to be of the machine default language
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            using (var container = new ApplicationContainer())
            {
                var taskAutomation = container.ResolveTaskAutomation();

                var machineStatusTaskAutomation = new MachineStatusTaskAutomation(taskAutomation);
                var timeoutTaskAutomation = new TimeoutTaskAutomation(taskAutomation);
                machineStatusTaskAutomation.StartAutomation();
                timeoutTaskAutomation.StartAutomation();

                Application.Run(container.ResolveTray());
            }
        }
    }
}
