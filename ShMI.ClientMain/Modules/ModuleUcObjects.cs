using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcObjects : ModuleMainWindow
    {
        public ModuleUcObjects(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables(NObject currentItem = null)
        {
            if (!currentItem.ThisNotNull())
            {
                GetRowsNObject();
                GetRowsTask_Device();
                GetRowsNCassa();
                GetRowsNTank();
                GetRowsNStruna();
                GetRowsReCodesTable();
            }
            else
            {
                GetRowsTask_Device(currentItem);
                GetRowsNCassa(currentItem);
                GetRowsNTank(currentItem);
                GetRowsNStruna(currentItem);
                GetRowsReCodesTable();
            }
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new NObject();
            SetWidthListButton(new UcObjectsEdit(this, CurrentItem));
        }
        public override void EditItem()
        {
            SetWidthListButton(new UcObjectsEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.Name_Object}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteNObject(CurrentItem);
                }
                CurrentItem = null;
                InitTables();
            }
        }
        public override void SaveItem()
        {
            if (string.IsNullOrEmpty(CurrentItem.Name_Object) || string.IsNullOrEmpty(CurrentItem.SiteID))
            {
                _ = new WindDialog(WindDialog.DialogType.Error, "\nНе все обязательные поля заполнены.\nСохранение невозможно.\n", _FontSize: 16).ShowDialog();
            }
            else
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                CurrentItem.Id == Guid.Empty ? $"\nДобавить TestTable - \"{CurrentItem.Name_Object}\"?\n" : $"\nСохранить TestTable - \"{CurrentItem.Name_Object}\"?\n", _FontSize: 16);
                if ((bool)d.ShowDialog())
                {
                    try
                    {
                        using (EntitiesDb db = GetDb)
                        {
                            db.SaveNObject(CurrentItem);
                            GetRowsNObject();
                            SetWidthListButton(new UcObjects(this, null));
                        }
                    }
                    catch (Exception er)
                    {
                        _ = new WindDialog(WindDialog.DialogType.Error, $"\nОшибка сохранения.\n{er.Message}", _FontSize: 16).ShowDialog();
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
            SetWidthListButton(new UcObjects(this, null));
        }

        #endregion IListButtonsService

        private NObject currentItem;
        public NObject CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
                MChangeProperty = "CurrentItem";
                InitTables(currentItem);
            }
        }
        #region EditItem

        #endregion EditItem

        //private NCassa currentNCassa;
        //public NCassa CurrentNCassa
        //{
        //    get => currentNCassa;
        //    set
        //    {
        //        currentNCassa = value;
        //        MChangeProperty = "CurrentNCassa";
        //    }
        //}


        //private NStruna currentNStruna;
        //public NStruna CurrentNStruna
        //{
        //    get => currentNStruna;
        //    set
        //    {
        //        currentNStruna = value;
        //        MChangeProperty = "CurrentNStruna";
        //    }
        //}

        //private Task_Device currentTask_Device;
        //public Task_Device CurrentTask_Device
        //{
        //    get => currentTask_Device;
        //    set
        //    {
        //        currentTask_Device = value;
        //        MChangeProperty = "CurrentTask_Device";
        //    }
        //}

    }
}
