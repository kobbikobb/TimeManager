using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TimeManager.Properties;
using Application = System.Windows.Forms.Application;

namespace TimeManager
{
    public class Tray : ApplicationContext
    {
        private readonly NotifyIcon notifyIcon;

        public Tray(params ITrayAction[] trayActions)
        {
            notifyIcon = new NotifyIcon {Icon = Resources.clock, ContextMenuStrip = new ContextMenuStrip(), Visible = true};

            foreach (var trayAction in trayActions)
            {
                notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(trayAction.Name, null, (sender, args) => trayAction.Execute()));
            }

            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Quit", null, Quit));
        }

        private void Quit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
