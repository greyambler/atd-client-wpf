using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcService.xaml
    /// </summary>
    public partial class UcService : UserControl
    {
        public UcService(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcService(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            bool isShow = Module.IsExist_Servise();

            ucListButton.Btn_Text(UcListButtons.BtnType.btn_Add, "Установить");
            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Add,
                 !isShow ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);

            ucListButton.Btn_Text(UcListButtons.BtnType.btn_Edit, "Тест1");
            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Edit,
                System.Windows.Visibility.Collapsed);

            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Delete,
                btnVisib: isShow ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);

            ucListButton.Btn_Text(UcListButtons.BtnType.btn_Util, "Рестарт сервис");
            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Util,
                isShow ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);

        }

        public ModuleUcService Module
        {
            get => mainGrid.DataContext as ModuleUcService;
            set => mainGrid.DataContext = value;
        }
    }
}
