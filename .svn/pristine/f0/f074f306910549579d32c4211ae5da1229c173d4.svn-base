using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TimeManager.Windows
{
    public class SessionChangeHandler : Control // allows us to override WndProc
    {
        [DllImport("WtsApi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)]int dwFlags);
        
        [DllImport("WtsApi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        private const int NotifyForThisSession = 0;

        private const int WmWtssessionChange = 0x2b1;

        const int WtsSessionLogon = 0x5; // A user has logged on to the session.
        const int WtsSessionLogoff = 0x6; // A user has logged off the session.
        private const int WtsSessionLock = 0x7;
        private const int WtsSessionUnlock = 0x8;

        public event EventHandler MachineLocked;
        public event EventHandler MachineUnlocked;

        public SessionChangeHandler()
        {
            if (!WTSRegisterSessionNotification(this.Handle, NotifyForThisSession))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // unregister the handle before it gets destroyed
            WTSUnRegisterSessionNotification(this.Handle);
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmWtssessionChange)
            {
                int value = m.WParam.ToInt32();
                if (value == WtsSessionLock || value == WtsSessionLogoff)
                {
                    if (MachineLocked != null)
                        MachineLocked(this, EventArgs.Empty);
                }
                else if (value == WtsSessionUnlock || value == WtsSessionLogon)
                {
                    if(MachineUnlocked != null)
                        MachineUnlocked(this, EventArgs.Empty);
                }
            }
            base.WndProc(ref m);
        }
    }
}
