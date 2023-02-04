using ShMI.BaseMain;
using ShMI.ClientMain.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    // Родительский класс для UC классов
    public class ModuleMainWindow : ModuleBaseMain, IMainWindow, IListButtonsService
    {
        public ModuleMainWindow()
        {

        }
        public ModuleMainWindow(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
        {
            this.ShellWindow = ShellWindow;
            this.WorkGrid = WorkGrid;
            this.ResourcesDict = ResourcesDict;
            this.IsAdmin = IsAdmin;
            this.DispatcherShell = DispatcherCore;
        }

        #region Инициализация DB ATD

        public bool IsExistObject()
        {
            bool isExistObject = false;
            using (EntitiesDb db = GetDb)
            {
                //isExistTH = db.NCassas.FirstOrDefault(s => s.Name.StartsWith("TH")).ThisNotNull();
                isExistObject = db.NObjects.FirstOrDefault().ThisNotNull();
            }
            return isExistObject;
        }

        public NCassa GetFirstTH()
        {
            NCassa nCassa = null;
            using (EntitiesDb db = GetDb)
            {
                nCassa = db.NCassas.FirstOrDefault(s => s.Name.StartsWith("TH"));
            }
            return nCassa;
        }

        public void InitFirstDB()
        {
            try
            {
                using (EntitiesDb db = GetDb)
                {
                    if (!IsExistObject())
                    {
                        db.SaveNCassa(new NCassa("th"));
                    }
                }
                GetSaveDataFromTH(GetFirstTH());

                WindDialog d = new WindDialog(WindDialog.DialogType.Message, $"\nИнициализация приложения прошла успешно.\n", _FontSize: 16);
                _ = d.ShowDialog();
            }
            catch (Exception er)
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Error, $"\nОшибка инициализация приложения.\n{er.Message}", _FontSize: 16);
                _ = d.ShowDialog();
            }
        }

        #endregion Инициализация DB ATD

        // Ширина панели кнопок

        private double widthListButton = 180;
        public double WidthListButton
        {
            get => widthListButton;
            set
            {
                widthListButton = value;
                MChangeProperty = "WidthListButton";
            }
        }

        // Показывает/скрывает кнопки в UcListButtons - текущая строка в рабочей таблице
        // CurrentItem

        private Visibility isExistItem = Visibility.Collapsed;
        public Visibility IsExistItemMain
        {
            get => isExistItem;
            set
            {
                isExistItem = value;
                MChangeProperty = "IsExistItemMain";
            }
        }

        public void DispatcherShellWork(UserControl Spinner, System.Windows.Visibility visibility)
        {
            _ = DispatcherShell.BeginInvoke((Action)(() =>
            {
                _ = Spinner.Do(s => s.Visibility = visibility);
            }));
        }

        public string GetChooseFile(string HeadText, string SetFirstCatalog = "")
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog
                {
                    Title = HeadText
                };
                if (SetFirstCatalog != "")
                {
                    fd.InitialDirectory = SetFirstCatalog;
                }
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return fd.FileName;
                }
            }
            catch (Exception er)
            {
                //MethodBase.GetCurrentMethod().Name.SetLogServiceError(this, er);

                //string msg = string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
                //_ = new WindDialog(msg, _FontSize: 16, IsError: true).ShowDialog();
            }
            return "";
        }

        // Поток текщего времени.

        #region Date_Time

        private string date_Time = "Date_Time";
        public string Date_Time
        {
            get => date_Time;
            set
            {
                date_Time = value;
                MChangeProperty = "Date_Time";
            }
        }

        private string time_Time = "Time_Time";
        public string Time_Time
        {
            get => time_Time;
            set
            {
                time_Time = value;
                MChangeProperty = "Time_Time";
            }
        }

        private System.Windows.Threading.DispatcherTimer timer;
        public void StartTime()
        {
            timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true,
            };
            timer.Tick += (o, t) =>
            {
                Date_Time = string.Format("{0:dd.MM.yyyy}", DateTime.Now);
                Time_Time = string.Format("{0:HH:mm:ss}", DateTime.Now);
            };
            timer.Start();
        }
        public void StopTime()
        {
            timer?.Stop();
        }

        #endregion Date_Time

        // Реализация интерфейса IMainWindow
        // ModuleMain         - Реализация Работа на уровне окна приложения
        // WorkGrid           - Реализация Рабочий грид для замены текущего контролла
        // ResourcesDict      - Реализация Ресурс тем приложения
        // SetWidthListButton - Реализация Метод установки ширины панели кнопок
        // DispatcherShell    - Предоставляет службы для управления очередью рабочих элементов для потока.
        #region IMainWindow

        public Window ShellWindow { get; }
        public Grid WorkGrid { get; }
        public ResourceDictionary ResourcesDict { get; }
        public void SetWidthListButton(UserControl _UserControl = null, bool visual = true)
        {
            WidthListButton = !visual ? 0 : 180;
            if (WorkGrid.ThisNotNull() && _UserControl.ThisNotNull())
            {
                WorkGrid.Children.Clear();
                _ = WorkGrid.Children.Add(_UserControl);
            }
        }
        public Dispatcher DispatcherShell { get; set; }

        // Св-во определяет работу администратора те показывает скрытые св-ва кнопки и тд.
        private bool isAdmin = true;
        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                MChangeProperty = "IsAdmin";
            }
        }

        #endregion IMainWindow

        // Реализация виртуальная методов панели кнопок.

        #region IListButtonsService

        public virtual void AddItem() { }
        public virtual void EditItem() { }
        public virtual void DeleteItem() { }
        public virtual void SaveItem() { }
        public virtual void CancelItem() { }
        public virtual void UtilItem() { }
        public virtual void Cancel() { }

        #endregion IListButtonsService

        // Общие объекты для главных окон UC.
        #region ObservableCollection 

        #region TestTable

        public void GetTestTable()
        {
            try
            {
                using (EntitiesDb db = GetDb)
                {
                    listTestTable = new ObservableCollection<TestTable>();
                    foreach (TestTable item in db.TestTables.ToList())
                    {
                        listTestTable.Add(item);
                    }
                    ListTestTable = listTestTable;
                }

            }
            catch (Exception er)
            {
            }
        }
        private ObservableCollection<TestTable> listTestTable;
        public ObservableCollection<TestTable> ListTestTable
        {
            get => listTestTable;
            set
            {
                listTestTable = value;
                MChangeProperty = "ListTestTable";
            }
        }

        #endregion TestTable

        #region NObject

        public void GetRowsNObject(string IdNObject = "")
        {
            try
            {
                using (EntitiesDb db = GetDb)
                {
                    listNObject = new ObservableCollection<NObject>();

                    _ = Guid.TryParse(IdNObject, out Guid idObj);

                    List<NObject> listLocal = idObj != Guid.Empty ? db.NObjects.Where(s => s.Id == idObj).ToList() : db.NObjects.ToList();

                    foreach (NObject item in listLocal)
                    {
                        listNObject.Add(item);
                    }
                    ListNObject = listNObject;
                }
            }
            catch (Exception er)
            {
            }
        }
        private ObservableCollection<NObject> listNObject;
        public ObservableCollection<NObject> ListNObject
        {
            get => listNObject;
            set
            {
                listNObject = value;
                MChangeProperty = "ListNObject";
            }
        }

        #endregion NObject

        #region ReCodesTable

        public void GetReCodesTable()
        {
            try
            {
                using (EntitiesDb db = GetDb)
                {
                    listReCodesTable = new ObservableCollection<ReCodesTable>();
                    foreach (ReCodesTable item in db.ReCodesTables.ToList())
                    {
                        listReCodesTable.Add(item);
                    }
                    ListReCodesTable = listReCodesTable;
                }
            }
            catch (Exception er)
            {
            }
        }
        private ObservableCollection<ReCodesTable> listReCodesTable;
        public ObservableCollection<ReCodesTable> ListReCodesTable
        {
            get => listReCodesTable;
            set
            {
                listReCodesTable = value;
                MChangeProperty = "ListReCodesTable";
            }
        }

        #endregion ReCodesTable

        #region NCassa

        public void GetNCassa(string nObjectId = "")
        {
            using (EntitiesDb db = GetDb)
            {
                listNCassa = new ObservableCollection<NCassa>();

                _ = Guid.TryParse(nObjectId, out Guid idObj);

                List<NCassa> listLocal = idObj != Guid.Empty ? db.NCassas.Where(s => s.NObjectId == idObj).ToList() : db.NCassas.ToList();

                listTH = new ObservableCollection<NCassa>();

                foreach (NCassa item in listLocal)
                {
                    if (item.Name.Contains("CASSA"))
                    {
                        listNCassa.Add(item);
                    }
                    else
                    {
                        listTH.Add(item);
                    }
                }
                ListNCassa = listNCassa;
                ListTH = listTH;
            }
        }
        public void GetOnlyNCassa(string nObjectId = "")
        {
            using (EntitiesDb db = GetDb)
            {
                listNCassa = new ObservableCollection<NCassa>();

                _ = Guid.TryParse(nObjectId, out Guid idObj);

                List<NCassa> listLocal = idObj != Guid.Empty ? db.NCassas.Where(s => s.NObjectId == idObj).ToList() : db.NCassas.ToList();

                foreach (NCassa item in listLocal)
                {
                    if (item.Name.Contains("CASSA"))
                    {
                        listNCassa.Add(item);
                    }
                }
                ListNCassa = listNCassa;
            }
        }
        private ObservableCollection<NCassa> listNCassa;
        public ObservableCollection<NCassa> ListNCassa
        {
            get => listNCassa;
            set
            {
                listNCassa = value;
                MChangeProperty = "ListNCassa";
            }
        }
        private ObservableCollection<NCassa> listTH;
        public ObservableCollection<NCassa> ListTH
        {
            get => listTH;
            set
            {
                listTH = value;
                MChangeProperty = "ListTH";
            }
        }

        #endregion NCassa

        #region NTank

        public void GetNTank(string IdNObject = "")
        {
            using (EntitiesDb db = GetDb)
            {
                listNTank = new ObservableCollection<NTank>();

                _ = Guid.TryParse(IdNObject, out Guid idObj);

                List<NTank> listLocal = new List<NTank>();

                if (idObj == Guid.Empty)
                {
                    listLocal = db.NTanks.ToList();
                }
                else
                {


                    listLocal = db.NTanks.Include("NStruna").Where(s => s.NStruna.NObjectId == idObj).ToList();

                    //List<NStruna> listStruna = db.NStrunas.Where(s => s.NObjectId == idObj).ToList();

                    //foreach (NStruna struna in listStruna)
                    //{
                    //    List<NTank> tankList = db.NTanks.Where(s => s.NStrunaId == struna.Id).ToList();

                    //    foreach (NTank item in tankList)
                    //    {
                    //        if (listLocal.FirstOrDefault(s => s.Id == item.Id) == null)
                    //        {
                    //            listLocal.Add(item);
                    //        }
                    //    }
                    //}
                }

                foreach (NTank item in listLocal)
                {
                    listNTank.Add(item);
                }
                ListNTank = listNTank;
            }
        }
        private ObservableCollection<NTank> listNTank;
        public ObservableCollection<NTank> ListNTank
        {
            get => listNTank;
            set
            {
                listNTank = value;
                MChangeProperty = "ListNTank";
            }
        }

        #endregion NTank

        #region NStruna

        public void GetNStruna(string nObjectId = "")
        {
            using (EntitiesDb db = GetDb)
            {
                listNStruna = new ObservableCollection<NStruna>();

                _ = Guid.TryParse(nObjectId, out Guid idObj);

                List<NStruna> listLocal = idObj != Guid.Empty ? db.NStrunas.Where(s => s.NObjectId == idObj).ToList() : db.NStrunas.ToList();

                foreach (NStruna item in listLocal)
                {
                    listNStruna.Add(item);
                }
                ListNStruna = listNStruna;
            }
        }
        private ObservableCollection<NStruna> listNStruna;
        public ObservableCollection<NStruna> ListNStruna
        {
            get => listNStruna;
            set
            {
                listNStruna = value;
                MChangeProperty = "ListNStruna";
            }
        }

        public List<string> listTypeLevel = new List<string>() { "NEW_servise", "struna", /*"veederoot", */ "tlg", "DOMS" };
        public List<string> ListTypeLevel
        {
            get => listTypeLevel;
            set => MChangeProperty = "ListTypeLevel";
        }

        #endregion NStruna

        #region Task_Device

        public void GetRowsTask_Device()//Task_Device task_Device = null)
        {
            using (EntitiesDb db = GetDb)
            {
                listTask_Device = new ObservableCollection<Task_Device>();
                Guid idObj = Guid.Empty;

                //if (task_Device.ThisNotNull())
                //{
                //    _ = Guid.TryParse(task_Device.NObjectId.ToString(), out idObj);
                //}

                List<Task_Device> listLocal = idObj != Guid.Empty
                    ? db.Task_Device.Where(s => s.NObjectId == idObj).ToList()
                    : db.Task_Device.ToList();


                foreach (Task_Device item in listLocal)
                {
                    item.NObject = db.NObjects.FirstOrDefault(s => s.Id == item.NObjectId);
                    item.InitListType_Device();
                    listTask_Device.Add(item);
                }

                ListTask_Device = listTask_Device;
            }
        }
        private ObservableCollection<Task_Device> listTask_Device;
        public ObservableCollection<Task_Device> ListTask_Device
        {
            get => listTask_Device;
            set
            {
                listTask_Device = value;
                MChangeProperty = "ListTask_Device";
            }
        }

        public Task_Device GetTask_Device(Guid id)
        {
            Task_Device task_Device = null;
            using (EntitiesDb db = GetDb)
            {
                task_Device = db.Task_Device.FirstOrDefault(s => s.Id == id);
            }
            return task_Device;
        }


        #endregion Task_Device

        //#region Type_Device

        //public void GetTypeTask()
        //{
        //    //string typeTask = new string { };

        //    //listTypeTask = new ObservableCollection<string>();


        //    ////foreach (string item in TypeTask)
        //    ////{
        //    ////    listTask_Device.Add(item);
        //    ////}


        //    //ListTypeTask = listTypeTask;
        //}

        ////public List<string> List_Type_Device
        ////{
        ////    get; set;
        ////}


        ////public enum TypeTask
        ////{
        ////    th = 1,
        ////    zip = 2,
        ////    auditarch = 3,
        ////    level = 4,
        ////    cassa = 5,
        ////    MSG_Water = 6,
        ////    MSG_Fuel = 7,
        ////};

        ////public void GetTypeTask()
        ////{
        ////    TypeTask typeTask = new TypeTask { };

        ////    listTypeTask = new ObservableCollection<string>();


        ////    //foreach (string item in TypeTask)
        ////    //{
        ////    //    listTask_Device.Add(item);
        ////    //}


        ////    ListTypeTask = listTypeTask;
        ////}
        ////private ObservableCollection<string> listTypeTask;
        ////public ObservableCollection<string> ListTypeTask
        ////{
        ////    get => listTypeTask;
        ////    set
        ////    {
        ////        listTypeTask = value;
        ////        MChangeProperty = "ListTypeTask";
        ////    }
        ////}

        //#endregion Type_Device


        #endregion ObservableCollection

        //Утилиты
        public string GetChooseDirectory(string HeadText, string SetFirstCatalog = "")
        {
            string outStr = String.Empty;
            try
            {
                System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog
                {
                    Description = HeadText,// "Директория куда будет перенесен архив";
                    SelectedPath = Base_Dir,
                };
                if (SetFirstCatalog != "")
                {
                    fd.SelectedPath = SetFirstCatalog;
                }
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outStr = fd.SelectedPath;
                }
            }
            catch (Exception er)
            {
                //MethodBase.GetCurrentMethod().Name.SetLogServiceError(this, er);

                //string msg = string.Format("\r\nMessage\r\n{0}\r\nGetType\r\n{1}\r\nStackTrace\r\n{2}\r\nInnerException\r\n{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
                //_ = new WindDialog(msg, _FontSize: 16, IsError: true).ShowDialog();
            }
            return outStr;
        }

    }
}
