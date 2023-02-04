using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcTasks : ModuleMainWindow
    {
        public ModuleUcTasks(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }

        private void InitTables()
        {
            GetRowsNObject();
            GetRowsTask_Device();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new Task_Device("TASK");
            SetWidthListButton(new UcTasksEdit(this, CurrentItem));
        }

        public override void EditItem()
        {
            SetWidthListButton(new UcTasksEdit(this, CurrentItem));
        }

        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.Type_Device}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteTask_Device(CurrentItem);
                }
                CurrentItem = null;
                InitTables();
            }
        }

        public override void SaveItem()
        {
            if (CurrentItem.Period_Task <= 0)
            {
                _ = new WindDialog(WindDialog.DialogType.Error, "\nПоле \"Период опроса(c)\" незаполнено.\nСохранение невозможно.\n", _FontSize: 16).ShowDialog();
            }
            else
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                    CurrentItem.Id == Guid.Empty ? $"\nДобавить расписание - \"{CurrentItem.Type_Device}\"?\n" : $"\nСохранить расписание - \"{CurrentItem.Type_Device}\"?\n", _FontSize: 16);
                if ((bool)d.ShowDialog())
                {
                    using (EntitiesDb db = GetDb)
                    {

                        try
                        {
                            db.ValidTask_Device(CurrentItem);

                            db.SaveTask_Device(CurrentItem);
                            InitTables();
                            SetWidthListButton(new UcTasks(this, CurrentItem));
                        }
                        catch (Exception er)
                        {
                            _ = new WindDialog(WindDialog.DialogType.Error, $"\nОшибка сохранения.\n{er.Message}", _FontSize: 16).ShowDialog();
                        }
                    }
                }
            }
        }

        public override void UtilItem()
        {
            _ = MessageBox.Show("UtilItem");
        }

        public override void Cancel()
        {
            SetWidthListButton(new UcTasks(this, null));
        }

        #endregion IListButtonsService

        private Task_Device currentItem;

        public Task_Device CurrentItem
        {
            get => currentItem; set
            {
                currentItem = value;
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
                MChangeProperty = "CurrentItem";
            }
        }

        #region EditItem

        public int Period_Task
        {
            get => CurrentItem.Period_Task;
            set
            {
                CurrentItem.Period_Task = value;
                MChangeProperty = "Period_Task";
            }
        }

        public DateTime? Date_LastLine_Struna
        {
            get => CurrentItem.Date_LastLine_Struna;
            set
            {
                CurrentItem.Date_LastLine_Struna = value;
                MChangeProperty = "Date_LastLine_Struna";
            }
        }

        public DateTime? Date_LastLine_Cassa
        {
            get => CurrentItem.Date_LastLine_Cassa;
            set
            {
                CurrentItem.Date_LastLine_Cassa = value;
                MChangeProperty = "Date_LastLine_Cassa";
            }
        }

        public int? Period_Ping
        {
            get => CurrentItem.Period_Ping;
            set
            {
                CurrentItem.Period_Ping = value;
                MChangeProperty = "Period_Ping";
            }
        }

        public string Name_Task
        {
            get => CurrentItem.Name_Task;
            set
            {
                CurrentItem.Name_Task = value;
                MChangeProperty = "Name_Task";
            }
        }

        public NObject NObject
        {
            get => CurrentItem.NObject;
            set
            {
                CurrentItem.NObject = value;
                MChangeProperty = "NObject";
            }
        }

        public NObject CurrentNObject
        {
            get => ListNObject.FirstOrDefault(s => s.Id == currentItem.NObjectId);
            set
            {
                CurrentItem.NObjectId = value.ThisNotNull() ? value.Id : Guid.Empty;
                MChangeProperty = "CurrentNObject";
            }
        }

        public string Zip_Dir
        {
            get => CurrentItem.Zip_Dir;
            set
            {
                CurrentItem.Zip_Dir = value;
                MChangeProperty = "Zip_Dir";
            }
        }

        public int CountDaySave
        {
            get => CurrentItem.CountDaySave;
            set
            {
                CurrentItem.CountDaySave = value;
                MChangeProperty = "CountDaySave";
            }
        }

        public string Status
        {
            get => CurrentItem.Status;
            set
            {
                CurrentItem.Status = value;
                MChangeProperty = "Status";
            }
        }

        #endregion EditItem

    }
}
