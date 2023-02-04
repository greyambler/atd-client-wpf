using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcCassa.xaml
    /// </summary>
    public partial class UcCassa : UserControl
    {
        public UcCassa(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcCassa(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }

        public UcCassa(ModuleUcCassa ModuleUc, BaseMain.NCassa EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcCassa Module
        {
            get => DataContext as ModuleUcCassa;
            set => DataContext = value;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Util, btnVisib: System.Windows.Visibility.Collapsed);
        }
    }
}
