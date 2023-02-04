using ShMI.ClientMain.Interface;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcObjects.xaml
    /// </summary>
    public partial class UcObjects : UserControl
    {

        public UcObjects(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcObjects(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }
        public UcObjects(ModuleUcObjects ModuleUc, BaseMain.NObject EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcObjects Module
        {
            get => DataContext as ModuleUcObjects;
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

