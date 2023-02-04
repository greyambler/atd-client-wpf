using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTanks.xaml
    /// </summary>
    public partial class UcTanks : UserControl
    {
        public UcTanks(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcTanks(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }

        public UcTanks(ModuleUcTanks ModuleUc, BaseMain.NTank EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcTanks Module
        {
            get => mainGrid.DataContext as ModuleUcTanks;
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
