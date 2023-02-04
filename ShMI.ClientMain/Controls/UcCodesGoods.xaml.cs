using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcCodesGoods.xaml
    /// </summary>
    public partial class UcCodesGoods : UserControl
    {
        public UcCodesGoods(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcCodesGoods(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }
        public UcCodesGoods(ModuleUcCodesGoods ModuleUc, BaseMain.ReCodesTable EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcCodesGoods Module
        {
            get => DataContext as ModuleUcCodesGoods;
            set => DataContext = value;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            ucListButton .Btn_Visibility(UcListButtons.BtnType.btn_Util, btnVisib: System.Windows.Visibility.Collapsed);
        }
    }
}
