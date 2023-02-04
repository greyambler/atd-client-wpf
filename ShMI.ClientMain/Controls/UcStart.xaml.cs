using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcStart.xaml
    /// </summary>
    public partial class UcStart : UserControl, Interface.IUcChildren
    {
        public UcStart(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();
            Module = new ModuleUcStart(ModuleMain.ShellWindow, ModuleMain.WorkGrid, ModuleMain.ResourcesDict, ModuleMain.IsAdmin, ModuleMain.DispatcherShell);
            UcListButton = ucListButton;
        }

        public ModuleUcStart Module
        {
            get => (ModuleUcStart)DataContext;
            set => DataContext = value;
        }

        #region IUcChildren

        public UserControl UcListButton { get; }

        #endregion IUcChildren

    }
}
