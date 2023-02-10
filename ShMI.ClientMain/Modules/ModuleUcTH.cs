using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;
using ShMI.ClientMain.MainWindowControls;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcTH : ModuleMainWindow
    {
        public ModuleUcTH( Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore )
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            GetRowsNCassa();

            InitTables();
        }
        private void InitTables( NCassa CurrentItem = null )
        {
            if (!CurrentItem.ThisNotNull())
            {
                GetRowsNObject();
                GetRowsTask_Device();
                GetRowsNTank();
                GetRowsNStruna();
                GetRowsReCodesTable();
            }
            else
            {

                GetItemsFromNCassa(CurrentItem, TypeTable.NCassa);
                GetItemsFromNCassa(CurrentItem, TypeTable.NObject);
                GetItemsFromNCassa(CurrentItem, TypeTable.NTank);
                GetItemsFromNCassa(CurrentItem, TypeTable.NStruna);
                GetItemsFromNCassa(CurrentItem, TypeTable.Task_Device);

                CurrentNObject = ListNObject.FirstOrDefault(s => s.Id == CurrentItem.NObjectId);
            }
        }

        public UcSpinner UcSpinner
        {
            get; set;
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new NCassa("th");
            SetWidthListButton(new UcTHEdit(this, CurrentItem));
        }
        public override void EditItem()
        {
            SetWidthListButton(new UcTHEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.Name}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteNCassa(CurrentItem);
                }
                CurrentItem = null;
                GetRowsNCassa();
            }
        }
        public override void SaveItem()
        {
            if (string.IsNullOrEmpty(CurrentItem.IP) || CurrentItem.Port < 0)
            {
                _ = new WindDialog(WindDialog.DialogType.Error, "\nНе все обязательные поля заполнены.\nСохранение невозможно.\n", _FontSize: 16).ShowDialog();
            }
            else
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                CurrentItem.Id == Guid.Empty ? $"\nДобавить TestTable - \"{CurrentItem.Name}\"?\n" : $"\nСохранить TestTable - \"{CurrentItem.Name}\"?\n", _FontSize: 16);
                if ((bool)d.ShowDialog())
                {
                    using (EntitiesDb db = GetDb)
                    {
                        CurrentItem.NObjectId = CurrentNObject.Id;
                        db.SaveNCassa(CurrentItem);
                        GetRowsNCassa();
                        SetWidthListButton(new UcTH(this, null));
                    }
                }
            }
        }

        // Опросить
        public override void UtilItem()
        {
            _ = Task.Factory.StartNew(() =>
            {
                try
                {
                    // Поток спинера открывает
                    DispatcherShellWork(UcSpinner, Visibility.Visible);

                    GetSaveDataFromTH(CurrentItem);
                    //System.Threading.Thread.Sleep(10000);
                }
                catch (Exception er)
                {
                    _ = DispatcherShell.BeginInvoke((Action)(() =>
                    {
                        WindDialog d = new WindDialog(WindDialog.DialogType.Error, $"\nОшибка инициализация приложения.\n{er.Message}", _FontSize: 16);
                        _ = d.ShowDialog();
                    }));
                }
                finally
                {
                    GetRowsNCassa();
                    /*иницализация***/
                    InitTables();

                    // Поток спинера закрывается
                    DispatcherShellWork(UcSpinner, Visibility.Collapsed);
                }
            });
        }

        public override void Cancel()
        {
            SetWidthListButton(new UcTH(this, CurrentItem));
        }

        #endregion IListButtonsService

        private NCassa currentItem;
        public NCassa CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
                InitTables(currentItem);
                MChangeProperty = "CurrentItem";
            }
        }

        public NObject currentNObject;
        public NObject CurrentNObject
        {
            get => currentNObject;
            set
            {
                currentNObject = value;
                MChangeProperty = "CurrentNObject";
            }
        }
    }
}
