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
        public ModuleUcCassa( Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore )
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            GetRowsNCassa();

            InitTables();
        }
        private void InitTables( NCassa CurrentItem = null )
        {
            _ = DispatcherShell.BeginInvoke((Action)(() =>
            {
                if (!CurrentItem.ThisNotNull())
                {
                    GetRowsNObject();
                }
                else
                {
                    GetItemsFromNCassa(CurrentItem, TypeTable.NObject);

                    CurrentNObject = ListNObject.FirstOrDefault(s => s.Id == CurrentItem.NObjectId);
                }
            }));
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new NCassa("CASSA");
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
            if (string.IsNullOrEmpty(CurrentItem.IP) || CurrentItem.Port < 0 || !CurrentNObject.ThisNotNull() || CurrentNObject.Id == Guid.Empty)
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
                        SetWidthListButton(new UcCassa(this, null));
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
            SetWidthListButton(new UcCassa(this, CurrentItem));
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
