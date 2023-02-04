﻿using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcLevelsMeter : ModuleMainWindow
    {
        //public ModuleUcLevelsMeter(ModuleMainWindow ModuleMain, UserControl uc)
        public ModuleUcLevelsMeter(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables()
        {
            //GetNObject();

            GetNStruna();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            CurrentItem = new NStruna("STRN");
            SetWidthListButton(new UcLevelsMeterEdit(this, CurrentItem));
        }
        public override void EditItem()
        {
            SetWidthListButton(new UcLevelsMeterEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.Name}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteNStruna(CurrentItem);
                }
                CurrentItem = null;
                InitTables();
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
                        db.SaveNStruna(CurrentItem);
                    }
                    InitTables();
                    SetWidthListButton(new UcLevelsMeter(this, CurrentItem));
                }
            }
        }
        public override void UtilItem()
        {
            _ = MessageBox.Show("UtilItem");
        }
        public override void Cancel()
        {
            SetWidthListButton(new UcLevelsMeter(this, null));
        }

        #endregion IListButtonsService

        private NStruna currentItem;
        public NStruna CurrentItem
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


        public string currentTypeLevel;
        public string CurrentTypeLevel
        {
            get => currentItem.Type_Level.ThisNotNull() ? ListTypeLevel.FirstOrDefault(s => s == currentItem.Type_Level) : ListTypeLevel.FirstOrDefault();
            set
            {
                currentTypeLevel = value;
                CurrentItem.Type_Level = currentTypeLevel;
                MChangeProperty = "CurrentTypeLevel";
            }
        }
    }
}