using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcTanks : ModuleMainWindow
    {
        public ModuleUcTanks( Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore )
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            GetRowsNTank();

            InitTables();
        }
        private void InitTables()
        {


            //GetNStruna();
            //GetNTank();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            //_ = MessageBox.Show("AddItem");

            CurrentItem = new NTank();
            SetWidthListButton(new UcTanksEdit(this, CurrentItem));

        }
        public override void EditItem()
        {
            SetWidthListButton(new UcTanksEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.TankNumber}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteNTank(CurrentItem);
                }
                CurrentItem = null;
                InitTables();
            }

            //_ = MessageBox.Show("DeleteItem");
        }
        public override void SaveItem()
        {
            //_ = MessageBox.Show("SaveItem");

            //if (string.IsNullOrEmpty(CurrentItem.Name_Object) || string.IsNullOrEmpty(CurrentItem.SiteID))
            //{
            //    _ = new WindDialog(WindDialog.DialogType.Error, "\nНе все обязательные поля заполнены.\nСохранение невозможно.\n", _FontSize: 16).ShowDialog();
            //}
            //else
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                CurrentItem.Id == Guid.Empty ? $"\nДобавить TestTable - \"{CurrentItem.TankNumber}\"?\n" : $"\nСохранить TestTable - \"{CurrentItem.Grade_Name}\"?\n", _FontSize: 16);
                if ((bool)d.ShowDialog())
                {
                    using (EntitiesDb db = GetDb)
                    {
                        db.SaveNTank(CurrentItem);

                        GetRowsNTank();

                        SetWidthListButton(new UcTanks(this, null));
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
            SetWidthListButton(new UcTanks(this, null));
        }

        #endregion IListButtonsService

        private NTank currentItem;
        public NTank CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                MChangeProperty = "CurrentItem";
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private NStruna currentNStruna;
        public NStruna CurrentNStruna
        {
            get => currentNStruna;
            set
            {
                currentNStruna = value;
                CurrentItem.NStrunaId = currentNStruna.ThisNotNull() ? currentNStruna.Id : Guid.Empty;

                MChangeProperty = "CurrentNStruna";
            }
        }
    }
}
