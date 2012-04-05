using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TimeManagerLib.Helpers
{
    public class Logger
    {
        private static Logger _instance;

        private Logger()
        {
            
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }

        public void WriteException(Exception ex)
        {
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TimeManager";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (var writer = new StreamWriter(directoryPath + @"\Errors_" + DateTime.Today.ToString("yyyyMMdd") + ".log", true, Encoding.Default))
            {
                writer.WriteLine(DateTime.Now + ": " + ex.Message);
                writer.WriteLine(ex.ToString());
            }
        }

        public void WriteMessage(string message)
        {
            using (var writer = new StreamWriter("TimeManager_" + DateTime.Today.ToString("yyyyMMdd") + ".log", true, Encoding.Default))
            {
                writer.WriteLine(DateTime.Now + ": " + message);
            }
        }
    }
}
