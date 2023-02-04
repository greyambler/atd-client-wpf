using ShMI.ClientMain.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcService : ModuleMainWindow
    {
        public ModuleUcService(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables()
        {
            FirstFullServiceString();
        }

        public ModuleMainWindow ModuleMain { get; set; }
        public UserControl Uc { get; }

        #region IListButtonsService

        //Установить
        public override void AddItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, "\nУстановить сервис АТД?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                Install_Service();
            }
            InitTables();
            (WorkGrid.Children[0] as UcService).InitButtons();
        }

        //Тест1
        public override void EditItem()
        {
            _ = MessageBox.Show("Тест1 EditItem");
        }

        //Удалить
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, "\nУдалить сервис АТД?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                UnInstall_Service();
            }
            InitTables();
            (WorkGrid.Children[0] as UcService).InitButtons();
        }

        //Рестарт сервис
        public override void UtilItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, "\nПерестартовать сервис АТД?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                ReStartService();
            }
        }

        #endregion IListButtonsService


        public void FirstFullServiceString()
        {
            try
            {
                FullServiceString = "Сервис не установлен!!!";
                if (IsExist_Servise())
                {
                    Microsoft.Win32.RegistryKey myRegKey = Microsoft.Win32.Registry.LocalMachine;
                    myRegKey = myRegKey.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{ServiceName}");
                    string[] Param = myRegKey.GetValueNames();
                    string strPath = Param.FirstOrDefault(s => s == "ImagePath");
                    FullServiceString = (strPath != null) ? myRegKey.GetValue(strPath).ToString() : FullServiceString;
                }
            }
            catch (Exception er)
            {
                //string msg = string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
                //_ = new WindDialog(msg).ShowDialog();
            }
        }
        private void Install_Service()
        {
            try
            {
                string fileService = "ShMI.WSClient.exe";
                if (!IsExist_Servise() && File.Exists(fileService))
                {
                    string scCommand = "sc.exe";
                    string create = string.Format("create {0} start= {1} binPath= {2}{3}", ServiceName, "auto", Base_Dir, fileService);
                    string description = string.Format("description {0} \"{1}\"", ServiceName, BaseMain.Properties.Resources.Description);
                    string start = $"start {ServiceName}";

                    System.Diagnostics.Process rCreate = System.Diagnostics.Process.Start(scCommand, create);
                    rCreate.WaitForExit();
                    System.Diagnostics.Process rDescription = System.Diagnostics.Process.Start(scCommand, description);
                    rCreate.WaitForExit();
                    System.Diagnostics.Process rStart = System.Diagnostics.Process.Start(scCommand, start);
                    rStart.WaitForExit();
                }
            }
            catch (Exception er)
            {
                //string msg = string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
                //_ = new WindDialog(msg).ShowDialog();
            }
        }
        public void UnInstall_Service()
        {
            if (IsExist_Servise())
            {
                string scCommand = "sc.exe";
                if (IsExist_Servise())
                {
                    string stop = $"stop {ServiceName}";
                    string delete = $"delete {ServiceName}";

                    System.Diagnostics.Process rStop = System.Diagnostics.Process.Start(scCommand, stop);
                    rStop.WaitForExit();
                    System.Diagnostics.Process rDelete = System.Diagnostics.Process.Start(scCommand, delete);
                    rDelete.WaitForExit();
                }
            }
        }

        public string ServiceName => BaseMain.Properties.Resources.ServiceName;

        private string _FullServiceString;
        public string FullServiceString
        {
            get => _FullServiceString;
            set
            {
                _FullServiceString = value;
                MChangeProperty = "FullServiceString";
            }
        }


    }
}
