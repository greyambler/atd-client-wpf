using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace ShMI.WSClient
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            while (true)
            {
                File.AppendAllText(@"D:\MyProjects\C#\text.txt", "Привет!!\n");
                await Task.Delay(3000);

                AddLog("Привет");
            }

        }

        protected override void OnStop()
        {
            AddLog("OnStop");

            //_ = ModuleCore.Do(s => s.Stop_Testing());
            //AddLog(string.Format("ATD_SSClient stop Ver {0}", Module_Core.NumberBuild_Stat));
        }

        private void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists(BaseMain.Properties.Resources.ServiceName))
                {
                    EventLog.CreateEventSource(BaseMain.Properties.Resources.ServiceName, BaseMain.Properties.Resources.JournalAtd);
                }
                eventLog1.Source = BaseMain.Properties.Resources.ServiceName;
                log = string.Format("{0:dd}/{0:MM}/{0:yyyy} {0:HH}:{0:mm}:{0:ss}.{0:fff}\n\t{1}", DateTime.Now, log);
                eventLog1.WriteEntry(log);
            }
            catch { }
        }

    }
}
