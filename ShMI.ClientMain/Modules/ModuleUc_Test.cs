using ShMI.BaseMain;
using ShMI.ClientMain.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUc_Test : ModuleMainWindow
    {
        public ModuleUc_Test(Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore)
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
            InitTables();
        }
        private void InitTables()
        {
            GetTestTable();
        }

        #region IListButtonsService

        public override void AddItem()
        {
            //_ = MessageBox.Show("AddItem  ModuleUc_Test");
            SetWidthListButton(new Uc_TestEdit(this, new TestTable()));
        }
        public override void EditItem()
        {
            //_ = MessageBox.Show("EditItem  ModuleUc_Test");
            SetWidthListButton(new Uc_TestEdit(this, CurrentItem));
        }
        public override void DeleteItem()
        {
            //_ = MessageBox.Show("DeleteItem  ModuleUc_Test");
            WindDialog d = new WindDialog(WindDialog.DialogType.Question, $"\nУдалить TestTable - \"{CurrentItem.name}\"?\n", _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.DeleteTestTable(CurrentItem);
                }
                InitTables();
            }
        }
        public override void SaveItem()
        {
            //_ = MessageBox.Show("SaveItem  ModuleUc_Test");
            WindDialog d = new WindDialog(WindDialog.DialogType.Question,
                CurrentItem.Id == 0 ? $"\nДобавить TestTable - \"{CurrentItem.name}\"?\n" : $"\nСохранить TestTable - \"{CurrentItem.name}\"?\n",
                _FontSize: 16);
            if ((bool)d.ShowDialog())
            {
                using (EntitiesDb db = GetDb)
                {
                    db.SaveTestTable(CurrentItem);
                }
                InitTables();

                SetWidthListButton(new Uc_Test(this, CurrentItem));
            }
        }
        public override void UtilItem()
        {
            _ = MessageBox.Show("UtilItem  ModuleUc_Test");
        }
        public override void Cancel()
        {
            //_ = MessageBox.Show("Cancel  ModuleUc_Test");
            SetWidthListButton(new Uc_Test(this, null));
        }

        #endregion IListButtonsService

        private TestTable currentItem;
        public TestTable CurrentItem
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