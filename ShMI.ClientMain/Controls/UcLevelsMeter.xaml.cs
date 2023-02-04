using ShMI.ClientMain.Interface;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcLevelsMeter.xaml
    /// </summary>
    public partial class UcLevelsMeter : UserControl
    {
        public UcLevelsMeter(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcLevelsMeter(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }
        public UcLevelsMeter(ModuleUcLevelsMeter ModuleUc, BaseMain.NStruna EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcLevelsMeter Module
        {
            get => mainGrid.DataContext as ModuleUcLevelsMeter;
            set => mainGrid.DataContext = value;
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
