using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcCodesGoods : ModuleMainWindow
    {
        public ModuleUcCodesGoods(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables()
        {
            GetRowsReCodesTable();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            SetWidthListButton(new UcCodesGoodsEdit(this, new ReCodesTable()));
        }
        public override void EditItem()
        {
            SetWidthListButton(new UcCodesGoodsEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteReCodesTable(CurrentItem);
                }
                InitTables();
            }
        }
        public override void SaveItem()
        {
            if (string.IsNullOrEmpty(CurrentItem.GlobalCode) || string.IsNullOrEmpty(CurrentItem.LocalCode))
            {
                _ = new WindDialog(WindDialog.DialogType.Error, "\nНе все обязательные поля заполнены.\nСохранение невозможно.\n", _FontSize: 16).ShowDialog();
            }
            else
            {
                WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                CurrentItem.Id == Guid.Empty ? $"\nДобавить TestTable - \"{CurrentItem}\"?\n" : $"\nСохранить TestTable - \"{CurrentItem}\"?\n", _FontSize: 16);
                if ((bool)d.ShowDialog())
                {
                    using (EntitiesDb db = GetDb)
                    {
                        db.SaveReCodesTable(CurrentItem);

                        //Guid idCurrentItem = CurrentItem.Id;
                        GetRowsReCodesTable();
                        //CurrentItem = ListReCodesTable.FirstOrDefault(s => s.Id == idCurrentItem);
                        SetWidthListButton(new UcCodesGoods(this, null));
                    }
                }
            }
        }
        public override void UtilItem()
        {
            _ = MessageBox.Show("Опросить UtilItem");
        }
        public override void Cancel()
        {
            SetWidthListButton(new UcCodesGoods(this, null));
        }

        #endregion IListButtonsService


        private ReCodesTable currentItem;
        public ReCodesTable CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                MChangeProperty = "CurrentItem";
                IsExistItemMain = value.ThisNotNull() ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
