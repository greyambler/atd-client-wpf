using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTH.xaml
    /// </summary>
    public partial class UcTH : UserControl
    {
        public UcTH(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcTH(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell)
            {
                UcSpinner = Spinner,
            };
        }

        public UcTH(ModuleUcTH ModuleUc, BaseMain.NCassa EditItem)
        {
            InitializeComponent();

            Module = new ModuleUcTH(ModuleUc.ShellWindow, ModuleUc.WorkGrid, ModuleUc.ResourcesDict, ModuleUc.IsAdmin, ModuleUc.DispatcherShell)
            {
                UcSpinner = Spinner,
            };

            if (EditItem.ThisNotNull())
            {
                Module.CurrentItem = EditItem;
            }
        }

        public ModuleUcTH Module
        {
            get => DataContext as ModuleUcTH;
            set => DataContext = value;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            ucListButton.Btn_Text(UcListButtons.BtnType.btn_Util, "Опросить");
        }
    }
}
