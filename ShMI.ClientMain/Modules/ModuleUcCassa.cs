using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcCassa : ModuleMainWindow
    {
        public ModuleMainWindow ModuleMain { get; set; }

        public ModuleUcCassa(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables()
        {
            //GetNObject();
            //GetNCassa();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new NCassa("CASSA");
            currentNObject = CurrentItem.CurrentNObject;
            SetWidthListButton(new UcCassaEdit(this, CurrentItem));

        }
        public override void EditItem()
        {
            SetWidthListButton(new UcCassaEdit(this, CurrentItem));
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
                InitTables();
            }
        }
        public override void SaveItem()
        {
            if (string.IsNullOrEmpty(CurrentItem.IP) || CurrentItem.Port < 0 || CurrentItem.NObjectId == Guid.Empty)
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
                        db.SaveNCassa(CurrentItem);
                    }
                    Guid idCurrentItem = CurrentItem.Id;
                    InitTables();
                    CurrentItem = ListTH.FirstOrDefault(s => s.Id == idCurrentItem);
                    SetWidthListButton(new UcCassa(this, CurrentItem));
                }
            }
        }
        public override void UtilItem()
        {
            _ = MessageBox.Show("UtilItem");
        }
        public override void Cancel()
        {
            SetWidthListButton(new UcCassa(this, null));
        }

        #endregion IListButtonsService

        private NCassa currentItem;
        public NCassa CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                MChangeProperty = "CurrentItem";
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public NObject currentNObject;
        public NObject CurrentNObject
        {
            get => ListNObject.FirstOrDefault(s => s.Id == currentItem.NObjectId);
            set
            {
                currentNObject = value;
                CurrentItem.NObjectId = currentNObject.ThisNotNull() ? currentNObject.Id : Guid.Empty;
                MChangeProperty = "CurrentNObject";
            }
        }
    }
}
