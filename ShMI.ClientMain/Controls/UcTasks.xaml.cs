using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTasks.xaml
    /// </summary>
    public partial class UcTasks : UserControl
    {
        public UcTasks(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();

            Module = new ModuleUcTasks(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);

        }
        public UcTasks(ModuleUcTasks ModuleUc, Task_Device EditItem)
        {
            InitializeComponent();
            System.Guid id = EditItem.ThisNotNull() ? EditItem.Id : System.Guid.Empty;
            Module = new ModuleUcTasks(ModuleUc.ShellWindow, ModuleUc.WorkGrid, ModuleUc.ResourcesDict, ModuleUc.IsAdmin, ModuleUc.DispatcherShell);
            if (id != System.Guid.Empty)
            {
                Module.CurrentItem = Module.GetTask_Device(id);
            }
        }

        public ModuleUcTasks Module
        {
            get => mainGrid.DataContext as ModuleUcTasks;
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
