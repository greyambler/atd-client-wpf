using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcStart.xaml
    /// </summary>
    public partial class Uc_Test : UserControl
    {
        public Uc_Test(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUc_Test(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
        }
        public Uc_Test(ModuleUc_Test ModuleUc, TestTable EditItem)
        {
            InitializeComponent();

            Module = ModuleUc;
            Module.CurrentItem = EditItem;
        }
        public ModuleUc_Test Module
        {
            get => DataContext as ModuleUc_Test;
            set => DataContext = value;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            //bool isShow = Module.IsExist_Servise();

            //(UcListButton as UcListButtons).Btn_Visibility(UcListButtons.BtnType.btn_Add);

            //(UcListButton as UcListButtons).Btn_Visibility(UcListButtons.BtnType.btn_Edit);

            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Util, btnVisib: System.Windows.Visibility.Visible);
            ucListButton.Btn_Text(UcListButtons.BtnType.btn_Util, "Пример");

        }

    }
}
