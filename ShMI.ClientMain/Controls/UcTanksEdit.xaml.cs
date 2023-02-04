using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTanksEdit.xaml
    /// </summary>
    public partial class UcTanksEdit : UserControl
    {
        public UcTanksEdit(ModuleUcTanks ModuleMain, NTank EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcTanks Module
        {
            get => mainGrid.DataContext as ModuleUcTanks;
            set => mainGrid.DataContext = value;
        }

    }
}
