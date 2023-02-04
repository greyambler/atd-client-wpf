using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcCassaEdit.xaml
    /// </summary>
    public partial class UcCassaEdit : UserControl
    {
        public UcCassaEdit(ModuleUcCassa ModuleMain, NCassa EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcCassa Module
        {
            get => DataContext as ModuleUcCassa;
            set => DataContext = value;
        }

        private void Btn_CassaDate_Click(object sender, RoutedEventArgs e)
        {
            //if (Module.Edit_Item.Id == Guid.Empty)
            //{
            //    Module.SaveItem();
            //}

            //Module.Full_Table_NCassa_ForDate(Module.Edit_Item);
            //Module.Cancel();
        }

        private void Btn_CleanID_Click(object sender, RoutedEventArgs e)
        {
            //if (Module.Edit_Item.Id == Guid.Empty)
            //{
            //    Module.SaveItem();
            //}
            //Module.ClearnDB(Module.Edit_Item);
        }

        private void Btn_CassaID_Click(object sender, RoutedEventArgs e)
        {
            //if (Module.Edit_Item.Id == Guid.Empty)
            //{
            //    Module.SaveItem();
            //}
            //Module.Full_Table_NCassa_ForID(Module.Edit_Item);
            //Module.Cancel();
        }
    }
}
