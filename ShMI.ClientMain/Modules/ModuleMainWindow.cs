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
            DispatcherShell = DispatcherCore;
        }

        #region Инициализация DB ATD

        public bool IsExistObject()
        {
            bool isExistObject = false;
            using (EntitiesDb db = GetDb)
            {
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

                _ = new WindDialog(WindDialog.DialogType.Error, $"\n{er.Message}\n", _FontSize: 16).ShowDialog();
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

        private DispatcherTimer timer;
        public void StartTime()
        {
            timer = new DispatcherTimer
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

        public Window ShellWindow
        {
            get;
        }
        public Grid WorkGrid
        {
            get;
        }
        public ResourceDictionary ResourcesDict
        {
            get;
        }
        public void SetWidthListButton(UserControl _UserControl = null, bool visual = true)
        {
            WidthListButton = !visual ? 0 : 180;
            if (WorkGrid.ThisNotNull() && _UserControl.ThisNotNull())
            {
                WorkGrid.Children.Clear();
                _ = WorkGrid.Children.Add(_UserControl);
            }
        }
        public Dispatcher DispatcherShell
        {
            get; set;
        }

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

        public virtual void AddItem()
        {
        }
        public virtual void EditItem()
        {
        }
        public virtual void DeleteItem()
        {
        }
        public virtual void SaveItem()
        {
        }
        public virtual void CancelItem()
        {
        }
        public virtual void UtilItem()
        {
        }
        public virtual void Cancel()
        {
        }

        #endregion IListButtonsService

        // Общие объекты для главных окон UC.
        #region ObservableCollection 

        public enum TypeTable
        {
            NCassa,
            NObject,
            NTank,
            NStruna,
            Task_Device
        }

        public void GetItemsFromNCassa(NCassa CurrentItem, TypeTable typeTable)
        {
            NObject currentNobject = null;

            if (CurrentItem.ThisNotNull() && CurrentItem.NObjectId != Guid.Empty)
            {
                using (EntitiesDb db = GetDb)
                {
                    currentNobject = db.NObjects.FirstOrDefault(s => s.Id == CurrentItem.NObjectId);
                }
            }
            switch (typeTable)
            {
                case TypeTable.NCassa:
                    {
                        GetRowsNCassaNotTH(currentNobject);
                        break;
                    }
                case TypeTable.NObject:
                    {
                        GetRowsNObject(currentNobject);
                        break;
                    }
                case TypeTable.NTank:
                    {
                        GetRowsNTank(currentNobject);
                        break;
                    }
                case TypeTable.NStruna:
                    {
                        GetRowsNStruna(currentNobject);
                        break;
                    }
                case TypeTable.Task_Device:
                    {
                        GetRowsTask_Device(currentNobject);
                        break;
                    }

                default:
                    break;
            }
        }

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

        public void GetRowsNObject(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listNObject = new ObservableCollection<NObject>();

            using (EntitiesDb db = GetDb)
            {

                List<NObject> listLocal = idObj != Guid.Empty
                    ? db.NObjects.Where(s => s.Id == idObj).ToList()
                    : db.NObjects.ToList();

                foreach (NObject item in listLocal)
                {
                    listNObject.Add(item);
                }
            }
            ListNObject = listNObject;
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

        public void GetRowsReCodesTable()
        {
            listReCodesTable = new ObservableCollection<ReCodesTable>();
            using (EntitiesDb db = GetDb)
            {
                List<ReCodesTable> listLocal = db.ReCodesTables.ToList();
                foreach (ReCodesTable item in listLocal)
                {
                    listReCodesTable.Add(item);
                }
            }
            ListReCodesTable = listReCodesTable;
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

        public void GetRowsNCassa(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listNCassa = new ObservableCollection<NCassa>();

            listTH = new ObservableCollection<NCassa>();

            using (EntitiesDb db = GetDb)
            {
                List<NCassa> listLocal = idObj != Guid.Empty ? db.NCassas.Where(s => s.NObjectId == idObj).ToList() : db.NCassas.ToList();

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

            }

            ListNCassa = listNCassa;

            ListTH = listTH;

        }

        public void GetRowsNCassaNotTH(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listNCassa = new ObservableCollection<NCassa>();

            using (EntitiesDb db = GetDb)
            {
                List<NCassa> listLocal = idObj != Guid.Empty ? db.NCassas.Where(s => s.NObjectId == idObj).ToList() : db.NCassas.ToList();

                foreach (NCassa item in listLocal)
                {
                    if (item.Name.Contains("CASSA"))
                    {
                        listNCassa.Add(item);
                    }
                }
            }

            ListNCassa = listNCassa;

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

        public void GetRowsNTank(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listNTank = new ObservableCollection<NTank>();

            using (EntitiesDb db = GetDb)
            {

                List<NTank> listLocal = idObj != Guid.Empty
                    ? db.NTanks.Include("NStruna")//.Where(s => s.NObjectId == idObj).ToList()
                    .Where(s => s.NStruna.NObjectId == idObj).ToList()
                    : db.NTanks.ToList();

                foreach (NTank item in listLocal)
                {
                    listNTank.Add(item);
                }
            }
            ListNTank = listNTank;
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

        public void GetRowsNStruna(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listNStruna = new ObservableCollection<NStruna>();

            using (EntitiesDb db = GetDb)
            {
                List<NStruna> listLocal = idObj != Guid.Empty
                    ? db.NStrunas.Where(s => s.NObjectId == idObj).ToList()
                    : db.NStrunas.ToList();

                foreach (NStruna item in listLocal)
                {
                    listNStruna.Add(item);
                }
            }
            ListNStruna = listNStruna;
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

        public void GetRowsTask_Device(NObject currentItem = null)
        {
            string Id = currentItem.ThisNotNull() ? currentItem.Id.ToString() : "";
            _ = Guid.TryParse(Id, out Guid idObj);

            listTask_Device = new ObservableCollection<Task_Device>();

            using (EntitiesDb db = GetDb)
            {

                List<Task_Device> listLocal = idObj != Guid.Empty
                    ? db.Task_Device.Where(s => s.NObjectId == idObj).ToList()
                    : db.Task_Device.ToList();

                foreach (Task_Device item in listLocal)
                {
                    item.NObject = db.NObjects.FirstOrDefault(s => s.Id == item.NObjectId);
                    item.InitListType_Device();
                    listTask_Device.Add(item);
                }
            }
            ListTask_Device = listTask_Device;

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
